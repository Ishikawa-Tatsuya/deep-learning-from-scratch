using System.Collections.Generic;
using static Contents.Utility.Functions;
using np = Contents.Utility.Numpy;
using System.Linq;
using Contents.Utility;

namespace Contents.ch03
{
    public class NeuralnetMinstBatch
    {
        public Std std { get; set; }
        public Minst minst { get; set; }

        (double[][] x_test, byte[] t_test) get_data()
        {
            (var x_train, var t_train, var x_test, var t_test) = minst.load_mnist_normalize(one_hot_label:false);
            return (x_test, t_test);
        }

        Dictionary<string, object> init_network()
        {
            //sample_weightをcsvファイルに落とした時点で若干精度が変わった。
            var dic = new Dictionary<string, object>();
            dic["W1"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/W1.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W2"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/W2.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W3"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/W3.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["b1"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/b1.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b2"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/b2.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b3"] = std.ReadAllLines("../dataset/model/ch03/sample_weight/b3.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            return dic;
        }

        static double[][] predict(Dictionary<string, object> network, double[][] x)
        {
            var w1 = (double[][])network["W1"];
            var w2 = (double[][])network["W2"];
            var w3 = (double[][])network["W3"];
            var b1 = (double[])network["b1"];
            var b2 = (double[])network["b2"];
            var b3 = (double[])network["b3"];

            var a1 = np.dot(x, w1).Add(b1);
            var z1 = sigmoid(a1);
            var a2 = np.dot(z1, w2).Add(b2);
            var z2 = sigmoid(a2);
            var a3 = np.dot(z2, w3).Add(b3);
            var y = softmax(a3);

            return y;
        }

        public void Execute()
        {
            (var x, var t) = get_data();
            var network = init_network();

            var batch_size = 100; // バッチの数
            double accuracy_cnt = 0;
            for (int i = 0; i < x.Length; i += batch_size)
            {
                var x_batch = x.TakeSkip(i, batch_size);
                var y_batch = predict(network, x_batch);
                var p = np.argmax(y_batch, axis:1);

                var t_batch = t.TakeSkip(i, batch_size);
                accuracy_cnt += p.Select((e, index) => new { actual = e, expected = t_batch[index] }).Count(e => e.actual == e.expected);
            }
            std.print("Accuracy:" + (accuracy_cnt / x.Length)); // Accuracy:0.9352*/
        }
    }
}
