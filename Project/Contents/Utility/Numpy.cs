﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents.Utility
{
    public static class Numpy
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
        
        public static double sum(double[] vals) => vals.Sum();

        public static T[] array<T>(params T[] vals) => vals; 

        public static double[] cos(double[] src) => src.Select(e => Math.Cos(e)).ToArray();

        public static double[] sin(double[] src) => src.Select(e => Math.Sin(e)).ToArray();

        public static byte[][] uint8(double[][] src) => src.Select(x=>x.Select(y=>(byte)y).ToArray()).ToArray();

        public static byte[][] uint8(byte[][] src) => src;

        public static string shape<T>(this T[] target) => "(" + target.Length + ")";

        public static double[] dot(byte[] l, double[][] r) => dot(l.Select(e => (double)e).ToArray(), r);

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

        public static double[][] dot(double[][] l, double[][] r) => l.Select(e => dot(e, r)).ToArray();

        //配列長さ0は未サポート
        public static string shape<T>(this T[][] target) => "(" + target.Length + "," + target[0].Length + ")";

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

        internal static int argmax(double[] vals)
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

        internal static int[] argmax(double[][] all, int axis) => all.Select(e => argmax(e)).ToArray();
    }
}