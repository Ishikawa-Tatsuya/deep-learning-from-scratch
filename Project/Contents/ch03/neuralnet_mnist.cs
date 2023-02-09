using System.Collections.Generic;
using static Contents.Utility.functions;
using np = Contents.Utility.numpy;
using System.Linq;
using Contents.Utility;

namespace Contents.ch03
{
    public class neuralnet_mnist
    {
        public std std { get; set; }
        public minst minst { get; set; }

        (double[][] x_test, byte[] t_test) get_data()
        {
            (var x_train, var t_train, var x_test, var t_test) = minst.load_mnist_normalize();
            return (x_test, t_test);
        }

        Dictionary<string, object> init_network()
        {
            //sample_weightをcsvファイルに落とした時点で若干精度が変わった。
            var dic = new Dictionary<string, object>();
            dic["W1"] = std.read_lines("../dataset/model/ch03/sample_weight/W1.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W2"] = std.read_lines("../dataset/model/ch03/sample_weight/W2.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["W3"] = std.read_lines("../dataset/model/ch03/sample_weight/W3.txt").Select(x => x.Split(',').Select(y => double.Parse(y)).ToArray()).ToArray();
            dic["b1"] = std.read_lines("../dataset/model/ch03/sample_weight/b1.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b2"] = std.read_lines("../dataset/model/ch03/sample_weight/b2.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
            dic["b3"] = std.read_lines("../dataset/model/ch03/sample_weight/b3.txt")[0].Split(',').Select(y => double.Parse(y)).ToArray();
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

            //x が画像でピクセル数が28*28=784
            //一段目のノードが784個
            //二段目のノードが50個
            //三段目は100個
            //出力は10個

            //W1は784×50
            //W2は50×100
            //W3は100×10
            //の配列になっている

            var a1 = np.dot(x, W1).add(b1);
            var z1 = sigmoid(a1);
            var a2 = np.dot(z1, W2).add(b2);
            var z2 = sigmoid(a2);
            var a3 = np.dot(z2, W3).add(b3);
            var y = softmax(a3);

            return y;
        }

        public void main()
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
            std.print("Accuracy:" + (accuracy_cnt / x.Length)); // Accuracy:0.9352
        }
    }
}
