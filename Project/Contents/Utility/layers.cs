using System;
using System.Linq;
using np = Contents.Utility.numpy;
using static Contents.Utility.functions;

namespace Contents.Utility
{
    public interface ILayer
    {
        double[][] forward(double[][] x);
        double[][] backward(double[][] x);
    }

    public class Relu : ILayer
    {
        bool[][] mask;
        public double[][] forward(double[][] x)
        {
            this.mask = x.Select(e1 => e1.Select(e2 => e2 <= 0).ToArray()).ToArray();
            var @out = x.Select(e1 => e1.Select(e2 => e2 <= 0 ? 0 : e2).ToArray()).ToArray();
            return @out;
        }

        public double[][] backward(double[][] dout)
        {
            for (int i = 0; i < dout.Length; i++)
            {
                for (int j = 0; j < dout[i].Length; j++)
                {
                    if (this.mask[i][j]) dout[i][j] = 0;
                }
            }
            return dout;
        }
    }

    public class Affine : ILayer
    {
        double[][] W;
        double[] b;
        public double[][] dW { get; private set; }
        public double[] db { get; private set; }
        double[][] x;
        int[] original_x_shape;

        public Affine(double[][] W, double[] b)
        {
            this.W = W;
            this.b = b;
        }

        public double[][] forward(double[][] x)
        {
            // テンソル対応
            original_x_shape = x.shape();
            // 2次元でなければ2次元に変更する処理だが一旦いらない
            //var x2 = x.reshape(x.shape()[0], -1);
            this.x = x;

            var @out = np.dot(this.x, this.W).add(this.b);

            return @out;
        }

        public double[][] backward(double[][] dout)
        {
            var dx = np.dot(dout, this.W.T());
            this.dW = np.dot(this.x.T(), dout);
            this.db = np.sum(dout, axis:0);

            //一旦いらない
            //var dx = dx.reshape(*self.original_x_shape);  // 入力データの形状に戻す（テンソル対応）
            return dx;
        }
    }

    public class SoftmaxWithLoss
    {
        double[][] t;
        double[][] y;
        double loss;

        public double forward(double[][] x, double[][] t)
        {
            this.t = t;
            this.y = softmax(x);
            this.loss = cross_entropy_error(this.y, this.t);
            return this.loss;
        }

        public double[][] backward(double dout = 1)
        {
            var batch_size = this.t.shape()[0];
            if (this.t.size() == this.y.size()) // 教師データがone-hot-vectorの場合
            {
                return this.y.minus(this.t).div(batch_size);
            }
            else
            {
                throw new NotImplementedException();
                //まだ来ないので一旦未実装
                /*
                var dx = this.y.copy();
                dx[np.arange(batch_size), this.t] -= 1;
                dx = dx / batch_size;*/
            }
        }
    }
}
