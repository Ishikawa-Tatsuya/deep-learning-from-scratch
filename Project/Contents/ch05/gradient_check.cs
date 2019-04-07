using Contents.Utility;
using np = Contents.Utility.numpy;
using Contents.Utility.matplotlib;
using System.Linq;

namespace Contents.ch05
{
    public class gradient_check
    {
        public std std { get; set; }
        public plotlib plt { get; set; }
        public minst minst { get; set; }

        public void main()
        {
            (var x_train, var t_trainSrc, var x_test, var t_testSrc) = minst.load_mnist_normalize_onehot();
            var t_train = t_trainSrc.todouble();
            var t_test = t_testSrc.todouble();

            var network = new TwoLayerNet(input_size:784, hidden_size:50, output_size:10);

            var x_batch = x_train.Take(3).ToArray();
            var t_batch = t_train.Take(3).ToArray();

            var grad_numerical = network.numerical_gradient(x_batch, t_batch);
            var grad_backprop = network.gradient(x_batch, t_batch);

            foreach (var key in grad_numerical.Keys)
            {
                var diff = np.average(np.abs(grad_backprop[key].minus(grad_numerical[key])));
                std.print(key + ":" + diff);
            }
        }
    }
}
