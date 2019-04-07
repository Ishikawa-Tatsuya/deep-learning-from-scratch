using Contents.Utility;

namespace Contents.ch05
{
    public class buy_apple
    {
        public std std { get; set; }

        public void main()
        {
            var apple = 100d;
            var apple_num = 2;
            var tax = 1.1;

            var mul_apple_layer = new MulLayer();
            var mul_tax_layer = new MulLayer();

            // forward
            var apple_price = mul_apple_layer.forward(apple, apple_num);
            var price = mul_tax_layer.forward(apple_price, tax);

            // backward
            var dprice = 1;
            (var dapple_price, var dtax) = mul_tax_layer.backward(dprice);
            (var dapple, var dapple_num) = mul_apple_layer.backward(dapple_price);

            std.print("price:" + (int)price);
            std.print("dApple:" + dapple);
            std.print("dApple_num:" + (int)dapple_num);
            std.print("dTax:" + dtax);
        }
    }
}
