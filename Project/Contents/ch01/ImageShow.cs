using Contents.MatPlotLib;
using plt = Contents.MatPlotLib.Plot;

namespace Contents.ch01
{
    public static class ImageShow
    {
        public static void Execute()
        {
            var img = new Image("../dataset/lena.png"); // 画像の読み込み
            plt.imshow(img);
            plt.show();
        }
    }
}
