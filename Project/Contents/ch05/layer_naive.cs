using System;
using System.Collections.Generic;
using System.Text;

namespace Contents.ch05
{
    public class MulLayer
    {
        double x;
        double y;

        public double forward(double x, double y)
        {
            this.x = x;
            this.y = y;
            return x * y;
        }

        public (double dx, double dy) backward(double dout)
        {
            var dx = dout * this.y;
            var dy = dout * this.x;
            return (dx, dy);
        }
    }

    public class AddLayer
    {
        public double forward(double x, double y)
        {
            var @out = x + y;
            return @out;
        }

        public (double dx, double dy) backward(double dout)
        {
            var dx = dout * 1;
            var dy = dout * 1;
            return (dx, dy);
        }
    }
}
