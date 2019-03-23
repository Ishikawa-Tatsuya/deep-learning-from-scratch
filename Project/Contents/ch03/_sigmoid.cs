using Contents.Utility.matplotlib;
using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch03
{
    public class sigmoid
    {
        public plotlib plt { get; set; }

        static double[] sigmoid_core(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public void main()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = sigmoid_core(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 1.1);
            plt.show();
        }
    }
}
