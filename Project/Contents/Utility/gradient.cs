using System;
using np = Contents.Utility.numpy;

namespace Contents.Utility
{
    public static class gradient
    {
        public static double[][] numerical_gradient(Func<double[][], double> f, double[][] x)
        {
            var h = 1e-4; // 0.0001
            var grad = np.zeros_like(x);

            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < x[i].Length; j++)
                {
                    var tmp_val = x[i][j];
                    x[i][j] = tmp_val + h;
                    var fxh1 = f(x); // f(x+h)

                    x[i][j] = tmp_val - h;
                    var fxh2 = f(x); // f(x-h)
                    grad[i][j] = (fxh1 - fxh2) / (2 * h);

                    x[i][j] = tmp_val; // 値を元に戻す
                }
            }

            return grad;
        }
    }
}
