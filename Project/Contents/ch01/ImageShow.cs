using Contents.Utility.MatPlotLib;

namespace Contents.ch01
{
    public class ImageShow
    {
        public Plot plt { get; set; }

        public void Execute()
        {
            var img = new Image("../dataset/lena.png"); // 画像の読み込み
            plt.imshow(img);
            plt.show();
        }
    }
}
