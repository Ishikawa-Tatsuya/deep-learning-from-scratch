using System;
using System.Linq;

namespace Contents.Utility
{
    public static class functions
    {
        public static double[][] sigmoid(double[][] x) => x.Select(e => sigmoid(e)).ToArray();

        public static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public static double[][] sigmoid_grad(double[][] x)
            => sigmoid(x).Select(e1 => e1.Select(e2 => 1.0 - e2).ToArray()).ToArray();

        public static double[][] softmax(double[][] x) => x.Select(e => softmax(e)).ToArray();

        public static double[] softmax(double[] x)
        {
            var max = x.Max();
            x = x.Select(e => e - max).ToArray(); // オーバーフロー対策
            var exp = x.Select(e => Math.Exp(e)).ToArray();
            var expSum = exp.Sum();
            return exp.Select(e => e / expSum).ToArray();
        }

        public static double cross_entropy_error(double[] ySrc, double[] tSrc)
            => cross_entropy_error(ySrc.reshape(1, ySrc.Length), tSrc.reshape(1, tSrc.Length));

        public static double cross_entropy_error(double[][] y, byte[][] t)
            => cross_entropy_error(y, t.Select(e1 => e1.Select(e2 => (double)e2).ToArray()).ToArray());

        public static double cross_entropy_error(double[][] y, double[][] t)
        {
            // 教師データがone-hot-vectorの場合、正解ラベルのインデックスに変換
            if (t.size() == y.size())
            {
                var t2 = t.argmax(axis: 1);

                var batch_size = y.Length;

                double sum = 0;
                for (int i = 0; i < y.Length; i++)
                {
                    var secondIndex = t2[i];
                    var val = y[i][secondIndex] + 1e-7;
                    sum += Math.Log(val);
                }
                return -sum / batch_size;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
