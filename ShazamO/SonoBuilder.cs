using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShazamO
{
    public class SonoBuilder
    {
        /// <summary>
        /// Данные звукового ряда, представляются в виде одного канала аудио
        /// </summary>
        public int[] Data { get; set; }

        /// <summary>
        /// Частота дискретизации входного сигнала
        /// </summary>
        public int SampleRate { get; set; }


        public delegate bool OnSonoProgressHandler(int Current, int All);
        /// <summary>
        /// Возникает при изменении прогресса построения спектрограммы
        /// </summary>
        public event OnSonoProgressHandler OnSonoProgress;


        public SonoBuilder() { }

        public SonoBuilder(int[] Data, int SampleRate)
        {
            this.Data = Data;
            this.SampleRate = SampleRate;
        }

        /// <summary>
        /// Изменить сэмплинг данных на новый. Лучше использовать внешние средства
        /// </summary>
        public SonoBuilder Resample(int NewSampleRate)
        {
            SonoBuilder result = new SonoBuilder();
            float coeff = ((float)NewSampleRate) / SampleRate;
            int[] dataResult = new int[(int)(coeff * Data.Length)];
            Resample(ref dataResult);
            result.Data = dataResult;
            result.SampleRate = NewSampleRate;
            return result;
        }
        
        /// <summary>
        /// Отображает спектрограмму на Bitmap в оттенках серого. 
        /// Предварительно спектрограмма должна быть приведена при помощи следующего кода:
        /// <code>
        /// SetFloatMax(sono);
        /// Log(ref sono, midLvl); // midLvl обычно 0..1, вызов опционален
        /// Normalize(ref sono, 255);
        /// </code>
        /// </summary>
        public Bitmap FillBmp(float[][] Sono)
        {
            Bitmap bmp = new Bitmap(Sono.Length, Sono[0].Length);
            int hei = Sono[0].Length;
            for (int i = 0; i < Sono.Length; i++)
            {
                for (int j = 0; j < hei; j++)
                {
                    int val = Sono[i][j] > 255 ? 255 : (int)Sono[i][j];
                    bmp.SetPixel(i, hei - j - 1, Color.FromArgb(val, val, val));
                }
            }
            return bmp;
        }

        /// <summary>
        /// Изменить сэмплинг данных на новый. Лучше использовать внешние средства
        /// </summary>
        public void Resample(ref int[] Result)
        {
            double coeff = (Data.Length * 1.0) / Result.Length;
            for (int i = 0; i < Result.Length; i++)
            {
                Result[i] = Avg((int)(i * coeff), (int)coeff);
            }
        }

        /// <summary>
        /// Считает среднее значение для исходного массива this.Data для диапазона [Begin, Begin + Length]
        /// </summary>
        private int Avg(int Begin, double Length)
        {
            double shift = Begin + Length;
            if (shift > Data.Length)
            {
                shift = Data.Length;
                Length = shift - Begin;
            }
            float retVal = 0;
            for (int i = Begin; i < shift; i++) retVal += Data[i];
            return (int)(retVal / Length);
        }
        
        /// <summary>
        /// Считает среднее значение для исходного массива Data для диапазона [Begin, Begin + Length]
        /// </summary>
        private float Avg(float[] Data, int Begin, float Length)
        {
            float shift = Begin + Length;
            if (shift > Data.Length)
            {
                shift = Data.Length;
                Length = shift - Begin;
            }
            float retVal = 0;
            for (int i = Begin; i < shift; i++) retVal += Data[i];
            return retVal / Length;
        }

        /// <summary>
        /// Лоает порядок массива!
        /// </summary>
        private float Median(float[] Data, int Begin, float Length, float MedianLvl)
        {
            if (Begin + Length > Data.Length) Length = Data.Length - Begin;
            Array.Sort(Data, Begin, (int)Length);
            return Data[Begin + (int)(Length * MedianLvl)];
        }

        private float floatMax = 0;

        /// <summary>
        /// Приводит сонограмму к максимуму в nMax
        /// </summary>
        public void Normalize(ref float[][] Sono, float nMax)
        {
            float coeff = nMax / floatMax;
            for (int i = 0; i < Sono.Length; i++)
            {
                for (int j = 0; j < Sono[i].Length; j++)
                {
                    Sono[i][j] *= coeff;
                }
            }
        }

        /// <summary>
        /// Устанавливает внутреннюю переменную floatMax
        /// </summary>
        public void SetFloatMax(float[][] Sono)
        {
            for (int i = 0; i < Sono.Length; i++)
            {
                for (int j = 0; j < Sono[0].Length; j++)
                {
                    if (floatMax < Sono[i][j]) floatMax = Sono[i][j];
                }
            }
        }

        /// <summary>
        /// Сжимает сонограмму через поиск среднего в глубину
        /// </summary>
        public float[][] CollapseAvg(float[][] Sono, int NewDepth)
        {
            float[][] result = new float[Sono.Length][];
            float delta = ((float)Sono[0].Length) / NewDepth;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new float[NewDepth];
                for (int j = 0; j < NewDepth; j++)
                {
                    result[i][j] = Avg(Sono[i], (int)(j * delta), delta);
                }
            }
            return result;
        }

        /// <summary>
        /// Сжимает сонограмму через поиск среднего в глубину
        /// </summary>
        public float[][] CollapseMedian(float[][] Sono, int NewDepth, float MedianLvl)
        {
            float[][] result = new float[Sono.Length][];
            float delta = ((float)Sono[0].Length) / NewDepth;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new float[NewDepth];
                for (int j = 0; j < NewDepth; j++)
                {
                    result[i][j] = Median(Sono[i], (int)(j * delta), delta, MedianLvl);
                }
            }
            return result;
        }

        /// <summary>
        /// Сжимает сонограмму через поиск максимального значения
        /// </summary>
        public float[][] CollapseMax(float[][] Sono, int NewDepth)
        {
            float[][] result = new float[Sono.Length][];
            float delta = ((float)Sono[0].Length) / NewDepth;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new float[NewDepth];
                for (int j = 0; j < NewDepth; j++)
                {
                    int begin = (int)(j * delta);
                    float val = Sono[i][begin];
                    for (int k = begin; k < begin - delta; k++) if (val < Sono[i][k]) val = Sono[i][k];
                    result[i][j] = val;
                }
            }
            return result;
        }

        // Изменяет floatMax
        public void Log(ref float[][] Sono, float MidLvl)
        {
            floatMax = (float)Math.Log(floatMax);
            float min = floatMax * MidLvl;
            floatMax += min;

            for (int i = 0; i < Sono.Length; i++)
            {
                for (int j = 0; j < Sono[i].Length; j++)
                {
                    float value = (float)Math.Log(Sono[i][j]) + min;
                    Sono[i][j] = (value < 0 ? 0 : value);
                }
            }
        }

        /// <summary>
        /// Создает сонограмму заданной глубины с заданным шагом. 
        /// Размер выходного массива: { x = Data.Length / Step; y = Depth / 2; }.
        /// Обнуляет floatMax.
        /// </summary>
        public float[][] CreateSono(int Depth, int Step)
        {
            return CreateSono(Depth, Step, 0, Data.Length - Depth);
        }

        /// <summary>
        /// Создает сонограмму заданной глубины с заданным шагом, смещением и размером.
        /// Размер считается в "полноценных" спектрах, то есть не дополненных нулями.
        /// Если спектрограмму такого размера создать невозможно, возращается null.
        /// Размер выходного массива: { x = Length / Step; y = Depth / 2; }.
        /// Обнуляет floatMax.
        /// </summary>
        public float[][] CreateSono(int Depth, int Step, int Offset, int Length)
        {
            if (Length + Offset + Depth > Data.Length)
            {
                Length = Data.Length - Offset - Depth;
            }
            floatMax = 0;
            FFT.Initialize();
            int steps = Length / Step;
            float[][] ret = new float[steps][];
            //List<float[]> ret = new List<float[]>();
            float dState = steps * 0.01f, nState = 0;

            for (int i = 0; i < steps; i++)
            {
                float[] fftFloat = new float[Depth / 2];
                Complex[] result = FFT.GenFFT(Data, Offset + i * Step, Depth);
                if (result == null) break;

                for (int j = 0; j < fftFloat.Length; j++)
                {
                    float abs = result[j].Amplitude;
                    if (floatMax < abs) floatMax = abs;
                    fftFloat[j] = abs;
                }
                if (nState < i)
                {
                    nState += dState;
                    if (OnSonoProgress != null) OnSonoProgress(i, steps);
                }
                ret[i] = fftFloat;
                //ret.Add(fftFloat);
            }
            return ret.ToArray();
        }

        /// <summary>
        /// Создает сонограмму заданной глубины с заданным шагом. 
        /// Размер выходного массива: { x = Data.Length / Step; y = Depth / 2; } 
        /// Обнуляет floatMax.
        /// </summary>
        public float[][] CreateSono(int Depth, int Step, WindowFunction WndFunc)
        {
            return CreateSono(Depth, Step, 0, Data.Length - Depth, WndFunc);
        }

        /// <summary>
        /// Создает сонограмму заданной глубины с заданным шагом, смещением и размером.
        /// Размер считается в "полноценных" спектрах, то есть не дополненных нулями.
        /// Если спектрограмму такого размера создать невозможно, возращается null.
        /// Размер выходного массива: { x = Length / Step; y = Depth / 2; } 
        /// </summary>
        public float[][] CreateSonoParallel(int Depth, int Step, int Offset, int Length, WindowFunction WndFunc)
        {
            if (Length + Offset + Depth > Data.Length)
            {
                Length = Data.Length - Offset - Depth;
            }
            FFT.Initialize();
            int steps = Length / Step;
            float[][] ret = new float[steps][];

            int dState = steps / 100;
            if (dState < 1) dState = 1;
            float nDepth = 1.0f / Depth;

            float[] nWnd = new float[Depth];
            for (int i = 0; i < Depth; i++) nWnd[i] = WndFunc.Act(i * nDepth);
            int k = 0;

            Parallel.For(0, steps, (i, n) =>
            {
                float[] data = new float[Depth];
                for (int j = 0, j0 = i * Step; j < Depth; j++) data[j] = nWnd[j] * Data[Offset + j0++];
                Complex[] result = FFT.GenFFT(data, 0, Depth);
                if (result == null) return;
                float[] fftFloat = new float[Depth / 2];
                for (int j = 0; j < fftFloat.Length; j++) fftFloat[j] = result[j].Amplitude;
                if (i % dState == 0) if (OnSonoProgress != null) OnSonoProgress(k++, steps);
                ret[i] = fftFloat;
            });

            return ret;
        }

        /// <summary>
        /// Создает сонограмму заданной глубины с заданным шагом, смещением и размером.
        /// Размер считается в "полноценных" спектрах, то есть не дополненных нулями.
        /// Если спектрограмму такого размера создать невозможно, возращается null.
        /// Размер выходного массива: { x = Length / Step; y = Depth / 2; } 
        /// </summary>
        public float[][] CreateSono(int Depth, int Step, int Offset, int Length, WindowFunction WndFunc)
        {
            if (Length + Offset + Depth > Data.Length)
            {
                Length = Data.Length - Offset - Depth;
            }
            floatMax = 0;
            FFT.Initialize();
            int steps = Length / Step;
            float[][] ret = new float[steps][];
            //List<float[]> ret = new List<float[]>();
            float dState = steps * 0.01f, nState = 0;
            float[] data = new float[Depth];
            float nDepth = 1.0f / Depth;

            float[] nWnd = new float[Depth];
            for (int i = 0; i < Depth; i++) nWnd[i] = WndFunc.Act(i * nDepth);

            Complex[] fftCpx = new Complex[Depth];
            for (int i = 0; i < steps; i++)
            {
                for (int j = 0, j0 = i * Step; j < Depth; j++)
                {
                    data[j] = nWnd[j] * Data[Offset + j0++];
                }

                float[] fftFloat = new float[Depth / 2];
                Complex[] result = FFT.GenFFT(data, 0, Depth);

                if (result == null) break;

                for (int j = 0; j < fftFloat.Length; j++)
                {
                    float abs = result[j].Amplitude;
                    if (floatMax < abs) floatMax = abs;
                    fftFloat[j] = abs;
                }
                if (nState < i)
                {
                    nState += dState;
                    if (OnSonoProgress != null) OnSonoProgress(i, steps);
                }
                ret[i] = fftFloat;
                //ret.Add(fftFloat);
            }
            return ret.ToArray();
        }

    }
}
