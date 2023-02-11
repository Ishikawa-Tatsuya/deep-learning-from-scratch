using Contents.Utility;
using System;
using np = Contents.Utility.numpy;
using static Contents.Utility.functions;
using static Contents.Utility.gradient;

namespace Contents.ch04
{
    public class gradient_simplenet
    {
        class simpleNet
        {
            public double[][] W { get; }

            internal simpleNet(double[][] w)
                => W = w == null ? np.random.randn(2, 3) : w;

            internal double[] predict(double[] x) => np.dot(x, W);

            internal double loss(double[] x, double[] t)
            {
                var z = predict(x);
                var y = softmax(z);
                var loss = 交差エントロピー誤差(y, t);
                return loss;
            }
        }

        public std std { get; set; }

        public void main() => Core(null);

        public void Core(double[][] random)
        {
            var x = np.array(0.6, 0.9);
            var t = np.array(0d, 0, 1);

            var net = new simpleNet(random);

            Func<double[][], double> f = w => net.loss(x, t);

            var dW = numerical_gradient(f, net.W);

            std.print(dW);
        }
    }
}
