using System;
using System.Linq;
using np = Contents.Numpy;
using plt = Contents.MatPlotLib.Plot;

namespace Contents.ch03
{
    public static class SigStepCompare
    {
        static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        static double[] step_function(double[] x) => x.Select(e => e > 0 ? (double)1 : 0).ToArray();

        public static void Execute()
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
