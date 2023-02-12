using Contents.Utility;
using System;
using np = Contents.Utility.numpy;
using Contents.Utility.matplotlib;
using System.Collections.Generic;

namespace Contents.ch04
{
    public class 微分
    {
        public std std { get; set; }
        public plotlib plt { get; set; }
        public minst minst { get; set; }

        public void main()
        {
            var xs = np.arange(0, 20, 0.1);
            var f_ys = new List<double>();
            var 接線_ys = new List<double>();
            var 微分_ys = new List<double>();

            Func<double, double> f = x => 0.01 * Math.Pow(x, 2) + 0.1 * x;

            var y5 = f(5);
            var y10 = f(10);

            //5a + b = y5
            //10a + b = y10
            //b = y5 - 5a
            //10a + y5 - 5a = y10
            //5a = y10 - y5
            var a = (y10 - y5) / 5;
            var b = y5 - 5 * ((y10 - y5) / 5);
            Func<double, double> 接線 = x => a * x + b;

            foreach (var x in xs)
            {
                f_ys.Add(f(x));
                接線_ys.Add(接線(x));
                微分_ys.Add(functions.微分_改良(f, x));
            }
            // グラフの描画
            plt.plot(xs, f_ys.ToArray(), label: "f");
            plt.plot(xs, 接線_ys.ToArray(), label: "接線");
            plt.plot(xs, 微分_ys.ToArray(), label: "微分");

            plt.ylim(0, 6);
            plt.legend(loc: "lower right");
            plt.show();
        }
    }

    public class 微分_関数チェック
    {
        public std std { get; set; }
        public plotlib plt { get; set; }
        public minst minst { get; set; }

        public void main()
        {
            var xs = np.arange(0, 20, 0.1);
            var 素直_ys = new List<double>();
            var 改良_ys = new List<double>();

            Func<double, double> f = x => 0.01 * Math.Pow(x, 2) + 0.1 * x;


            foreach (var x in xs)
            {
                素直_ys.Add(functions.微分_素直(f, x));
                改良_ys.Add(functions.微分_改良(f, x));
            }
            // グラフの描画
            plt.plot(xs, 素直_ys.ToArray(), label: "素直");
            plt.plot(xs, 改良_ys.ToArray(), label: "改良");

            plt.ylim(0, 1);
            plt.legend(loc: "lower right");
            plt.show();
        }
    }
}
