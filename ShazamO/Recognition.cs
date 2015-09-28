using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
/* *
 * DEFINE_OUTPUT for output print to bitmap.
 * */

namespace ShazamO
{
    class Recognition
    {
        /// <summary>
        /// Build footprints by sonoram
        /// </summary>
        /// <param name="Sono">Sonogram</param>
        public static uint[] GetPrintsFromSono(float[][] Sono)
        {
            int halfDepth = Sono[0].Length;
            const int Size = 33; // sizeof(uint) + 1;

            // Get pows (summ of squares)
            float[][] pows = new float[Sono.Length][];
            float dfstep = halfDepth / Size, fstep = 0;
            uint[] prints = new UInt32[pows.Length - 1];

            for (int i = 0; i < Sono.Length; i++)
            {
                pows[i] = new float[Size];
                fstep = 0;
                for (int j = 0; j < Size; j++)
                {
                    for (int k = (int)fstep; k < fstep + dfstep; k++)
                    {
                        pows[i][j] += Sono[i][k] * Sono[i][k];
                    }
                    fstep += dfstep;
                }
            }

#if DEFINE_OUTPUT 
            float[][] img = new float[pows.Length][];
            for(int i=0;i<img.Length;i++) img[i] = new float[pows[0].Length];
#endif
            //for (int i = 0; i < Size - 1; i++) pows[0][i] -= pows[0][i + 1];
            for (int i = 1; i < pows.Length; i++)
            {
                uint key = 1, val = 0;
                for (int j = 0; j < Size - 1; j++)
                {
                    //pows[i][j] -= pows[i][j + 1];
                    if (pows[i - 1][j] > pows[i][j])
                    {
                        val |= key;
#if DEFINE_OUTPUT       
                        img[i][j] = 255;
#endif
                    }
                    key <<= 1;
                }
                prints[i - 1] = val;
            }
#if DEFINE_OUTPUT
            SonoBuilder sono = new SonoBuilder();
            Bitmap bmp = sono.FillBmp(img);
            bmp.Save(DateTime.Now.ToShortDateString() + ".bmp");
#endif
            return prints;
        }

        private static byte[] Hamming256 = null;

        /// <summary>
        /// Initialize the hamming distance calculating
        /// </summary>
        public static void Initialize()
        {
            Hamming256 = new byte[256];
            Hamming256[0] = 0;
            for (int i = 1, k = 1; i < 256; i += i)
            {
                for (int j = 0; j < i; j++)
                {
                    Hamming256[k++] = (byte)(Hamming256[j] + 1);
                }
            }
        }

        /// <summary>
        /// Get the Hamming distance. Should once call Initialize() before 
        /// </summary>
        public static int HammingDist(uint A, uint B)
        {
            if (A == B) return 0;
            byte[] vs = BitConverter.GetBytes(A ^ B);
            return Hamming256[vs[0]] + Hamming256[vs[1]] + Hamming256[vs[2]] + Hamming256[vs[3]];
        }

        /// <summary>
        /// Get the Hamming distance. Should once call Initialize() before 
        /// </summary>
        public static long HammingDist(uint[] A, int AOffs, uint[] B, int BOffs, int Size)
        {
            long result = 0;
            for (int a = AOffs, b = BOffs; a < AOffs + Size; a++, b++)
            {
                result += HammingDist(A[a], B[b]);
            }
            return result;
        }

        /// <summary>
        /// Calculate the minimum of input array using splines
        /// </summary>
        public static PointF Minimum(float[] Data, int Offset, int Length)
        {
            if(Offset < 0) Offset = 0;
            if (Data.Length < Offset + Length) Length = Data.Length - Offset;
            if (Length < 2) return new PointF(Offset, Data[Offset]);

            int Accuracy = 50;
            float[] pts = new float[Length];
            Probability prob_pts = new Probability(Data, Offset, Length);
            for (int i = 0; i < Length; i++) pts[i] = Data[Offset + i] - prob_pts.M;

            float[] data = Spline(pts, 0, Length, Accuracy);

            float min = data[0];
            float imin = 0;
            for (int i = 1; i < data.Length; i++)
            {
                if (min > data[i])
                {
                    min = data[i];
                    imin = i;
                }
            }
            min += prob_pts.M;

            return new PointF(Offset + imin / Accuracy, min);
        }

        /// <summary>
        /// Increase sample rate of input array in Acceleration times using 3-Spline
        /// </summary>
        public static float[] Spline(float[] Data, int Offset, int Length, int Acceleration)
        {
            //PointF[] gpts = points.ToArray();
            int n = Length, ndec = n - 1;
            DObject d = new DObject(3 * n - 2, 3 * ndec);

            // Заполнение первого условия, количество точек - 2*(n-1)
            for (int i = 0; i < ndec; i++)
            {
                int i2 = i << 1, i3 = i2 + i;
                float _x = Offset + i;

                d.set(i3, i2, _x * _x);
                d.set(i3 + 1, i2, _x);
                d.set(i3 + 2, i2, 1);
                d.set(3 * ndec, i2, Data[(int)_x]);

                _x++;
                i2++;

                d.set(i3, i2, _x * _x);
                d.set(i3 + 1, i2, _x);
                d.set(i3 + 2, i2, 1);
                d.set(3 * ndec, i2, Data[(int)_x]);
            }

            // Второе условие, количество точек - n-2
            // 2a1x1 + b1 = 2a2x1 + b2
            // 2a2x2 + b2 = 2a3x2 + b3 
            //          ...
            for (int i = 0, i3 = 0, delta = 2 * ndec;
                i < n - 2;
                i++, i3 += 3, delta++)
            {
                float _x = (Offset + i + 1) * 2;

                d.set(i3, delta, _x);
                d.set(i3 + 1, delta, 1);
                d.set(i3 + 3, delta, -_x);
                d.set(i3 + 4, delta, -1);
            }

            // Третье условие
            int del = 3 * n - 4;
            d.set(0, del, 2 * Offset);
            d.set(1, del, -1);
            DObject coeff = new DObject(d).SLAU();
            if (coeff == null)
            {
                del = 6 * n - 9;
                d.set(0, del, (n << 1) - 2);
                d.set(1, del + 1, -1);
                coeff = new DObject(d).SLAU();
            }
            if (coeff == null) return null;

            float[] ret = new float[ndec * Acceleration];
            int k = 0;
            float dAcc = 1.0F / (Acceleration - 1);

            for (int i = 0; i < n - 1; i++)
            {
                ret[k++] = Data[Offset + i];
                int i3 = 3 * i;
                //ret.Add(gpts[i].toPoint());
                double _a = coeff.get(i3, 0);
                double _b = coeff.get(i3 + 1, 0);
                double _c = coeff.get(i3 + 2, 0);

                for (int j = 1; j < Acceleration - 1; j++)
                {
                    //double _x = i + j * 3;
                    //int _x = i * Acceleration + j;
                    double _x = Offset + i + j * dAcc;
                    ret[k++] = (float)((_a * _x + _b) * _x + _c);
                }
            }
            ret[k] = Data[Offset + ndec];
            
            return ret;
        
        }

        /// <summary>
        /// Return array of relative numbers
        /// </summary>
        public static float[] CompareHamming(uint[] A, uint[] B)
        {
            if (A.Length < B.Length) return CompareHamming(B, A);
            float[] result = new float[A.Length - B.Length + 1];
            for (int i = 0; i < result.Length; i++)
            {
                float dist = HammingDist(A, i, B, 0, B.Length);
                result[i] = dist;
            }
            return result;
        }
    }

    /// <summary>
    /// Позволяет использовать статистические величины для ходной последовательности данных
    /// </summary>
    public class Probability
    {
        /// <summary>
        /// Первый момент (математическое ожидание)
        /// </summary>
        public float M { get; private set; }

        /// <summary>
        /// Второй момент (мат. ожидание от квадрата)
        /// </summary>
        public float M2 { get; private set; }

        /// <summary>
        /// Дисперсия
        /// </summary>
        public float D { get; private set; }

        /// <summary>
        /// Минимум
        /// </summary>
        public float Min { get; private set; }

        /// <summary>
        /// Максимум
        /// </summary>
        public float Max { get; private set; }
        
        /// <summary>
        /// Индекс позиции минимального элемента
        /// </summary>
        public float IndexMin { get; private set; }

        /// <summary>
        /// Индекс позиции максимального элемента
        /// </summary>
        public float IndexMax { get; private set; }

        private Probability() { }

        public Probability(float[] Data)
            : this(Data, 0, Data.Length) { }

        public Probability(float[] Data, int Offset, int Length)
        {
            Min = Max = M = Data[Offset];
            M2 = M * M;
            double dM2 = M2, dM = M;
            IndexMin = IndexMax = 0;
            for (int i = Offset + 1; i < Offset + Length; i++)
            {
                float v = Data[i];
                dM += v;
                dM2 += v * v;
                if (v < Min)
                {
                    Min = v;
                    IndexMin = i;
                }
                else if (v > Max)
                {
                    Max = v;
                    IndexMax = i;
                }
            }
            dM2 /= Length;
            dM /= Length;
            M2 = (float)dM2;
            M = (float)dM;
            D = M2 - M * M;
        }

        /// <summary>
        /// Заполняет bmp изображением распределения данных сигнала.
        /// </summary>
        public static Bitmap FillBmpLogDistribution(float[] Data, int Width, int Height)
        {
            Bitmap bmp = new Bitmap(Width, Height);
            Pen pen = Pens.DarkGreen;
            Pen penLines = new Pen(Color.FromArgb(128, Color.Black));

            Probability prob = new Probability(Data);
            float Begin = prob.Min - 5;
            float End = prob.Max + 5;
            float Length = End - Begin;
            float delta = Width / Length;

            float sigma = (float)Math.Sqrt(prob.D);

            float[] probData = new float[(int)Width];
            float probDataMax = 0;

            for (int i = 0; i < Data.Length; i++)
            {
                int index = (int)((Data[i] - Begin) * delta);
                probData[index]++;
                if (probDataMax < probData[index]) probDataMax = probData[index];
            }

            for (int i = 0; i < probData.Length; i++)
            {
                probData[i] = (float)Math.Log(probData[i] + 1);
            }
            probDataMax = (float)Math.Log(probDataMax + 1);

            float dx = (float)Width / Length;
            float dy = (float)Height / probDataMax;
            using (var g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < probData.Length; i++)
                {
                    float y2 = (Height - probData[i] * dy);
                    g.DrawLine(pen, i, Height, i, y2);
                }
                float x2 = (prob.M - Begin);
                for (int i = 0; x2 > 0; i++)
                {
                    float x = x2 * dx;
                    g.DrawLine(penLines, x, 2, x, Height - 2);
                    x2 -= sigma;
                }
            }

            return bmp;
        }

        /// <summary>
        /// Заполняет bmp изображением распределения данных сигнала.
        /// </summary>
        public static Bitmap FillBmpDistribution(float[] Data)
        {
            int Width = 512;
            int Height = 256;

            Bitmap bmp = new Bitmap(Width, Height);
            Pen pen = Pens.Bisque;

            Probability prob = new Probability(Data);
            float Begin = prob.Min;
            float End = prob.Max;
            float Length = End - Begin;
            float delta = Width / Length;

            int[] probData = new int[Data.Length / 100];
            int probDataMax = 0;
            
            for (int i = 0; i < Data.Length; i++)
            {
                int index = (int)((Data[i] - Begin) * delta);
                probData[index]++;
                if(probDataMax < probData[index]) probDataMax = probData[index];
            }

            float dy = (float)Height / probDataMax;
            using (var g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < probData.Length; i++)
                {
                    float y2 = (Height - probData[i] * dy);
                    g.DrawLine(pen, i, Height, i, y2);
                }
            }

            return bmp;
        }


        public override string ToString()
        {
            return String.Format(
                " M = {0:0.0} {1} M2 = {2:0.0} {1} D = {3:0.0} {1} Min = {4:0.0} {1} Max = {5:0.0}",
                M, Environment.NewLine, M2, D, Min, Max );
        }

        public string ToStringCompact()
        {
            return String.Format(
                " M = {0:0.0}; sigma = {2:0.0}; Min = {3:0.0}; Max = {4:0.0}",
                M, Environment.NewLine, Math.Sqrt(D), Min, Max);
        }
    }
}
