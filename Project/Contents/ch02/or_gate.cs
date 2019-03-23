using Contents.Utility;
using System.Linq;
using np = Contents.Utility.numpy;

namespace Contents.ch02
{
    public class or_gate
    {
        public std std { get; set; }

        internal static int OR(int x1, int x2)
        {
            var x = np.array(x1, x2);
            var w = np.array(0.5, 0.5);
            var b = -0.2;
            var tmp = w.mul(x).Sum() + b;
            if (tmp <= 0)
                return 0;
            else
                return 1;
        }

        public void main()
        {
            foreach (var xs in new[] { new[] { 0, 0 }, new[] { 1, 0 }, new[] { 0, 1 }, new[] { 1, 1 } })
            {
                var y = OR(xs[0], xs[1]);
                std.print(std.str(xs) + " -> " + std.str(y));
            }
        }
    }
}
