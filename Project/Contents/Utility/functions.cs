using System;
using System.Linq;

namespace Contents.Utility
{
    public static class functions
    {
        public static double[][] sigmoid(double[][] x) => x.Select(e => sigmoid(e)).ToArray();

        public static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public static double[][] softmax(double[][] x) => x.Select(e => softmax(e)).ToArray();

        public static double[] softmax(double[] x)
        {
            var max = x.Max();
            x = x.Select(e => e - max).ToArray(); // オーバーフロー対策
            var exp = x.Select(e => Math.Exp(e)).ToArray();
            var expSum = exp.Sum();
            return exp.Select(e => e / expSum).ToArray();
        }
    }
}
