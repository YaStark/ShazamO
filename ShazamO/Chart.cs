using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ShazamO
{
    public class Chart2D
    {
        protected double[] Data = null;
        protected double min = 0;
        protected double max = 0;

        public double DSize = sizeof(int);
        public long SamplingFrequency = 10000;

        public Color ColorLine = Color.Red;
        public Color ColorScale = Color.LightGray;
        public Color ColorDigits = Color.Black;

        protected double this[long i]
        {
            get
            {
                if (i > 0 && i < Data.Length) return Data[i];
                else return 0;
            }
        }

        public Chart2D() { }
        public Chart2D(double[] InputData)
        {
            Data = InputData;
            min = max = Data[0];
            for (int i = 1; i < Data.Length; i++)
            {
                if (min > Data[i]) min = Data[i];
                else if (max < Data[i]) max = Data[i];
            }
        }

        public void Chart1DLinear(
            ref Bitmap Chart,
            Rectangle Bounds,
            long BeginSample,
            long EndSample)
        {
            double mmax = Math.Max(Math.Abs(min), Math.Abs(max));
            long dsample = EndSample - BeginSample;
            double stepX = Bounds.Width * 1.0 / dsample;
            double stepY = Bounds.Height * 0.5 / mmax;
            Graphics g = Graphics.FromImage(Chart);
            Pen pen = new Pen(ColorLine);
            double y, y1;
            double SamplePerPixel = dsample / Bounds.Width;

            //Shading
            Color clr = Color.FromArgb(ColorLine.R / 2, ColorLine.G / 2, ColorLine.B / 2);
            pen.Color = clr;
            //
            int bb2 = Bounds.Height / 2 + Bounds.Top;

            for (int i = 0; i < Bounds.Width; i++)
            {
                y = y1 = 0;
                int d = (int)(BeginSample + i * SamplePerPixel);
                for (int j = 0; j < SamplePerPixel; j++)
                {
                    if (y > Data[d + j]) y = Data[d + j];
                    else if (y1 < Data[d + j]) y1 = Data[d + j];
                }
                y = bb2 - (int)(y * stepY);
                y1 = bb2 - (int)(y1 * stepY);
                if (y == y1) y++;
                g.DrawLine(pen, i + Bounds.X, (int)y, i + Bounds.X, (int)y1);
            }
            g.Save();
        }

        /*
        public void Chart1DLog(ref Bitmap Chart,
                               long BeginSample,
                               long EndSample)
        {
            Rectangle rect = new Rectangle(0, 0, Chart.Width, Chart.Height);
            Chart1DLog(ref Chart, BeginSample, EndSample, rect);
        }

        public void Chart1DLog(ref Bitmap Chart,
                               long BeginSample,
                               long EndSample,
                               Rectangle Bounds)
        {
            long mmax = (Math.Abs(max) > Math.Abs(min) ? Math.Abs(max) : Math.Abs(min));
            double mmin = Math.Abs(Data[0]);
            for (int i = 1; i < Data.Length; i++)
            {
                long d = Math.Abs(Data[i]);
                if (d != 0 && mmin > d) mmin = d;
            }

            long dsample = EndSample - BeginSample;
            double stepY = Bounds.Height * 1.0 / Math.Log10(mmin * 1.0 / mmax);
            Graphics g = Graphics.FromImage(Chart);
            Pen pen = new Pen(ColorLine);

            long y0, y1;
            double dd = dsample * 1.0 / Bounds.Width;

            for (int i = 0; i < Bounds.Width; i++)
            {
                int m = (int)(i * dd);
                y0 = y1 = Math.Abs(Data[m]);
                long data_mj;
                for (int j = 1; j < dd; j++)
                {
                    data_mj = Math.Abs(Data[m + j]);
                    if (y0 < data_mj) y0 = data_mj;
                    else if (y1 > data_mj) y1 = data_mj;
                }
                if (y0 - y1 == 0) y0++;

                if (y0 == 0) y0 = Bounds.Bottom - 1;
                else y0 = (int)(Math.Log10(y0 * 1.0 / mmax) * stepY);

                if (y1 == 0) y1 = Bounds.Bottom - 1;
                else y1 = (int)(Math.Log10(y1 * 1.0 / mmax) * stepY);

                g.DrawLine(pen, i + Bounds.X, Bounds.Y + y0, i + Bounds.X, Bounds.Y + y1);
            }
        }
         */
    }
}
