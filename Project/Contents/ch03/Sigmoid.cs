using System;
using System.Linq;
using np = Contents.Numpy;
using plt = Contents.MatPlotLib.Plot;

namespace Contents.ch03
{
    public static class Sigmoid
    {
        static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public static void Execute()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = sigmoid(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 1.1);
            plt.show();
        }
    }
}
