using Contents.Utility;
using Contents.Utility.matplotlib;
using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch04
{
    public class gradient_2d
    {
        public std std { get; set; }
        public plotlib plt { get; set; }

        static double[] _numerical_gradient_no_batch(Func<double[], double> f, double[] x)
        {
            var h = 1e-4;  // 0.0001
            var grad = np.zeros_like(x);

            for (int idx = 0; idx < x.Length; idx++)
            {
                var tmp_val = x[idx];
                x[idx] = tmp_val + h;
                var fxh1 = f(x);  // f(x+h)

                x[idx] = tmp_val - h;
                var fxh2 = f(x);  // f(x-h)
                grad[idx] = (fxh1 - fxh2) / (2 * h);

                x[idx] = tmp_val; // 値を元に戻す
            }

            return grad;
        }

        public static double[] numerical_gradient(Func<double[], double> f, double[] X)
            => _numerical_gradient_no_batch(f, X);

        public static double[][] numerical_gradient(Func<double[], double> f, double[][] X)
        {
            var grad = np.zeros_like(X);
            for (int idx = 0; idx < X.Length; idx++)
            {
                var x = X[idx];
                grad[idx] = _numerical_gradient_no_batch(f, x);
            }
            return grad;
        }

        double function_2(double[] a) => a.Sum(x => Math.Pow(x, 2));

        public void main()
        {
            var x0 = np.arange(-2, 2.5, 0.25);
            var x1 = np.arange(-2, 2.5, 0.25);
            (var XSrc, var YSrc) = np.meshgrid(x0, x1);

            var X = XSrc.flatten();
            var Y = YSrc.flatten();

            var grad = numerical_gradient(function_2, np.array(X, Y).縦横回転()).縦横回転();

            plt.figure();
            plt.quiver(X, Y, grad[0].minus(), grad[1].minus(), angles: "xy", color: "#666666");
            plt.xlim(-2, 2);
            plt.ylim(-2, 2);
            plt.xlabel("x0");
            plt.ylabel("x1");
            plt.grid();
            plt.draw();
            plt.show();
        }
    }
}
