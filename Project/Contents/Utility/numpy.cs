using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contents.Utility
{
    public static class numpy
    {
        public class Random
        {
            System.Random core = new System.Random();

            public double[][] randn(int count1, int count2)
            {
                return Enumerable.Range(0, count1).Select(_ => Enumerable.Range(0, count2).
                        Select(__ => core.NextDouble()).ToArray()).ToArray();
            }

            public int[] choice(int last, int count)
            {
                return Enumerable.Range(0, count).Select(_ => core.Next(0, last)).ToArray();
            }
        }

        public static double[] max(double[][] dy, int axis)
        {
            if (axis == 0)
            {
                var dst = Enumerable.Range(0, dy[0].Length).Select(_ => double.MinValue).ToArray();
                for (int j = 0; j < dst.Length; j++)
                {
                    for (int i = 0; i < dy.Length; i++)
                    {
                        dst[j] = Math.Max(dst[j], dy[i][j]);
                    }
                }
                return dst;
            }
            else if (axis == 1)
            {
                return dy.Select(e => e.Max()).ToArray();
            }
            throw new NotSupportedException();
        }

        public static double[][] exp(double[][] x) => x.Select(e1 => e1.Select(e2 => Math.Exp(e2)).ToArray()).ToArray();

        public static Random random { get; } = new Random();

        public static double[] arange(double start, double last, double interval)
        {
            var list = new List<double>();
            for (var i = start; i <= last; i += interval)
            {
                list.Add(i);
            }
            return list.ToArray();
        }

        public static T[] zeros<T>(int a) => new T[a];
        public static T[][] zeros<T>(int a1, int a2) => Enumerable.Range(0, a1).Select(_ => new T[a2]).ToArray();

        public static double[] arange(int count) => Enumerable.Range(0, count).Select(e => (double)e).ToArray();

        public static T[][] copy<T>(this T[][] src) => src.Select(e => e.ToArray()).ToArray();

        public static T[] copy<T>(this T[] src) => src.ToArray();

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

        internal static double[] sum(double[][] dy, int axis)
        {
            if (axis == 0)
            {
                var dst = new double[dy[0].Length];
                for (int j = 0; j < dst.Length; j++)
                {
                    for (int i = 0; i < dy.Length; i++)
                    {
                        dst[j] += dy[i][j];
                    }
                }
                return dst;
            }
            else if (axis == 1)
            {
                return dy.Select(e => e.Sum()).ToArray();
            }
            throw new NotSupportedException();
        }

        public static T[] array<T>(params T[] vals) => vals; 

        public static double[] cos(double[] src) => src.Select(e => Math.Cos(e)).ToArray();

        public static double[] sin(double[] src) => src.Select(e => Math.Sin(e)).ToArray();
        
        public static string shape<T>(this T[] target) => "(" + target.Length + ")";

        public static double[] dot(byte[] l, double[][] r) => dot(l.Select(e => (double)e).ToArray(), r);

        public static double[][] dot(double[][] l, double[][] r)
        {
            var dst = new double[l.Length][];
            Parallel.For(0, l.Length, i =>
            {
                dst[i] = dot(l[i], r);
            });
            return dst;
        }

        public static double[] dot(double[] l, double[][] r)
        {
            if (l.Length != r.Length || r.Length == 0) throw new NotSupportedException();

            var dst = new double[r[0].Length];
            
            for (int j = 0; j < r[0].Length; j++)
            { 
                double sum = 0;
                for (int k = 0; k < l.Length; k++)
                {
                    var val = l[k] * r[k][j];
                    sum += val;
                }
                dst[j] = sum;
            };

            return dst;
        }

        public static int size<T>(this T[][] target)
        {
            if (target.Length == 0) return 0;
            return target.Length * target[0].Length;
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

        public static int[] argmax(this byte[][] all, int axis)
            => argmax(all.Select(e1 => e1.Select(e2 => (double)e2).ToArray()).ToArray(), axis);

        public static int[] argmax(this double[][] all, int axis)
        {
            if (axis == 1) return all.Select(e => argmax(e)).ToArray();

            if (all.Length == 0) return new int[0];

            var maxs = new int[all[0].Length];

            for (int j = 0; j < all[0].Length; j++)
            {
                int index = 0;
                double max = double.MinValue;
                for (int i = 0; i < all.Length; i++)
                {
                    if (max < all[i][j])
                    {
                        max = all[i][j];
                        index = i;
                    }
                }
                maxs[j] = index;
            }
            return maxs;
        }

        public static int argmax(this double[] vals)
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
