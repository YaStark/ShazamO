using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ShazamO
{
    /// <summary>
    /// Сериализуемый тип заголовка звукового файла
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    [Serializable()]
    public class WavHeader
    {
        /// <summary>
        /// "RIFF" (0x46464952)
        /// </summary>
        public UInt32 ChunkId;

        /// <summary>
        /// (размер файла - 8), то есть, исключены поля chunkId и chunkSize
        /// </summary>
        public UInt32 ChunkSize;

        /// <summary>
        /// "WAVE" (0x45564157)
        /// </summary>
        public UInt32 Format;
        
        /// <summary>
        /// "fmt " (0x20746d66)
        /// </summary>
        public UInt32 Subchunk1Id;

        /// <summary>
        /// оставшийся размер подцепочки, начиная с этой позиции (16)
        /// </summary>
        public UInt32 Subchunk1Size;

        /// <summary>
        /// Аудио формат, http://audiocoding.ru/wav_formats.txt
        /// Для PCM = 1 (то есть, Линейное квантование), иначе это сжатие
        /// </summary>
        public UInt16 AudioFormat;

        /// <summary>
        /// Количество каналов. Моно = 1, Стерео = 2 и т.д.
        /// </summary>
        public UInt16 NumChannels;

        /// <summary>
        /// Частота дискретизации. 8000 Гц, 44100 Гц и т.д.
        /// </summary>
        public UInt32 SampleRate;

        /// <summary>
        /// sampleRate * numChannels * bitsPerSample/8
        /// </summary>
        public UInt32 ByteRate;

        /// <summary>
        /// numChannels * bitsPerSample/8 (байт для одного сэмпла, включая все каналы)
        /// </summary>
        public UInt16 BlockAlign;

        /// <summary>
        /// Так называемая "глубиная" или точность звучания. 8 бит, 16 бит и т.д.
        /// </summary>
        public UInt16 BitsPerSample;

        /// <summary>
        /// "data" (0x61746164)
        /// </summary>
        public UInt32 Subchunk2Id;

        /// <summary>
        /// numSamples * numChannels * bitsPerSample/8 (байт в области данных)
        /// </summary>
        public UInt32 Subchunk2Size;

        public WavHeader() { }

        public WavHeader(WavHeader wavHeader)
        {
            ChunkId = wavHeader.ChunkId;
            ChunkSize = wavHeader.ChunkSize;
            Format = wavHeader.Format;
            Subchunk1Id = wavHeader.Subchunk1Id;
            Subchunk1Size = wavHeader.Subchunk1Size;
            AudioFormat = wavHeader.AudioFormat;
            NumChannels = wavHeader.NumChannels;
            SampleRate = wavHeader.SampleRate;
            ByteRate = wavHeader.ByteRate;
            BlockAlign = wavHeader.BlockAlign;
            BitsPerSample = wavHeader.BitsPerSample;
            Subchunk2Id = wavHeader.Subchunk2Id;
            Subchunk2Size = wavHeader.Subchunk2Size;
        }
    }

    /// <summary>
    /// Звуковой файл
    /// </summary>
    public class WavFile : IDisposable
    {
        public WavHeader Header;
        public int[][] Data;
        MemoryStream stream;

        /// <summary>
        /// Возвращает файл в виде потока; StreamFlush() для заполнения
        /// </summary>
        public Stream Stream { get { return (Stream)stream; } }

        /// <summary>
        /// Продолжительность записи
        /// </summary>
        public TimeSpan TimeDuration
        {
            get
            {
                //Значение в секундах
                double seconds = NumSamples / Header.SampleRate;
                return TimeSpan.FromSeconds(seconds);
            }
        }

        /// <summary>
        /// Количество сэмплов
        /// </summary>
        public long NumSamples { get; set; }

        public WavFile() { }
        public WavFile(string Path) { Load(Path); }
        public WavFile(WavFile Copy)
        {
            Header = new WavHeader(Copy.Header);
        }

        /// <summary>
        /// Скидывает все данные из объекта класса в Stream и обновляет данные о воспроизведении
        /// </summary>
        public void StreamFlush()
        {
            if (stream != null) stream.Dispose();
            stream = new MemoryStream();

            /****************** Запись заголовка ***********************/

            int length = Marshal.SizeOf(Header.GetType());
            IntPtr hdr = Marshal.AllocHGlobal(length);
            byte[] buffer = new byte[length];
            Marshal.StructureToPtr(Header, hdr, true);
            Marshal.Copy(hdr, buffer, 0, length);
            stream.Write(buffer, 0, length);

            /****************** Запись данных ******************/

            if (Header.BitsPerSample > 32) 
                throw new FormatException("Format is not supported: too large bps");
            if (Header.AudioFormat != 1) 
                throw new FormatException("Format is not supported: encoding format is not PCM");
            
            long dataLen = NumSamples;
            BinaryWriter bw = new BinaryWriter(stream);

            switch (Header.BitsPerSample)
            {
                case 8:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) bw.Write((byte)Data[i][j]);
                    }
                    break;

                case 16:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) bw.Write((Int16)Data[i][j]);
                    }
                    break;

                default:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) bw.Write(Data[j][i]);
                    }
                    break;
            }
            
            bw.Flush();
        }

        /// <summary>
        /// Сохраняет Wav-файл по указанному пути
        /// </summary>
        /// <param name="Path"></param>
        public void Save(string Path)
        {
            StreamFlush();
            FileStream fstream = new FileStream(Path, FileMode.Create, FileAccess.Write);
            stream.WriteTo(fstream);
            fstream.Close();
        }

        /// <summary>
        /// Загружает данные из файла
        /// </summary>
        /// <param name="path"></param>
        public void Load(Stream stream)
        {
            /******************************** Чтение заголовка *******************************/

            Header = new WavHeader();
            int hsize = Marshal.SizeOf(Header.GetType());
            byte[] buffer = new byte[hsize];
            stream.Read(buffer, 0, hsize);
            GCHandle handle = GCHandle.Alloc(buffer, GCHandleType.Pinned);
            Header = (WavHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), Header.GetType());
            handle.Free();

            /******************************** Чтение данных *******************************/

            if (Header.BitsPerSample > 32)
                throw new FormatException("Format is not supported: too large bps");
            if (Header.AudioFormat != 1)
                throw new FormatException("Format is not supported: encoding format is not PCM");
            
            NumSamples = (long)((stream.Length - hsize) * 8.0 / Header.BitsPerSample / Header.NumChannels);
            BinaryReader br = new BinaryReader(stream);
            Data = new int[Header.NumChannels][];
            for (int i = 0; i < Header.NumChannels; i++) Data[i] = new int[NumSamples];

            switch (Header.BitsPerSample)
            {
                case 8:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) Data[i][j] = br.ReadByte();
                    }
                    break;

                case 16:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) Data[i][j] = br.ReadInt16();
                    }
                    break;

                case 24: 
                    throw new NotImplementedException("24 bytes per second not supported");

                default:
                    for (int i = 0; i < Header.NumChannels; i++)
                    {
                        for (int j = 0; j < NumSamples; j++) Data[i][j] = br.ReadInt32();
                    }
                    break;
            }

            stream.Close();
            return;
        }

        /// <summary>
        /// Загружает данные из файла
        /// </summary>
        /// <param name="path"></param>
        public void Load(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                Load(stream);
            }
        }

        /// <summary>
        /// Закрывает привязанные потоки
        /// </summary>
        public void Dispose()
        {
            if (stream != null)
            {
                stream.Close();
                stream.Dispose();
            }
        }
    }
}
