using Contents.Utility.MatPlotLib;
using System;
using System.Linq;
using np = Contents.Utility.Numpy;

namespace Contents.ch03
{
    public class Relu
    {
        public Plot plt { get; set; }

        static double[] relu(double[] x) => x.Select(e => Math.Max(0, e)).ToArray();

        public void Execute()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = relu(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 5.5);
            plt.show();
        }
    }
}
