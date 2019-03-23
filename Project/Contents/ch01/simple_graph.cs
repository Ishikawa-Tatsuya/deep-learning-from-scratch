using Contents.Utility.matplotlib;
using np = Contents.Utility.numpy;

namespace Contents.ch01
{
    public class simple_graph
    {
        public plotlib plt { get; set; }

        public void main()
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
