using np = Contents.Numpy;
using static Contents.Std;

namespace Contents.ch02
{
    public class NAndGate
    {
        internal static int NAND(int x1, int x2)
        {
            var x = np.array(x1, x2);
            var w = np.array(-0.5, -0.5);
            var b = 0.7;
            var tmp = np.sum(w.Mul(x)) + b;
            if (tmp <= 0)
                return 0;
            else
                return 1;
        }

        public static void Execute()
        {
            foreach (var xs in new[] { new[] { 0, 0 }, new[] { 1, 0 }, new[] { 0, 1 }, new[] { 1, 1 } })
            {
                var y = NAND(xs[0], xs[1]);
                print(str(xs) + " -> " + str(y));
            }
        }
    }
}
