using Contents.Utility;
using np = Contents.Utility.numpy;

namespace Contents.ch03
{
    public class mnist_show
    {
        public std std { get; set; }
        public minst minst { get; set; }
        public pil pil { get; set; }

        void img_show(byte[][] img)
        {
            var pil_img = pil.fromarray(img);
            pil_img.show();
        }

        public void main()
        {
            (var x_train, var t_train, var x_test, var t_test) = minst.load_mnist();

            var img = x_train[0];
            var label = t_train[0];
            std.print(label);  // 5

            std.print(img.shape());  // (784,)
            var img2 = img.reshape(28, 28);  // 形状を元の画像サイズに変形
            std.print(img2.shape());  // (28, 28)

            img_show(img2);
        }
    }
}
