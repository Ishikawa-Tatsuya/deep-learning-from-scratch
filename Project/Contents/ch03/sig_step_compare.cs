using Contents.Utility.matplotlib;
using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch03
{
    public class sig_step_compare
    {
        public plotlib plt { get; set; }

        static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        static double[] step_function(double[] x) => x.Select(e => e > 0 ? (double)1 : 0).ToArray();

        public void main()
        {
            var x = np.arange(-5.0, 5.0, 0.1);
            var y1 = sigmoid(x);
            var y2 = step_function(x);
            plt.plot(x, y1);
            plt.plot(x, y2, "k--");
            plt.ylim(-0.1, 1.1);
            plt.show();
        }
    }
}
