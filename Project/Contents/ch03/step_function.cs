using Contents.Utility.matplotlib;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch03
{
    public class step_function
    {
        public plotlib plt { get; set; }

        static double[] step_function_core(double[] x) => x.Select(e => e > 0 ? (double)1 : 0).ToArray();

        public void main()
        {
            var X = np.arange(-5.0, 5.0, 0.1);
            var Y = step_function_core(X);
            plt.plot(X, Y);
            plt.ylim(-0.1, 1.1);  // 図で描画するy軸の範囲を指定
            plt.show();
        }
    }
}
