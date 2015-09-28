using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShazamO
{
    /// <summary>
    /// Описывает комплексное число с реальной и мнимой частями типа double
    /// </summary>
    public struct Complex : IComparable<Complex>
    {
        /// <summary>
        /// Мнимая часть
        /// </summary>
        public float im;

        /// <summary>
        /// Реальная часть
        /// </summary>
        public float re;

        /// <summary>
        /// Амплитуда
        /// </summary>
        public float Amplitude
        {
            get { return (float)Math.Sqrt(re * re + im * im); }
        }

        public Complex(float Re, float Im) { im = Im; re = Re; }
        public Complex(long Re) { re = Re; im = 0; }
        public Complex(float Re) { re = Re; im = 0; }

        /// <summary>
        /// Линейная свертка
        /// </summary>
        public static Complex[] LinearConvolution(Complex[] Data, Complex[] Core)
        {
            Complex[] r = new Complex[Data.Length];
            Complex storage;

            for (int i = 0; i < Core.Length; i++)
            {
                storage = new Complex();
                int bound = Core.Length - 1 - i;
                for (int j = Core.Length - 1; j >= bound; j--) storage = storage + Data[i + j] * Core[j];
                r[i] = storage;
            }
            for (int i = Core.Length; i < r.Length - Core.Length; i++)
            {
                storage = new Complex();
                for (int j = 0; j < Core.Length; j++) storage = storage + Data[i + j] * Core[j];
                r[i] = storage;
            }
            for (int i = r.Length - Core.Length; i < r.Length; i++)
            {
                storage = new Complex();
                int bound = Data.Length - 1 - i;
                for (int j = 0; j < bound; j++) storage = storage + Data[i + j] * Core[j];
                r[i] = storage;
            }
            return r;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);
        }

        public static Complex operator -(Complex a)
        {
            return new Complex(-a.re, -a.im);
        }

        public static Complex operator *(Complex a, float b)
        {
            return new Complex(a.re * b, a.im * b);
        }

        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.re + b.re, a.im + b.im);
        }

        public static Complex operator -(Complex a, Complex b)
        {
            return new Complex(a.re - b.re, a.im - b.im);
        }

        public static Complex operator +(Complex a, float b)
        {
            return new Complex(a.re + b, a.im);
        }

        public static Complex Exp(Complex x)
        {
            float ex = (float)Math.Exp(x.re);
            return new Complex(ex * (float)Math.Cos(x.im), ex * (float)Math.Sin(x.im));
        }

        public int CompareTo(Complex obj)
        {
            return (int)(re - obj.re);
        }
    }

}
