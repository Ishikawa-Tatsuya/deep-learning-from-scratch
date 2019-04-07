using System;
using System.Collections.Generic;
using np = Contents.Utility.numpy;
using Contents.Utility;
using System.Linq;

namespace Contents.ch05
{
    public class TwoLayerNet
    {
        Dictionary<string, Array> _params = new Dictionary<string, Array>();
        Dictionary<string, ILayer> layers = new Dictionary<string, ILayer>();
        SoftmaxWithLoss lastLayer;

        public TwoLayerNet(int input_size, int hidden_size, int output_size, double weight_init_std = 0.01)
        {
            _params["W1"] = np.random.randn(input_size, hidden_size).mul(weight_init_std);
            _params["b1"] = np.zeros<double>(hidden_size);
            _params["W2"] = np.random.randn(hidden_size, output_size).mul(weight_init_std);
            _params["b2"] = np.zeros<double>(output_size);

            // レイヤの生成
            this.layers["Affine1"] = new Affine((double[][])this._params["W1"], (double[])this._params["b1"]);
            this.layers["Relu1"] = new Relu();
            this.layers["Affine2"] = new Affine((double[][])this._params["W2"], (double[])this._params["b2"]);
            this.lastLayer = new SoftmaxWithLoss();
        }

        public double[][] predict(double[][] x)
        {
            foreach (var layer in this.layers.Values)
            {
                x = layer.forward(x);
            }
            return x;
        }

        public double loss(double[][] x, double[][] t)
        {
            var y = predict(x);
            return this.lastLayer.forward(y, t);
        }

        public double accuracy(double[][] x, double[][] t)
        {
            var y = predict(x);
            var y2 = np.argmax(y, axis: 1);

            //常に2次元なので一旦ifはいらない
            //if t.ndim != 1 : t = np.argmax(t, axis = 1)
            var t2 = np.argmax(t, axis: 1);

            var accuracy = (double)y2.pair(t2).Count(e => e.Item1 == e.Item2) / x.Length;
            return accuracy;
        }

        // x:入力データ, t:教師データ
        public Dictionary<string, Array> numerical_gradient(double[][] x, double[][] t)
        {
            Func<double[][], double> loss_W1 = W => loss(x, t);
            Func<double[], double> loss_W2 = W => loss(x, t);

            var grads = new Dictionary<string, Array>();
            grads["W1"] = Contents.Utility.gradient.numerical_gradient(loss_W1, (double[][])this._params["W1"]);
            grads["b1"] = Contents.Utility.gradient.numerical_gradient(loss_W2, (double[])this._params["b1"]);
            grads["W2"] = Contents.Utility.gradient.numerical_gradient(loss_W1, (double[][])this._params["W2"]);
            grads["b2"] = Contents.Utility.gradient.numerical_gradient(loss_W2, (double[])this._params["b2"]);

            return grads;
        }

        public Dictionary<string, Array> gradient(double[][] x, double[][] t)
        {
            // forward
            this.loss(x, t);

            // backward
            var dout = this.lastLayer.backward(1);

            var layers = this.layers.Values.Reverse().ToList();

            foreach (var layer in layers)
            {
                dout = layer.backward(dout);
            }

            var grads = new Dictionary<string, Array>();

            grads["W1"] = ((Affine)this.layers["Affine1"]).dW;
            grads["b1"] = ((Affine)this.layers["Affine1"]).db;
            grads["W2"] = ((Affine)this.layers["Affine2"]).dW;
            grads["b2"] = ((Affine)this.layers["Affine2"]).db;
            return grads;
        }
    }
}
