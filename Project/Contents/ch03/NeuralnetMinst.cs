using System.Collections.Generic;
using static Contents.Minst;
using static Contents.Std;
using static Contents.Functions;
using np = Contents.Numpy;
using System.Linq;

namespace Contents.ch03
{
    public static class NeuralnetMinst
    {
        static (double[][] x_test, byte[] t_test) get_data()
        {
            (var x_train, var t_train, var x_test, var t_test) = load_mnist_normalize(one_hot_label:false);
            return (x_test, t_test);
        }

        static Dictionary<string, object> init_network()
        {
            //sample_weightをcsvファイルに落とした時点で若干精度が変わった。
            var dic = new Dictionary<string, object>();
            dic["W1"] = ReadAllLines("../dataset/model/ch03/sample_weight/W1.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W2"] = ReadAllLines("../dataset/model/ch03/sample_weight/W2.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W3"] = ReadAllLines("../dataset/model/ch03/sample_weight/W3.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["b1"] = ReadAllLines("../dataset/model/ch03/sample_weight/b1.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b2"] = ReadAllLines("../dataset/model/ch03/sample_weight/b2.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b3"] = ReadAllLines("../dataset/model/ch03/sample_weight/b3.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            return dic;
        }

        static double[] predict(Dictionary<string, object> network, double[] x)
        {
            var W1 = (double[][])network["W1"];
            var W2 = (double[][])network["W2"];
            var W3 = (double[][])network["W3"];
            var b1 = (double[])network["b1"];
            var b2 = (double[])network["b2"];
            var b3 = (double[])network["b3"];
            
            var a1 = np.dot(x, W1).Add(b1);
            var z1 = sigmoid(a1);
            var a2 = np.dot(z1, W2).Add(b2);
            var z2 = sigmoid(a2);
            var a3 = np.dot(z2, W3).Add(b3);
            var y = softmax(a3);

            return y;
        }

        public static void Execute()
        {
            (var x, var t) = get_data();
            var network = init_network();
            double accuracy_cnt = 0;
            for (int i = 0; i < x.Length; i++)
            {
                var y = predict(network, x[i]);
                var p = np.argmax(y); // 最も確率の高い要素のインデックスを取得
                if (p == t[i]) accuracy_cnt++;
            }
            print("Accuracy:" + (accuracy_cnt / x.Length)); // Accuracy:0.9352
        }
    }
}
