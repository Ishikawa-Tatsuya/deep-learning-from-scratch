using Contents.Utility;

namespace Contents.ch05
{
    public class buy_apple_orange
    {
        public std std { get; set; }

        public void main()
        {
            var apple = 100d;
            var apple_num = 2d;
            var orange = 150d;
            var orange_num = 3d;
            var tax = 1.1d;

            // layer
            var mul_apple_layer = new MulLayer();
            var mul_orange_layer = new MulLayer();
            var add_apple_orange_layer = new AddLayer();
            var mul_tax_layer = new MulLayer();

            // forward
            var apple_price = mul_apple_layer.forward(apple, apple_num);  // (1)
            var orange_price = mul_orange_layer.forward(orange, orange_num);  // (2)
            var all_price = add_apple_orange_layer.forward(apple_price, orange_price);  // (3)
            var price = mul_tax_layer.forward(all_price, tax);  // (4)

            // backward
            var dprice = 1;
            (var dall_price, var dtax) = mul_tax_layer.backward(dprice);  // (4)
            (var dapple_price, var dorange_price) = add_apple_orange_layer.backward(dall_price);  // (3)
            (var dorange, var dorange_num) = mul_orange_layer.backward(dorange_price);  // (2)
            (var dapple, var dapple_num) = mul_apple_layer.backward(dapple_price);  // (1)

            std.print("price:" + (int)price);
            std.print("dApple:" + dapple);
            std.print("dApple_num:" + (int)dapple_num);
            std.print("dOrange:" + dorange);
            std.print("dOrange_num:" + (int)dorange_num);
            std.print("dTax:" + dtax);
        }
    }
}
