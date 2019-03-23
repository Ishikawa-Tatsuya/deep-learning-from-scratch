using Contents.Utility;
using Contents.Utility.matplotlib;
using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch04
{
    public class gradient_1d
    {
        public std std { get; set; }
        public plotlib plt { get; set; }

        static double numerical_diff(Func<double, double> f, double x)
        {
            var h = 1e-4; // 0.0001
            return (f(x + h) - f(x - h)) / (2 * h);
        }

        static double function_1(double x)
            => 0.01 * Math.Pow(x, 2) + 0.1 * x;

        static double[] function_1(double[] a)
            => a.Select(x => 0.01 * Math.Pow(x, 2) + 0.1 * x).ToArray();

        Func<double[], double[]> tangent_line(Func<double, double> f, double x)
        {
            var d = numerical_diff(f, x);
            std.print(d);
            var y = f(x) - d * x;
            return a => a.Select(t => d * t + y).ToArray();
        }

        public void main()
        {
            var x = np.arange(0.0, 20.0, 0.1);
            var y = function_1(x);
            plt.xlabel("x");
            plt.ylabel("f(x)");

            var tf = tangent_line(function_1, 5);
            var y2 = tf(x);

            plt.plot(x, y);
            plt.plot(x, y2);
            plt.show();
        }
    }
}
