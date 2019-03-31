using System;
using Contents.ch04;
using Contents.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test
{
    [TestClass]
    public class Test_gradient_simplenet : std.INeed
    {
        string _text;

        [TestMethod]
        public void Test()
        {
            var terget = new gradient_simplenet { std = new std(this) };
            terget.Core(new double[][]
                        {
                            new double[] { 0.32654684, 1.1150947, 1.5081993},
                            new double[] { -0.21576164, 0.05781574, -0.67418718 }
                        });

            var expected = "[0.136422055995178, 0.280087426224318, -0.416509482148442]\r\n[0.204633084056605, 0.420131139347024, -0.624764223167151]";
            Assert.AreEqual(expected, _text);
        }

        public void Print(string text) => _text = text;
        public string[] ReadAllLines(string path) => throw new NotSupportedException();
    }
}
