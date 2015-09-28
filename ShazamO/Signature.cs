using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShazamO
{
    /// <summary>
    /// Описывает файл с сигнатурой звукового файла
    /// </summary>
    [Serializable]
    public class Signature : IComparable<string>, IComparable<Signature>
    {
        /// <summary>
        /// Сигнатура в виде массива байт
        /// </summary>
        public uint[] Data;

        /// <summary>
        /// Заголовочный файл для родительского звукового файла
        /// </summary>
        public WavHeader BaseFileInfo { get; set; }

        /// <summary>
        /// Путь до родительского звукового файла
        /// </summary>
        public string BaseFilePath { get; set; }

        /// <summary>
        /// Сигнатур в секунду
        /// </summary>
        public int FreqRate { get; set; }

        /// <summary>
        /// Оконная функция, использованная при построении
        /// </summary>
        public WindowFunction WindowFunction { get; set; }

        /// <summary>
        /// Установить в true для того, чтобы сигнатуры строились параллельно
        /// </summary>
        public static bool Parallel = false;

        /// <summary>
        /// Вызывается для обратной связи процедуры построения спектрограммы. 
        /// Принимает float - прогресс построения в процентах. 
        /// Вернуть false для отмены построения или ture для продолжения.
        /// </summary>
        public delegate bool OnSonoBuildHandler(string Description, float ProgressPercent);

        /// <summary>
        /// Создает новый экземпляр Signature из Wav файла по строке адреса
        /// </summary>
        public static async Task<Signature> GenerateFromWav(
            string FilePath,
            int FreqRate,
            WindowFunction WndFunc,
            OnSonoBuildHandler OnSonoBuild,
            int ResampleTo = 0)
        {
            Signature sign = new Signature();
            sign.WindowFunction = WndFunc;
            sign.BaseFilePath = FilePath;
            sign.FreqRate = FreqRate;
            using(var stream = new FileStream(FilePath, FileMode.Open))
            {
                using (var reader = new NAudio.Wave.WaveFileReader(stream))
                {
                    NAudio.Wave.WaveFormat format;
                    if (ResampleTo <= 0)
                    {
                        format = reader.WaveFormat;
                        sign.BaseFileInfo = NAudioWrapper.NSampleData.FormatToHeader(format, (int)stream.Length);

                        byte[] data = new byte[reader.Length];

                        if (OnSonoBuild != null) OnSonoBuild("Read file...", -1);
                        int received = await new Task<int>(() => reader.Read(data, 0, data.Length));

                        sign.Data = await GetSign(format, data, received, FreqRate, WndFunc, OnSonoBuild);
                    }
                    else
                    {
                        format = new NAudio.Wave.WaveFormat(ResampleTo, 16, 1);
                        sign.BaseFileInfo = NAudioWrapper.NSampleData.FormatToHeader(format, (int)reader.Length);
                        double coeff = (double)format.SampleRate / reader.WaveFormat.SampleRate;
                        coeff *= (double)format.BitsPerSample / reader.WaveFormat.BitsPerSample;
                        coeff *= (double)format.Channels / reader.WaveFormat.Channels;
                        byte[] data = new byte[(int)(reader.Length * coeff)];
                        reader.Seek(0, SeekOrigin.Begin);

                        using (var resampler = new NAudio.Wave.MediaFoundationResampler(reader, format))
                        {
                            resampler.ResamplerQuality = 60;
                            if (OnSonoBuild != null) OnSonoBuild("Resample file...", -1);

                            int received = resampler.Read(data, 0, data.Length);

                            sign.Data = await GetSign(format, data, received, FreqRate, WndFunc, OnSonoBuild);
                        }

                    }
                }
            }
            return sign;
        }

        public static async Task<Signature> TryGenerateFromMp3(
            string FilePath,
            int FreqRate,
            WindowFunction WndFunc,
            OnSonoBuildHandler OnSonoBuild,
            int ResampleTo = 0)
        {
            try
            {
                return await GenerateFromMp3(FilePath, FreqRate, WndFunc, OnSonoBuild, ResampleTo);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        /// <summary>
        /// Создает новый экземпляр Signature из Mp3 файла по строке адреса
        /// </summary>
        public static async Task<Signature> GenerateFromMp3(
            string FilePath,
            int FreqRate,
            WindowFunction WndFunc,
            OnSonoBuildHandler OnSonoBuild,
            int ResampleTo = 0)
        {
            Signature sign = new Signature();
            sign.WindowFunction = WndFunc;
            sign.BaseFilePath = FilePath;
            sign.FreqRate = FreqRate;

            using (var stream = new FileStream(FilePath, FileMode.Open))
            {
                using (var reader = new NAudio.Wave.Mp3FileReader(stream))
                {
                    NAudio.Wave.WaveFormat format;
                    if (ResampleTo <= 0)
                    {
                        format = reader.WaveFormat;
                        sign.BaseFileInfo = NAudioWrapper.NSampleData.FormatToHeader(format, (int)stream.Length);

                        byte[] data = new byte[reader.Length];

                        if (OnSonoBuild != null) OnSonoBuild("Read file...", -1);
                        int received = await new Task<int>(() => reader.Read(data, 0, data.Length));

                        sign.Data = await GetSign(format, data, received, FreqRate, WndFunc, OnSonoBuild);
                    }
                    else
                    {
                        format = new NAudio.Wave.WaveFormat(ResampleTo, 16, 1);
                        sign.BaseFileInfo = NAudioWrapper.NSampleData.FormatToHeader(format, (int)reader.Length);
                        double coeff = (double)format.SampleRate / reader.WaveFormat.SampleRate;
                        coeff *= (double)format.BitsPerSample / reader.WaveFormat.BitsPerSample;
                        coeff *= (double)format.Channels / reader.WaveFormat.Channels;
                        byte[] data = new byte[(int)(reader.Length * coeff)];
                        reader.Seek(0, SeekOrigin.Begin);

                        using (var resampler = new NAudio.Wave.MediaFoundationResampler(reader, format))
                        {
                            resampler.ResamplerQuality = 60;
                            if (OnSonoBuild != null) OnSonoBuild("Resample file...", -1);

                            int received = resampler.Read(data, 0, data.Length);

                            sign.Data = await GetSign(format, data, received, FreqRate, WndFunc, OnSonoBuild);
                        }

                    }
                }
            }
            return sign;
        }

        /// <summary>
        /// Создает данные сигнатуры
        /// </summary>
        private static async Task<uint[]> GetSign(
            NAudio.Wave.WaveFormat Format,
            byte[] Data,
            int DataLength,
            int FreqRate,
            WindowFunction WndFunc,
            OnSonoBuildHandler OnSonoBuild)
        {
            int dstep = Format.SampleRate / FreqRate;
            int depth = 1 << ((int)Math.Log(dstep, 2) + 1);
            if (depth < 2 * FreqRate) throw new Exception("File sample rate is too small");

            int bps = (Format.Channels * Format.BitsPerSample / 8);
            int sampSize = dstep * 1024;
            int wndSize = sampSize * bps;

            int position = 0, oldDataSize = 0;

            List<uint[]> prints = new List<uint[]>();
            SonoBuilder sonoBuilder;
            float[][] sono;
            Recognition.Initialize();

            float progressStep = (float)wndSize / DataLength, progressCur = 0;

            wndSize = (DataLength > wndSize + position ? wndSize : DataLength - position);
            int[] data = NAudioWrapper.NSampleData.ReadFromPcm(
                                Data, Format.Channels, Format.BitsPerSample, position, wndSize);

            position += data.Length * bps;

            while (position < DataLength)                                                   // [1]
            {
                if (OnSonoBuild != null) OnSonoBuild("Building sono", 100 * progressCur);
                progressCur += progressStep;

                wndSize = (DataLength > wndSize + position ? wndSize : DataLength - position);
                int[] data0 = NAudioWrapper.NSampleData.ReadFromPcm(
                                    Data, Format.Channels, Format.BitsPerSample,
                                    position, wndSize);
                oldDataSize = data.Length;

                if (data0.Length <= 0) break;

                data = data.Concat(data0).ToArray();
                position += data0.Length * bps;

                sonoBuilder = new SonoBuilder(data, Format.SampleRate);
                sono = await RunBuildSono(sonoBuilder, depth, dstep, oldDataSize + dstep, WndFunc);
                prints.Add(Recognition.GetPrintsFromSono(sono));
                data = data0;
            }
            if (OnSonoBuild != null) OnSonoBuild("Building sono", 100);

            int dLength = oldDataSize - position + DataLength;
            sonoBuilder = new SonoBuilder(data, Format.SampleRate);
            sono = await RunBuildSono(sonoBuilder, depth, dstep, data.Length, WndFunc);

            if (sono.Length > 0) prints.Add(Recognition.GetPrintsFromSono(sono));

            IEnumerable<uint> print = prints[0];
            for (int i = 1; i < prints.Count; i++) print = print.Concat(prints[i]);

            if (OnSonoBuild != null) OnSonoBuild("Ready", -1);
            return print.ToArray();
        }

        /// <summary>
        /// Обработчик запуска построителя сонограммы
        /// </summary>
        /// <param name="depth">Глубина построения</param>
        /// <param name="step">Шаг построения, в сэмплах</param>
        /// <param name="size">Размер данных в сэмплах, участвующих в построении</param>
        /// <param name="WndFunc">Функция сглаживания</param>
        private static async Task<float[][]> RunBuildSono(
                        SonoBuilder sonoBuilder,
                        int depth,
                        int step,
                        int size,
                        WindowFunction WndFunc)
        {
            return await Task.Run(() =>
                {
                    if (WndFunc != null)
                    {
                        if (Parallel) return sonoBuilder.CreateSonoParallel(depth, step, 0, size, WndFunc);
                        else return sonoBuilder.CreateSono(depth, step, 0, size, WndFunc);
                    }
                    else
                    {
                        if (Parallel) return sonoBuilder.CreateSonoParallel(
                                                depth, step, 0, size, WindowFunction.RectangleWindow);
                        return sonoBuilder.CreateSono(depth, step, 0, size);
                    }
                });
        }

        /// <summary>
        /// Формирует файл *.sgn путем сериализации этого класса
        /// </summary>
        public void Save(string Path)
        {
            using (FileStream fstream = new FileStream(Path, FileMode.OpenOrCreate))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                bformatter.Serialize(fstream, this);
            }
        }

        /// <summary>
        /// Загружает экземпляр класса Signature из файла *.sgn, созданного при помощи Signature.Save(Path)
        /// </summary>
        public static Signature LoadFromFile(string Url)
        {
            Signature sign = null;
            using (FileStream fstream = new FileStream(Url, FileMode.Open))
            {
                BinaryFormatter bformatter = new BinaryFormatter();
                sign = bformatter.Deserialize(fstream) as Signature;
            }
            return sign;
        }

        /// <summary>
        /// Переводит позицию в массиве сигнатур во временной интервал с начала текущего файла
        /// </summary>
        public TimeSpan ToTimeSpan(float Position)
        {
            return TimeSpan.FromSeconds(Position / FreqRate);
        }

        /// <summary>
        /// Возвращает размер файла
        /// </summary>
        public TimeSpan ToTimeSpan()
        {
            return TimeSpan.FromSeconds(Data.Length / FreqRate);
        }

        public Congruence[] CompareTo(Signature Sign, float DeltaSigm)
        {
                // this smaller than Sign
            if (Data.Length < Sign.Data.Length) return null;

            float[] cmp = Recognition.CompareHamming(Data, Sign.Data);

            Probability prob = new Probability(cmp);
            float sigm = (float)Math.Sqrt(prob.D);
            float ds = prob.M - DeltaSigm * sigm;       // M - X * sigma

            List<Congruence> congr = new List<Congruence>();
            for (int j = 0; j < cmp.Length; j++)
            {
                if (ds >= cmp[j])
                {
                    PointF min = Recognition.Minimum(cmp, j - 2, 5);
                    congr.Add(new Congruence(min, Sign, (prob.M - min.Y) / sigm));
                    j += 2;
                }
            }
            
            if (congr.Count > 0) return congr.ToArray();
            else return null;
        }

        /// <summary>
        /// Класс для проведения сравнения по имени
        /// </summary>
        public class ComparerByName : IEqualityComparer<Signature>
        {
            public bool Equals(Signature x, Signature y)
            {
                return x.ToString().Equals(y.ToString());
            }

            public int GetHashCode(Signature obj)
            {
                return obj.ToString().GetHashCode();
            }
        }

        /// <summary>
        /// Возвращает краткое имя файла, по которому строилась сигнатура
        /// </summary>
        public override string ToString()
        {
            return FileParser.GetFileName(BaseFilePath);
        }

        public int CompareTo(string Name)
        {
            return this.ToString().CompareTo(Name);
        }

        public int CompareTo(Signature other)
        {
            return this.ToString().CompareTo(other.ToString());
        }
    }
}
