using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using System.IO;
using System.Runtime.InteropServices;
using NAudio.Wave;


namespace NAudioWrapper
{
    /// <summary>
    /// Обертка над NAudio
    /// </summary>
    static class NSampleData
    {
        /// <summary>
        /// Convert WavHeader to NAudio.Wave.WaveFormat
        /// </summary>
        public static NAudio.Wave.WaveFormat HeaderToFormat(
            ShazamO.WavHeader WavHeader)
        {
            return NAudio.Wave.WaveFormat.CreateCustomFormat(
                NAudio.Wave.WaveFormatEncoding.Pcm,
                (int)WavHeader.SampleRate,
                WavHeader.NumChannels,
                (int)WavHeader.ByteRate,
                WavHeader.BlockAlign,
                WavHeader.BitsPerSample);
        }

        /// <summary>
        /// Convert NAudio.Wave.WaveFormat to WavHeader
        /// </summary>
        public static ShazamO.WavHeader FormatToHeader(
            NAudio.Wave.WaveFormat WaveFormat, int FileSize)
        {
            ShazamO.WavHeader header = new ShazamO.WavHeader();
            header.AudioFormat = 1;
            header.BitsPerSample = (ushort)WaveFormat.BitsPerSample;
            header.BlockAlign = (ushort)WaveFormat.BlockAlign;
            header.ByteRate = (uint)WaveFormat.AverageBytesPerSecond;
            header.ChunkId = 0x46464952;
            header.ChunkSize = (uint)FileSize - 8;
            header.Format = 0x45564157;
            header.NumChannels = (ushort)WaveFormat.Channels;
            header.SampleRate = (uint)WaveFormat.SampleRate;
            header.Subchunk1Id = 0x20746d66;
            header.Subchunk1Size = 16;
            header.Subchunk2Id = 0x61746164;
            header.Subchunk2Size = (uint)(FileSize - Marshal.SizeOf(header.GetType()));

            return header;
        }

        /// <summary>
        /// Асинхронно считывает данные из MP3 файла в массив; верхний индекс - номер канала, нижний - номер сэмпла
        /// </summary>
        public static async Task<int[][]> DataFromMp3(Stream Stream)
        {
            using (var reader = new NAudio.Wave.Mp3FileReader(Stream))
            {
                return await Task<int[][]>.Factory.StartNew(() => _readMp3(reader));
            }
        }

        /// <summary>
        /// Считывает данные о заголоке из MP3 файла
        /// </summary>
        public static NAudio.Wave.WaveFormat WavHeaderFromMp3(Stream Stream)
        {
            using (var reader = new NAudio.Wave.Mp3FileReader(Stream))
            {
                return reader.WaveFormat;
            }
        }

        /// <summary>
        /// Получает данные PCM в виде файла и считывает их, поканально усредняя. 
        /// Необходимо внимательно следить за тем, сколько байт приходит в эту функцию,
        /// так как если они будут приходить со сдвигом от начала, отличным от количества
        /// байт на сэмпл, то информация считается некорректно.
        /// </summary>
        public static int[] ReadFromPcm(byte[] Data, int Channels, int BitsPerSample, int ByteOffset, int ByteSize)
        {
            int bytePerSample = BitsPerSample / 8 * Channels;
            if (ByteOffset + ByteSize > Data.Length) ByteSize = Data.Length - 1 - ByteOffset;
            if (ByteSize <= 0) return new int[0];
            int samples = ByteSize / bytePerSample;
            int[] receivedSamples = new int[samples];

            int n = ByteOffset;
            switch (BitsPerSample)
            {
                case 32:
                    #region 32 bitrate
                    for (int i = 0; i < samples; i++)
                    {
                        long v = 0;
                        for (int j = 0; j < Channels; j++)
                        {
                            v += BitConverter.ToInt32(Data, n);
                            n += 4;
                        }
                        receivedSamples[i] = (int)(v / Channels);
                    }
                    #endregion
                    break;

                case 24:
                    #region 24 bitrate
                    for (int i = 0; i < samples; i++)
                    {
                        int v = 0;
                        for (int j = 0; j < Channels; j++)
                        {
                            v += (Data[n] << 16) + (Data[n + 1] << 8) + Data[n + 2];
                            n += 3;
                        }
                        receivedSamples[i] = v / Channels;
                    }
                    #endregion
                    break;

                case 16:
                    #region 16 bitrate
                    for (int i = 0; i < samples; i++)
                    {
                        int v = 0;
                        for (int j = 0; j < Channels; j++)
                        {
                            v += BitConverter.ToInt16(Data, n);
                            n += 2;
                        }
                        receivedSamples[i] = v / Channels;
                    }
                    #endregion
                    break;

                case 8:
                    #region 8 bitrate
                    for (int i = 0; i < samples; i++)
                    {
                        int v = 0;
                        for (int j = 0; j < Channels; j++)
                        {
                            v += BitConverter.ToInt32(Data, n++);
                        }
                        receivedSamples[i] = v / Channels;
                    }
                    #endregion
                    break;

                default:
                    throw new NotImplementedException("This file format is not supported");
            }
            return receivedSamples;
        }


        private static int[][] _readMp3(NAudio.Wave.Mp3FileReader reader)
        {
            // Preparing converting
            int bytePerSample = reader.WaveFormat.BitsPerSample / 8 * reader.WaveFormat.Channels;

            int samplesCount = (int)(reader.Length / bytePerSample);

            int[][] receivedSamples = new int[reader.WaveFormat.Channels][];
            for (int i = 0; i < receivedSamples.Length; i++) receivedSamples[i] = new int[samplesCount];

            switch (reader.WaveFormat.BitsPerSample)
            {
                case 32:
                    #region 32 bitrate
                    for (int i = 0; i < samplesCount; i++)
                    {
                        byte[] read = new byte[bytePerSample];
                        int received = reader.Read(read, 0, bytePerSample);
                        if (received < bytePerSample) throw new EndOfStreamException();

                        for (int j = 0; j < receivedSamples.Length; j++)
                        {
                            receivedSamples[j][i] = BitConverter.ToInt32(read, j * 4);
                        }
                    }
                    #endregion
                    break;

                case 24:
                    #region 24 bitrate
                    for (int i = 0; i < samplesCount; i++)
                    {
                        byte[] read = new byte[bytePerSample];
                        int received = reader.Read(read, 0, bytePerSample);
                        if (received < bytePerSample) throw new EndOfStreamException();
                        int n = 0;

                        for (int j = 0; j < receivedSamples.Length; j++)
                        {
                            receivedSamples[j][i] = (read[n] << 16) + (read[n + 1] << 8) + read[n + 2];
                            n += 3;
                        }
                    }
                    #endregion
                    break;

                case 16:
                    #region 16 bitrate
                    for (int i = 0; i < samplesCount; i++)
                    {
                        byte[] read = new byte[bytePerSample];
                        int received = reader.Read(read, 0, bytePerSample);
                        if (received < bytePerSample) throw new EndOfStreamException();

                        for (int j = 0; j < receivedSamples.Length; j++)
                        {
                            receivedSamples[j][i] = BitConverter.ToInt16(read, j * 2);
                        }
                    }
                    #endregion
                    break;

                case 8:
                    #region 8 bitrate
                    for (int i = 0; i < samplesCount; i++)
                    {
                        byte[] read = new byte[bytePerSample];
                        int received = reader.Read(read, 0, bytePerSample);
                        if (received < bytePerSample) throw new EndOfStreamException();

                        for (int j = 0; j < receivedSamples.Length; j++)
                        {
                            receivedSamples[j][i] = BitConverter.ToInt32(read, j);
                        }
                    }
                    #endregion
                    break;

                default:
                    throw new NotImplementedException("This file format is not supported");
            }
            return receivedSamples;
        }

        /*
        public static Mp3Frame[] ReadMp3FramesFromStream(Stream stream)
        {
            var frames = new List<Mp3Frame>();
            while (true)
            {
                var frame = Mp3Frame.LoadFromStream(stream);
                if (frame == null) break;
                frames.Add(frame);
            }
            return frames.ToArray();
        }

        public static byte[] DecompressMp3(Stream stream, WaveFormat OutputFormat)
        {
            Mp3Frame[] frames = ReadMp3FramesFromStream(stream);
            WaveFormat format = null;
            using (MemoryStream mstream = new MemoryStream())
            {
                var waveFormat = new Mp3WaveFormat(frames[0].SampleRate,
                                        frames[0].ChannelMode == ChannelMode.Mono ? 1 : 2,
                                        frames[0].FrameLength, frames[0].BitRate);
                var decompressor = new AcmMp3FrameDecompressor(waveFormat);
                    
                foreach (var frame in frames)
                {
                    byte[] buffer = new byte[frame.SampleCount * decompressor.OutputFormat.BlockAlign];
                    int decompressed = decompressor.DecompressFrame(frame, buffer, 0);
                    format = decompressor.OutputFormat;
                    byte[] framePcmData = buffer.Take(decompressed).ToArray();

                    var provider = new BufferedWaveProvider(format);
                    provider.AddSamples(framePcmData, 0, framePcmData.Length);

                    using (var resampler = new MediaFoundationResampler(provider, OutputFormat))
                    {
                        byte[] resampleData = new byte[framePcmData.Length * OutputFormat.BlockAlign];
                        int resampled = resampler.Read(resampleData, 0, resampleData.Length);
                        mstream.Write(resampleData, (int)mstream.Position, resampled);
                    }
                }
                byte[] resultData = new byte[mstream.Length];
                int resulted = mstream.Read(resultData, 0, resultData.Length);
                return resultData.Take(resulted).ToArray();
            }
        }
         */
    }
}
