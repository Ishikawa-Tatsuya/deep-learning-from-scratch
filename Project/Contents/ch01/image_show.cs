using Contents.Utility.matplotlib;

namespace Contents.ch01
{
    public class image_show
    {
        public plotlib plt { get; set; }

        public void main()
        {
            var img = new image("../dataset/lena.png"); // 画像の読み込み
            plt.imshow(img);
            plt.show();
        }
    }
}
