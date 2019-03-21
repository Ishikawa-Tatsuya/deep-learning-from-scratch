using System.Linq;

namespace Contents
{
    public static class Minst
    {
        public interface INeed
        {
            byte[] LoadFile(string path);
        }

        public static INeed Need { get; set; }

        public static (byte[][] x_train, byte[] t_train, byte[][] x_test, byte[] t_test) load_mnist(bool flatten, bool normalize)
        {
            var x_train = LoadImage(Need.LoadFile("dataset/mnist/train-images.idx3-ubyte"));
            var t_train = Need.LoadFile("dataset/mnist/train-labels.idx1-ubyte").Skip(8).ToArray();
            var x_test = LoadImage(Need.LoadFile("dataset/mnist/t10k-images.idx3-ubyte"));
            var t_test = Need.LoadFile("dataset/mnist/t10k-labels.idx1-ubyte").Skip(8).ToArray();
            return (x_train, t_train, x_test, t_test);
        }

        static byte[][] LoadImage(byte[] bin)
        {
            byte[][] x_train = new byte[(bin.Length - 16) / 784][];
            int index = 16;
            for (int i = 0; i < x_train.Length; i++)
            {
                x_train[i] = new byte[784];
                for (int j = 0; j < 784; j++, index++)
                {
                    x_train[i][j] = bin[index];
                }
            }

            return x_train;
        }
    }
}

