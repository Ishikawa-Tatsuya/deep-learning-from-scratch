using Contents.Utility.MatPlotLib;
using System;
using System.Linq;
using np = Contents.Utility.Numpy;

namespace Contents.ch03
{
    public class Sigmoid
    {
        public Plot plt { get; set; }

        static double[] sigmoid(double[] x) => x.Select(e => 1 / (1 + Math.Exp(-e))).ToArray();

        public void Execute()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = sigmoid(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 1.1);
            plt.show();
        }
    }
}
