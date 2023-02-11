using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.Utility
{
    public static class functions
    {
        public static double[][] sigmoid(double[][] x) => x.Select(e => sigmoid(e)).ToArray();

        public static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public static double[][] sigmoid_grad(double[][] x)
        {
            var sigmoidX = sigmoid(x);
            return sigmoidX.Select(e1 => e1.Select(e2 => 1.0 - e2).ToArray()).ToArray().mul(sigmoidX);
        }

        public static double[][] softmax(double[][] x)
        {
            var x2 = x.T();
            var x3 = x2.minus(np.max(x2, axis: 0));
            var y = np.exp(x3).div(np.sum(np.exp(x3), axis:0));
            return y.T();
        }

        public static double[] softmax(double[] x)
        {
            var max = x.Max();
            x = x.Select(e => e - max).ToArray(); // オーバーフロー対策
            var exp = x.Select(e => Math.Exp(e)).ToArray();
            var expSum = exp.Sum();
            return exp.Select(e => e / expSum).ToArray();
        }

        public static double 交差エントロピー誤差(double[] ySrc, double[] tSrc)
            => 交差エントロピー誤差(ySrc.reshape(1, ySrc.Length), tSrc.reshape(1, tSrc.Length));

        public static double 交差エントロピー誤差(double[][] y, byte[][] t)
            => 交差エントロピー誤差(y, t.Select(e1 => e1.Select(e2 => (double)e2).ToArray()).ToArray());

        public static double 交差エントロピー誤差(double[][] 結果, double[][] 正解)
        {
            if (正解.size() != 結果.size()) throw new NotImplementedException();

            // 教師データがone-hot-vectorの場合、正解ラベルのインデックスに変換
            var 正解の数字= 正解.argmax(axis: 1);

            double 合計 = 0;
            for (int i = 0; i < 結果.Length; i++)
            {
                var secondIndex = 正解の数字[i];

                var 正解に対する確率 = 結果[i][secondIndex];

                var val = 正解に対する確率 + 1e-7;
                合計 += Math.Log(val);//自然対数  底 2.718281828459
            }

            //平均
            var テストデータの数 = 結果.Length;
            return -合計 / テストデータの数;
        }
    }
}
