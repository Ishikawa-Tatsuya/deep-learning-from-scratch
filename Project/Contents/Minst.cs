using System;
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

        public static (double[][] x_train, byte[] t_train, double[][] x_test, byte[] t_test) load_mnist_normalize(bool one_hot_label = false)
        {
            (var x_train, var t_train, var x_test, var t_test) = load_mnist(one_hot_label);
            return (x_train.Select(a=>a.Select(b=>((double)b)/255).ToArray()).ToArray(), t_train,
                    x_test.Select(a => a.Select(b => ((double)b) / 255).ToArray()).ToArray(), t_test);
        }

        public static (byte[][] x_train, byte[] t_train, byte[][] x_test, byte[] t_test) load_mnist(bool one_hot_label=false)
        {
            if (one_hot_label) throw new NotSupportedException();

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

