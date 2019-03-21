using Contents.PIL;
using static Contents.Minst;
using static Contents.Std;
using np = Contents.Numpy;

namespace Contents.ch03
{
    public static class MinstShow
    {
        static void img_show(byte[][] img)
        {
            var pil_img = Image.fromarray(np.uint8(img));
            pil_img.show();
        }

        public static void Execute()
        {
            (var x_train, var t_train, var x_test, var t_test) = load_mnist(flatten: true, normalize: false);

            var img = x_train[0];
            var label = t_train[0];
            print(label);  // 5

            print(img.shape());  // (784,)
            var img2 = img.reshape(28, 28);  // 形状を元の画像サイズに変形
            print(img2.shape());  // (28, 28)

            img_show(img2);
        }
    }
}
