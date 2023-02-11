using System;
using System.Collections;
using System.Collections.Generic;
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
            var x2 = x.縦横回転();
            var x3 = x2.minus(np.max(x2, axis: 0));
            var y = np.exp(x3).div(np.sum(np.exp(x3), axis:0));
            return y.縦横回転();
        }

        public static double[] softmax(double[] x)
        {
            var max = x.Max();
            x = x.Select(e => e - max).ToArray(); // オーバーフロー対策
            var exp = x.Select(e => Math.Exp(e)).ToArray();
            var expSum = exp.Sum();
            return exp.Select(e => e / expSum).ToArray();
        }




        public static double 二乗和誤差(double[] 結果, double[] 正解)
        {
            if (正解.Length != 結果.Length) throw new NotImplementedException();
            double 合計 = 0;
            for (int j = 0; j < 結果.Length; j++)
            {
                var 正解確率 = 正解[j];
                var 計算確率 = 結果[j];
                合計 += Math.Pow(計算確率 - 正解確率, 2);
            }
            return 合計 / 2;
        }

        public static double 交差エントロピー誤差_1(double[] 結果, double[] 正解)
        {
            if (正解.Length != 結果.Length) throw new NotImplementedException();

            double 合計 = 0;
            for (int j = 0; j < 結果.Length; j++)
            {
                var 正解確率 = 正解[j];
                var 計算確率 = 結果[j];

                //1e-7はLog(0)を防ぐため
                合計 += Math.Log(計算確率 + 1e-7) * 正解確率;
            }
            double 誤差 = -合計;

            return 誤差;
        }

        public static double 交差エントロピー誤差_2(double[] 結果, double[] 正解)
        {
            if (正解.Length != 結果.Length) throw new NotImplementedException();

            // 教師データがone-hot-vectorの場合、正解ラベルのインデックスに変換
            var 正解の数字のインデックス = 正解.ToList().IndexOf(1);

            var 正解に対する確率 = 結果[正解の数字のインデックス];

            return -Math.Log(正解に対する確率 + 1e-7);//自然対数  底 2.718281828459
        }

        public static double 交差エントロピー誤差_バッチ対応(double[][] 結果, double[][] 正解)
        {
            //平均をとっている
            double 全体の合計 = 0;
            for (int i = 0; i < 正解.Length; i++)
            {
                全体の合計 += 交差エントロピー誤差_2(結果[i], 正解[i]);
            }
            var テストデータの数 = 結果.Length;
            return 全体の合計 / テストデータの数;
        }




        public static double 交差エントロピー誤差(double[] ySrc, double[] tSrc)
            => 交差エントロピー誤差_バッチ対応(ySrc.reshape(1, ySrc.Length), tSrc.reshape(1, tSrc.Length));

        public static double 交差エントロピー誤差(double[][] y, byte[][] t)
            => 交差エントロピー誤差_バッチ対応(y, t.Select(e1 => e1.Select(e2 => (double)e2).ToArray()).ToArray());

    }
}
