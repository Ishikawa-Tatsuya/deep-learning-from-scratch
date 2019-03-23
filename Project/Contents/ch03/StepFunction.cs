using System.Linq;
using np = Contents.Numpy;
using plt = Contents.MatPlotLib.Plot;

namespace Contents.ch03
{
    public static class StepFunction
    {
        static double[] step_function(double[] x) => x.Select(e => e > 0 ? (double)1 : 0).ToArray();

        public static void Execute()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = step_function(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 1.1);  // 図で描画するy軸の範囲を指定
            plt.show();
        }
    }
}
