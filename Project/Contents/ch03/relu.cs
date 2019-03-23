using Contents.Utility.matplotlib;
using System;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch03
{
    public class relu
    {
        public plotlib plt { get; set; }

        static double[] relu_core(double[] x) => x.Select(e => Math.Max(0, e)).ToArray();

        public void main()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = relu_core(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 5.5);
            plt.show();
        }
    }
}
