﻿using System;
using System.Linq;

namespace Contents.Utility
{
    public class minst
    {
        public interface INeed
        {
            byte[] LoadFile(string path);
        }

        INeed need;

        public minst(INeed need) => this.need = need;

        public (double[][] x_train, byte[] t_train, double[][] x_test, byte[] t_test) load_mnist_normalize(bool one_hot_label = false)
        {
            (var x_train, var t_train, var x_test, var t_test) = load_mnist(one_hot_label);
            return (x_train.Select(a=>a.Select(b=>((double)b)/255).ToArray()).ToArray(), t_train,
                    x_test.Select(a => a.Select(b => ((double)b) / 255).ToArray()).ToArray(), t_test);
        }

        public (byte[][] x_train, byte[] t_train, byte[][] x_test, byte[] t_test) load_mnist(bool one_hot_label=false)
        {
            if (one_hot_label) throw new NotSupportedException();

            var x_train = LoadImage(need.LoadFile("dataset/mnist/train-images.idx3-ubyte"));
            var t_train = need.LoadFile("dataset/mnist/train-labels.idx1-ubyte").Skip(8).ToArray();
            var x_test = LoadImage(need.LoadFile("dataset/mnist/t10k-images.idx3-ubyte"));
            var t_test = need.LoadFile("dataset/mnist/t10k-labels.idx1-ubyte").Skip(8).ToArray();
            return (x_train, t_train, x_test, t_test);
        }

        byte[][] LoadImage(byte[] bin)
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

