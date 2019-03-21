using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents
{
    public class Numpy
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

        internal static double sum(double[] vals) => vals.Sum();

        internal static T[] array<T>(params T[] vals) => vals; 

        public static double[] cos(double[] src) => src.Select(e => Math.Cos(e)).ToArray();

        public static double[] sin(double[] src) => src.Select(e => Math.Sin(e)).ToArray();
    }
}
