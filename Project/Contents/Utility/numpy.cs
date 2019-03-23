using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents.Utility
{
    public static class numpy
    {
        public static double[] arange(double start, double last, double interval)
        {
            var list = new List<double>();
            for (var i = start; i <= last; i += interval)
            {
                list.Add(i);
            }
            return list.ToArray();
        }

        public static T[] zeros_like<T>(T[] x) => new T[x.Length];

        public static T[][] zeros_like<T>(T[][] x) => x.Select(e => new T[e.Length]).ToArray();

        public static (double[][] x, double[][] y) meshgrid(double[] x0, double[] y0)
        {
            var x = y0.Select(e => x0).ToArray();
            var y = y0.Select(e => Enumerable.Range(0, x0.Length).Select(_ => e).ToArray()).ToArray();
            return (x, y);
        }

        public static T[] flatten<T>(this T[][] src)
        {
            var list = new List<T>();
            foreach (var e in src)
            {
                list.AddRange(e);
            }
            return list.ToArray();
        }

        public static TT[][] T<TT>(this TT[][] src)
        {
            var dst = new TT[src[0].Length][];
            for (int i = 0; i < dst.Length; i++)
            {
                dst[i] = new TT[src.Length];
                for (int j = 0; j < dst[i].Length; j++)
                {
                    dst[i][j] = src[j][i];
                }
            }
            return dst;
        }

        public static T[] array<T>(params T[] vals) => vals; 

        public static double[] cos(double[] src) => src.Select(e => Math.Cos(e)).ToArray();

        public static double[] sin(double[] src) => src.Select(e => Math.Sin(e)).ToArray();
        
        public static string shape<T>(this T[] target) => "(" + target.Length + ")";

        public static double[] dot(byte[] l, double[][] r) => dot(l.Select(e => (double)e).ToArray(), r);

        public static double[][] dot(double[][] l, double[][] r) => l.Select(e => dot(e, r)).ToArray();

        public static double[] dot(double[] l, double[][] r)
        {
            if (l.Length != r.Length || r.Length == 0) throw new NotSupportedException();

            var dst = new List<double>(); 
            for (int j = 0; j < r[0].Length; j++)
            {
                double sum = 0;
                for (int i = 0; i < l.Length; i++)
                {
                    sum += l[i] * r[i][j];
                }
                dst.Add(sum);
            }
            return dst.ToArray();
        }

        public static string shape<T>(this T[][] target)
        {
            if (target.Length == 0) throw new NotSupportedException();
            return "(" + target.Length + "," + target[0].Length + ")";
        }

        public static T[][] reshape<T>(this T[] src, int rowCount, int colCount)
        {
            if (src.Length != rowCount * colCount) throw new NotSupportedException();

            var dst = new T[rowCount][];
            int i = 0;
            for (int row = 0; row < rowCount; row++)
            {
                dst[row] = new T[colCount];
                for (int col = 0; col < colCount; col++, i++)
                {
                    dst[row][col] = src[i];
                }
            }
            return dst;
        }

        public static int[] argmax(double[][] all, int axis) => all.Select(e => argmax(e)).ToArray();

        public static int argmax(double[] vals)
        {
            int index = 0;
            double max = double.MinValue;
            for (int i = 0; i < vals.Length; i++)
            {
                if (max < vals[i])
                {
                    max = vals[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
