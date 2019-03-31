using Contents.Utility;
using Contents.Utility.matplotlib;
using System;
using System.Collections.Generic;
using np = Contents.Utility.numpy;

namespace Contents.ch04
{
    public class gradient_method
    {
        public plotlib plt { get; set; }

        static (double[] x, double[][] x_history) gradient_descent(Func<double[], double> f, double[] init_x, double lr = 0.01, int step_num = 100)
        {
            var x = init_x;
            var x_history = new List<double[]>();

            for (int i = 0; i < step_num; i++)
            {
                x_history.Add(x.copy());

                var grad = gradient_2d.numerical_gradient(f, x);
                x = x.minus(grad.mul(lr));

            }
            return (x, x_history.ToArray());
        }

        static double function_2(double[] x)
            => Math.Pow(x[0], 2) + Math.Pow(x[1], 2);

        public void main()
        {
            var init_x = np.array(-3.0, 4.0);

            var lr = 0.1;
            var step_num = 20;
            (var x, var x_history) = gradient_descent(function_2, init_x, lr: lr, step_num: step_num);

            plt.plot(new double[] { -5, 5 }, new double[] { 0, 0 }, "--b");
            plt.plot(new double[] { 0, 0 }, new double[] { -5, 5 }, "--b");
            plt.plot(x_history.get_col(0), x_history.get_col(1), "o");

            plt.xlim(-3.5, 3.5);
            plt.ylim(-4.5, 4.5);
            plt.xlabel("X0");
            plt.ylabel("X1");
            plt.show();
        }
    }
}
