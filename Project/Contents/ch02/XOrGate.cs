﻿using static Contents.Std;
using static Contents.ch02.AndGate;
using static Contents.ch02.OrGate;
using static Contents.ch02.NAndGate;

namespace Contents.ch02
{
    public class XOrGate
    {
        static int XOR(int x1, int x2)
        {
            var s1 = NAND(x1, x2);
            var s2 = OR(x1, x2);
            var y = AND(s1, s2);
            return y;
        }

        public static void Execute()
        {
            foreach (var xs in new[] { new[] { 0, 0 }, new[] { 1, 0 }, new[] { 0, 1 }, new[] { 1, 1 } })
            {
                var y = XOR(xs[0], xs[1]);
                print(str(xs) + " -> " + str(y));
            }
        }
    }
}
