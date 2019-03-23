﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents.Utility
{
    public class Std
    {
        public interface INeed
        {
            void Print(string text);
            string[] ReadAllLines(string path);
        }

        INeed need;

        public Std(INeed need) => this.need = need;
        public void print(object text) => need.Print(text.ToString());
        public string[] ReadAllLines(string path) => need.ReadAllLines(path);
        public string str<T>(params T[] args) => "[" + string.Join(", ", args) + "]";
    }

    public static class StdExt
    {
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