using Contents.Utility.MatPlotLib;
using np = Contents.Utility.Numpy;

namespace Contents.ch01
{
    public class SimpleGraph
    {
        public Plot plt { get; set; }

        public void Execute()
        {
            // データの作成
            var x = np.arange(0, 6, 0.1); // 0から6まで0.1刻みで生成
            var y = np.sin(x);

            // グラフの描画
            plt.plot(x, y);
            plt.show();
        }
    }
}
