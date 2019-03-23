﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents
{
    public static class Std
    {
        public interface INeed
        {
            void Print(string text);
            string[] ReadAllLines(string path);
        }

        public static INeed Need { get; set; }
        
        public static void print(object text) => Need.Print(text.ToString());
        
        public static double[][] Add(this double[][] l, double[] r) => l.Select(e=>e.Add(r)).ToArray();

        public static double[] Add(this double[] l, double[] r)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            var list = new List<double>();
            for (int i = 0; i < l.Length; i++)
            {
                list.Add(l[i] + r[i]);
            }
            return list.ToArray();
        }

        public static string[] ReadAllLines(string path) => Need.ReadAllLines(path);

        public static double[] Mul(this double[] l, int[] r) => Mul(l, r.Select(e=>(double)e).ToArray());

        public static double[] Mul(this double[] l, double[] r)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            var list = new List<double>();
            for (int i = 0; i < l.Length; i++)
            {
                list.Add(l[i] * r[i]);
            }
            return list.ToArray();
        }

        public static string str<T>(params T[] args) => "[" + string.Join(", ", args) + "]";
        
        public static T[] TakeSkip<T>(this T[] src, int start, int count)
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(src[start + i]);
            }
            return list.ToArray();
        }
    }
}
