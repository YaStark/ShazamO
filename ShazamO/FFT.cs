using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace ShazamO
{
    /// <summary>
    /// Быстрое преобразование Фурье
    /// </summary>
    public class FFT
    {
        /// <summary>
        /// Инииализирует статические вызовы построителя
        /// </summary>
        public static void Initialize()
        {
            if (FFT.rev256 == null) GenerateRev256();
        }

        #region Service

        /// <summary>
        /// Таблица обратных битовых перестановок от 0 до 255
        /// </summary>
        private static byte[] rev256 = null;

        /// <summary>
        /// Генерирует таблицу обратных битовых перестановок rev256
        /// </summary>
        private static void GenerateRev256()
        {
            rev256 = new byte[256];
            int[] fwd = new int[8] { 1, 2, 4, 8, 16, 32, 64, 128 };
            for (int i = 0; i < 256; i++)
            {
                int R = 0, L = 0;
                for (byte j = 0; j < 4; j++)
                {
                    L |= (fwd[j] & i) << (7 - j * 2);
                    R |= (fwd[7 - j] & i) >> (7 - j * 2);
                }
                rev256[i] = (byte)(L | R);
            }
        }

        /// <summary>
        /// Битовая перестановка числа [Number] по основанию [Size] бит
        /// </summary>
        /// <param name="Number"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        private static uint Rev(Int32 Number, int Size)
        {
            byte[] R = new byte[4];
            byte[] N = BitConverter.GetBytes(Number);
            for (int i = 0; i < 4; i++)
            {
                R[i] = rev256[N[3 - i]];
                //R[i] = rev256[Marshal.ReadByte(Number, 3 - i)];
            }
            // (uint)Marshal.ReadInt32(R, 0);
            return BitConverter.ToUInt32(R, 0) >> (32 - Size);
        }
        
        /// <summary>
        /// Вычисление ФФТ с переставленным массивом
        /// </summary>
        private static void _FFT(ref Complex[] revData, bool Reverse)
        {
            int Size = revData.Length;
            Complex W = new Complex();
            double pi2 = -Math.PI * 2;
            if (Reverse) pi2 = -pi2;
            for (int N = 2; N <= Size; N <<= 1)
            {
                double dn = pi2 / N;
                for (int i = 0; i < N / 2; i++)
                {
                    double x = i * dn;
                    W = new Complex((float)Math.Cos(x), (float)Math.Sin(x));
                    //W = Complex.Exp(c01 * (pi2 * i / N));
                    for (int j = i, s = i + N / 2; j < Size; j += N, s += N)
                    {
                        Complex t = W * revData[s];
                        revData[s] = revData[j] - t;
                        revData[j] = revData[j] + t;
                    }
                }
            }
            if (Reverse)
            {
                for (int i = 0; i < Size; i++)
                {
                    revData[i].re /= Size;
                    revData[i].im /= Size;
                }
            }
        }

        #endregion

        /// <summary>
        /// Обратное преобразование Фурье
        /// </summary>
        public static Complex[] RevFFT(Complex[] Data)
        {
            /****************** Перестановка ********************/
            Complex[] nData = new Complex[Data.Length];
            int bSize = (int)Math.Log(Data.Length, 2);
            for (int i = 0; i < Data.Length; i++)
            {
                uint revN = Rev(i, bSize);
                nData[revN] = Data[i];
            }

            _FFT(ref nData, true);
            return nData;
        }

        /// <summary>
        /// Прямое преобразование Фурье
        /// </summary>
        public static Complex[] GenFFT(long[] Data, int Offset, int Size)
        {
            if (Offset + Size > Data.Length) return null;

            Complex[] nData = new Complex[Size];
            int bSize = (int)Math.Log(Size, 2);
            for (int i = 0; i < Size; i++)
            {
                nData[Rev(i, bSize)] = new Complex(Data[i + Offset]);
            }
            _FFT(ref nData, false);
            return nData;
        }

        /// <summary>
        /// Прямое преобразование Фурье
        /// </summary>
        public static Complex[] GenFFT(int[] Data, int Offset, int Size)
        {
            if (Offset + Size > Data.Length) return null;

            Complex[] nData = new Complex[Size];
            int bSize = (int)Math.Log(Size, 2);
            for (int i = 0; i < Size; i++)
            {
                nData[Rev(i, bSize)] = new Complex(Data[i + Offset]);
            }
            _FFT(ref nData, false);
            return nData;
        }

        /// <summary>
        /// Прямое преобразование Фурье
        /// </summary>
        public static Complex[] GenFFT(float[] Data, int Offset, int Size)
        {
            if (Offset + Size > Data.Length) return null;

            Complex[] nData = new Complex[Size];
            int bSize = (int)Math.Log(Size, 2);
            for (int i = 0; i < Size; i++)
            {
                nData[Rev(i, bSize)] = new Complex(Data[i + Offset]);
            }
            _FFT(ref nData, false);
            return nData;
        }
        
        /// <summary>
        /// Прямое преобразование Фурье
        /// </summary>
        public static Complex[] GenFFT(double[] Data, int Offset, int Size)
        {
            if (Offset + Size > Data.Length) return null;

            Complex[] nData = new Complex[Size];
            int bSize = (int)Math.Log(Size, 2);
            for (int i = 0; i < Size; i++)
            {
                nData[Rev(i, bSize)] = new Complex((float)Data[i + Offset]);
            }
            _FFT(ref nData, false);
            return nData;
        }


        /// <summary>
        /// Прямое преобразование Фурье
        /// </summary>
        public static Complex[] GenFFT(Complex[] Data, int Offset, int Size)
        {
            Complex[] nData = new Complex[Size];
            int bSize = (int)Math.Log(Size, 2);
            for (int i = 0; i < Size; i++)
            {
                nData[Rev(i, bSize)] = Data[i + Offset];
            }
            _FFT(ref nData, false);
            return nData;
        }
    }


    [Serializable]
    public class WindowFunction
    {
        public delegate float WFunction(float n);
        
        public WFunction Act;
        
        string caption = "func";
        
        public WindowFunction(string Caption, WFunction function)
        {
            Act = function;
            caption = Caption;
        }

        public override string ToString() { return caption; }

        public static WindowFunction RectangleWindow = new WindowFunction(
            "Прямоугольное окно",
            delegate(float n) { return 1; });

        public static WindowFunction SinusWindow = new WindowFunction(
            "Синус-окно",
            delegate(float n) { return (float)Math.Sin(Math.PI * n); });

        public static WindowFunction LanczosWindow = new WindowFunction(
            "Окно Ланцоша",
            delegate(float n)
            {
                float x = (float)Math.PI * (2 * n - 1);
                return (float)Math.Sin(x) / x;
            });

        public static WindowFunction BartlettWindow = new WindowFunction(
            "Окно Барлетта (треугольное окно)",
            delegate(float n) { return 1.0f - Math.Abs(2 * n - 1); });

        public static WindowFunction HannWindow = new WindowFunction(
            "Окно Ханна",
            delegate(float n) { return 0.5f - 0.5f * (float)Math.Cos(2 * Math.PI * n); });

        public static WindowFunction BartlettHannWindow = new WindowFunction(
            "Окно Барлетта-Ханна",
            delegate(float n)
            {
                return 0.62f - 0.48f * Math.Abs(n - 0.5f) - 0.38f * (float)Math.Cos(2 * Math.PI * n);
            });

        public static WindowFunction HammingWindow = new WindowFunction(
            "Окно Хемминга",
            delegate(float n) { return 0.54f - 0.46f * (float)Math.Cos(2 * Math.PI * n); });

        public static WindowFunction BlackmanWindow = new WindowFunction(
            "Окно Блэкмана",
            delegate(float n)
            {
                double P = Math.PI * n;
                return 0.42f - 0.5f * (float)Math.Cos(2 * P) + 0.08f * (float)Math.Cos(4 * P);
            });

        public static WindowFunction BlackmanHarrisWindow = new WindowFunction(
            "Окно Блэкмана-Харриса",
            delegate(float n)
            {
                const float a0 = 0.35875f, a1 = 0.48829f, a2 = 0.14128f, a3 = 0.01168f;
                double P = Math.PI * n;
                return a0 - a1 * (float)Math.Cos(2 * P) + a2 * (float)Math.Cos(4 * P) - a3 * (float)Math.Cos(6 * P);
            });

        public static WindowFunction NuttallWindow = new WindowFunction(
            "Окно Наталла",
            delegate(float n)
            {
                const float a0 = 0.355768f, a1 = 0.487396f, a2 = 0.144232f, a3 = 0.012604f;
                double P = Math.PI * n;
                return (float)(a0 - a1 * Math.Cos(2 * P) + a2 * Math.Cos(4 * P) - a3 * Math.Cos(6 * P));
            });

        public static WindowFunction BlackmanNuttallWindow = new WindowFunction(
            "Окно Блэкмана-Наталла",
            delegate(float n)
            {
                const float a0 = 0.3635819f, a1 = 0.4891775f, a2 = 0.1365995f, a3 = 0.0106411f;
                double P = Math.PI * n;
                return (float)(a0 - a1 * Math.Cos(2 * P) + a2 * Math.Cos(4 * P) - a3 * Math.Cos(6 * P));
            });

        public static WindowFunction FlatTopWindow = new WindowFunction(
            "Окно с плоской вершиной",
            delegate(float n)
            {
                const float a0 = 1, a1 = 1.93f, a2 = 1.29f, a3 = 0.388f, a4 = 0.032f;
                double P = Math.PI * n;
                return (float)(a0 - a1 * Math.Cos(2 * P) + a2 * Math.Cos(4 * P) - a3 * Math.Cos(6 * P) + a4 * Math.Cos(8 * P));
            });
    }

}
