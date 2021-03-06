﻿using Contents.Utility.matplotlib;
using np = Contents.Utility.numpy;

namespace Contents.ch01
{
    public class sin_cos_graph
    {
        public plotlib plt { get; set; }

        public void main()
        {
            // データの作成
            var x = np.arange(0, 6, 0.1); // 0から6まで0.1刻みで生成
            var y1 = np.sin(x);
            var y2 = np.cos(x);

            // グラフの描画
            plt.plot(x, y1, label : "sin");
            plt.plot(x, y2, linestyle : "--", label : "cos");
            plt.xlabel("x"); // x軸のラベル
            plt.ylabel("y"); // y軸のラベル
            plt.title("sin & cos");
            plt.legend();
            plt.show();
        }
    }
}