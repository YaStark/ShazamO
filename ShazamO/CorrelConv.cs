using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ShazamO
{
    public class CorrelConv
    {
        #region Pearson Correlation

        public static float[] PearsonCorr(float[] A, float[] B)
        {
            if (A.Length > B.Length) return PearsonCorr(B, A);

            float Ma = 0, Mb = 0, Ma2 = 0, Mb2 = 0;
            for (int i = 0; i < A.Length; i++)
            {
                Ma += A[i];
                Mb += B[i];
                Ma2 += A[i] * A[i];
                Mb2 += B[i] * B[i];
            }
            Ma /= A.Length;
            Mb /= A.Length;
            Ma2 /= A.Length;
            Mb2 /= A.Length;
            float Da = Ma2 - Ma * Ma;
            float Db = Mb2 - Mb * Mb;

            int count = B.Length - A.Length;
            float[] ret = new float[count];

             // First step
            float s = 0;
            for (int j = 0; j < A.Length; j++) s += (A[j] - Ma) * (B[j] - Mb);
            ret[0] = s / (float)Math.Sqrt(Da * Db);

             // Continue
            for (int i = 1; i < count; i++)
            {
                int iIn = A.Length + i - 1, iOut = i - 1;
                Mb += (B[iIn] - B[iOut]) / A.Length;
                Mb2 += (B[iIn] * B[iIn] - B[iOut] * B[iOut]) / A.Length;
                s = 0;
                Db = Mb2 - Mb * Mb;
                for (int j = 0; j < A.Length; j++) s += (A[j] - Ma) * (B[j + i] - Mb);
                ret[i] = s / (float)Math.Sqrt(Da * Db);
            }

            return ret;
        }

        public static float PearsonCorrSingle(float[] A, float[] B)
        {
            if (A.Length != B.Length) return 0;

            float Ma = 0, Mb = 0, Ma2 = 0, Mb2 = 0;
            for (int i = 0; i < A.Length; i++)
            {
                Ma += A[i];
                Mb += B[i];
                Ma2 += A[i] * A[i];
                Mb2 += B[i] * B[i];
            }
            Ma /= A.Length;
            Mb /= A.Length;
            Ma2 /= A.Length;
            Mb2 /= A.Length;
            float Da = Ma2 - Ma * Ma;
            float Db = Mb2 - Mb * Mb;
            float s = 0;
            for (int j = 0; j < A.Length; j++) s += (A[j] - Ma) * (B[j] - Mb);
            return s / (float)Math.Sqrt(Da * Db);
        }


        #endregion

        #region Spearman Correlation

        public static double[] CorrSpearman(int[] A, int[] B)
        {
            if (A.Length > B.Length) return CorrSpearman(B, A);

            int count = B.Length - A.Length;
            double[] ret = new double[count];
            int[] ARanked = GetRanked(A), BRanked;
            for (int i = 0; i < count; i++)
            {
                BRanked = GetRanked(B, i, A.Length);
                ret[i] = CorrSpearmanRanked(ARanked, BRanked);
            }
            return ret;
        }

        public static double CorrSpearmanRanked(int[] ARanked, int[] BRanked)
        {
            int alen = ARanked.Length;
            if (alen != BRanked.Length) throw new ArgumentException();
            double D = 0, Dt = 0;
            for (int i = 0; i < alen; i++)
            {
                Dt = ARanked[i] - BRanked[i];
                D += Dt * Dt;
            }
            return 1 - 6 * D / (alen * (alen * alen - 1));
        }

        public static int[] GetRanked(int[] A)
        {
            Dictionary<int, int> Dict = new Dictionary<int, int>();
            for (int i = 0; i < A.Length; i++) Dict.Add(i, A[i]);
            Dictionary<int, int> Dict2 = new Dictionary<int, int>();
            int index = 0;
            foreach (var item in Dict.OrderBy(pair => pair.Value)) Dict[item.Key] = index++;
            int[] ret = new int[A.Length];
            foreach(var item in Dict.OrderBy(pair => pair.Key))
            {
                ret[item.Key] = item.Value;
            }
            return ret;
        }

        public static int[] GetRanked(int[] A, int ABegin, int ALength)
        {
            Dictionary<int, int> Dict = new Dictionary<int, int>();
            for (int i = 0; i < ALength; i++) Dict.Add(i, A[i + ABegin]);
            Dictionary<int, int> Dict2 = new Dictionary<int, int>();
            int index = 0;
            foreach (var item in Dict.OrderBy(pair => pair.Value)) Dict[item.Key] = index++;
            int[] ret = new int[ALength];
            foreach (var item in Dict.OrderBy(pair => pair.Key))
            {
                ret[item.Key] = item.Value;
            }
            return ret;
        }

        #endregion
    }

}
