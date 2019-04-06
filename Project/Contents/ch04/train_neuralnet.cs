using Contents.Utility;
using System;
using np = Contents.Utility.numpy;
using static Contents.Utility.functions;
using static Contents.Utility.gradient;
using Contents.Utility.matplotlib;
using System.Collections.Generic;

namespace Contents.ch04
{
    public class train_neuralnet
    {
        public std std { get; set; }
        public plotlib plt { get; set; }
        public minst minst { get; set; }

        public void main()
        {
            (var x_train, var t_trainSrc, var x_test, var t_testSrc) = minst.load_mnist_normalize_onehot();
            var t_train = t_trainSrc.todouble();
            var t_test = t_testSrc.todouble();

            var network = new TwoLayerNet(input_size: 784, hidden_size: 50, output_size: 10);

            var iters_num = 10000;  // 繰り返しの回数を適宜設定する
            var train_size = x_train.Length;
            var batch_size = 100;
            var learning_rate = 0.1;

            var train_loss_list = new List<double>();
            var train_acc_list = new List<double>();
            var test_acc_list = new List<double>();

            var iter_per_epoch = Math.Max(train_size / batch_size, 1);

            for (int i = 0; i < iters_num; i++)
            {
                var batch_mask = np.random.choice(train_size, batch_size);
                var x_batch = x_train.choice(batch_mask);
                var t_batch = t_train.choice(batch_mask);

                // 勾配の計算
                // grad = network.numerical_gradient(x_batch, t_batch);
                var grad = network.gradient(x_batch, t_batch);

                foreach (var key in new[] { "W1", "b1", "W2", "b2" })
                {
                    network._params[key] = network._params[key].minus(grad[key].mul(learning_rate));
                }

                var loss = network.loss(x_batch, t_batch);
                train_loss_list.Add(loss);

                if (i % iter_per_epoch == 0)
                {
                    var train_acc = network.accuracy(x_train, t_train);
                    var test_acc = network.accuracy(x_test, t_test);
                    train_acc_list.Add(train_acc);
                    test_acc_list.Add(test_acc);
                    std.print("train acc, test acc | " + std.str(train_acc) + ", " + std.str(test_acc));
                }
            }

            // グラフの描画
            var markers = new Dictionary<string, string> { { "train", "o" }, { "test", "s" } };
            var x = np.arange(train_acc_list.Count);
            plt.plot(x, train_acc_list.ToArray(), label: "train acc");
            plt.plot(x, test_acc_list.ToArray(), label: "test acc", linestyle: "--");
            plt.xlabel("epochs");
            plt.ylabel("accuracy");
            plt.ylim(0, 1.0);
            plt.legend(loc: "lower right");
            plt.show();
        }
    }
}
