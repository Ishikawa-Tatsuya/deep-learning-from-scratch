using System;
using System.Collections.Generic;
using np = Contents.Utility.numpy;
using Contents.Utility;
using static Contents.Utility.functions;
using System.Linq;

namespace Contents.ch04
{
    public class TwoLayerNet
    {
        public Dictionary<string, Array> _params { get; } = new Dictionary<string, Array>();

        public TwoLayerNet(int input_size, int hidden_size, int output_size, double weight_init_std = 0.01)
        {
            _params["W1"] = np.random.randn(input_size, hidden_size).mul(weight_init_std);
            _params["b1"] = np.zeros<double>(hidden_size);
            _params["W2"] = np.random.randn(hidden_size, output_size).mul(weight_init_std);
            _params["b2"] = np.zeros<double>(output_size);
        }

        public double[][] predict(double[][] x)
        {
            var W1 = (double[][])_params["W1"];
            var W2 = (double[][])_params["W2"];
            var b1 = (double[])_params["b1"];
            var b2 = (double[])_params["b2"];

            var a1 = np.dot(x, W1).add(b1);
            var z1 = sigmoid(a1);
            var a2 = np.dot(z1, W2).add(b2);
            var y = softmax(a2);

            return y;
        }

        public double loss(double[][] x, double[][] t)
        {
            var y = predict(x);
            return 交差エントロピー誤差_バッチ対応(y, t);
        }

        public double accuracy(double[][] x, double[][] t)
        {
            var y = predict(x);
            var y2 = np.argmax(y, axis:1);
            var t2 = np.argmax(t, axis:1);

            var accuracy = (double)y2.pair(t2).Count(e=>e.Item1 == e.Item2) / x.Length;
            return accuracy;
        }

        public Dictionary<string, Array> gradient(double[][] 入力データ, double[][] 正解データ)
        {
            var W1 = (double[][])_params["W1"];
            var W2 = (double[][])_params["W2"];
            var b1 = (double[])_params["b1"];
            var b2 = (double[])_params["b2"];

            // forward
            // 普通に確率を出す
            var 一層目の出力 = np.dot(入力データ, W1).add(b1);
            var 一層目の活性化後 = sigmoid(一層目の出力);
            var 二層目の出力 = np.dot(一層目の活性化後, W2).add(b2);
            var 今回計算した確率 = softmax(二層目の出力);

            var grads = new Dictionary<string, Array>();
            var テストの数 = 入力データ.Length;

            // backward
            var dy = 今回計算した確率.minus(正解データ).div(テストの数);
            grads["W2"] = np.dot(一層目の活性化後.縦横回転(), dy);
            grads["b2"] = np.sum(dy, axis:0);

            var dz1 = np.dot(dy, W2.縦横回転());
            var da1 = sigmoid_grad(一層目の出力).mul(dz1);
            grads["W1"] = np.dot(入力データ.縦横回転(), da1);
            grads["b1"] = np.sum(da1, axis:0);

            return grads;
        }
    }
}

