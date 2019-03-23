using System;
using System.Linq;
using np = Contents.Numpy;
using plt = Contents.MatPlotLib.Plot;

namespace Contents.ch03
{
    public static class Relu
    {
        static double[] relu(double[] x) => x.Select(e => Math.Max(0, e)).ToArray();

        public static void Execute()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = relu(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 5.5);
            plt.show();
        }
    }
}
