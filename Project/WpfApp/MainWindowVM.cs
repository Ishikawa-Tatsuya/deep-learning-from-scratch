using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Contents.ch01;
using Contents.ch02;
using Contents.ch03;
using Contents.ch04;
using Contents.ch05;
using Contents.Utility;
using Contents.Utility.matplotlib;
using OxyPlot;
using VVMConnection;

namespace WpfApp
{
    public class MainWindowVM : std.INeed, plotlib.INeed, pil.INeed, minst.INeed
    {
        static readonly Color[] LineDefaultColors = new[] {
                    Colors.Blue, Colors.Orange, Colors.Green,
                    Colors.AliceBlue, Colors.OrangeRed, Colors.GreenYellow,
                    Colors.DarkBlue, Colors.DarkOrange, Colors.DarkGreen,
                    Colors.Black };

        public Notify<string> CurrentContens { get; } = new Notify<string>();

        public List<ContensTreeVM> Contents { get; } = new List<ContensTreeVM>();
        public Func<ContensTreeVM> GetSelectedContentsItem { get; set; }
        public Notify<bool> Executable { get; } = new Notify<bool>(true);

        public Notify<string> Log { get; } = new Notify<string>();

        public LineSeriesViewModel[] Graph { get; } = Enumerable.Range(0, 10).Select(i => new LineSeriesViewModel { Color = new Notify<Color>(LineDefaultColors[i])}).ToArray();
        public Notify<string> XAxisTitle { get; } = new Notify<string>();
        public Notify<double> XMax { get; } = new Notify<double>();
        public Notify<double> XMin { get; } = new Notify<double>();
        public Notify<string> YAxisTitle { get; } = new Notify<string>();
        public Notify<double> YMax { get; } = new Notify<double>();
        public Notify<double> YMin { get; } = new Notify<double>();
        public Notify<string> GraphTitle { get; } = new Notify<string>();

        public Action<string> ShowImageFile { get; set; }
        public Action<byte[][]> ShowImageBinary { get; set; }
        public Action<Action> MatchContext { get; set; }

        public MainWindowVM() => MakeContentsTree();

        public async void Execute()
        {
            //選択されているアイテムを取得
            var node = GetSelectedContentsItem();
            if (node == null || node.Execute == null) return;

            //実行中の名前を表示
            CurrentContens.Value = node.Name + " Executing ...";

            //クリア
            int i = 0;
            Log.Value = string.Empty;
            foreach (var e in Graph)
            {
                e.Title.Value = string.Empty;
                e.Color.Value = LineDefaultColors[i++];
                e.LineStyle.Value = LineStyle.Solid;
                e.MarkerType.Value = MarkerType.None;
                e.Coordinates.Clear();
            }
            XAxisTitle.Value = string.Empty;
            XMin.Value = double.NaN;
            XMax.Value = double.NaN;
            YAxisTitle.Value = string.Empty;
            YMin.Value = double.NaN;
            YMax.Value = double.NaN;
            GraphTitle.Value = string.Empty;

            //実行
            Executable.Value = false;
            await Task.Factory.StartNew(()=> node.Execute());
            Executable.Value = true;

            CurrentContens.Value = node.Name;
        }

        void MakeContentsTree()
        {
            var ch1 = new ContensTreeVM { Name = "Ch1" };
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(hungry), Execute = CreateContent<hungry>().main });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(man), Execute = CreateContent<man>().main });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(simple_graph), Execute = CreateContent<simple_graph>().main });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(sin_cos_graph), Execute = CreateContent<sin_cos_graph>().main });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(image_show), Execute = CreateContent<image_show>().main });
            Contents.Add(ch1);
            
            var ch2 = new ContensTreeVM { Name = "Ch2" };
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(and_gate), Execute = CreateContent<and_gate>().main });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(or_gate), Execute = CreateContent<or_gate>().main });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(nand_gate), Execute = CreateContent<nand_gate>().main });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(xor_gate), Execute = CreateContent<xor_gate>().main });
            Contents.Add(ch2);

            var ch3 = new ContensTreeVM { Name = "Ch3" };
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(step_function), Execute = CreateContent<step_function>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(sigmoid), Execute = CreateContent<sigmoid>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(sig_step_compare), Execute = CreateContent<sig_step_compare>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(relu), Execute = CreateContent<relu>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(mnist_show), Execute = CreateContent<mnist_show>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(neuralnet_mnist), Execute = CreateContent<neuralnet_mnist>().main });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(neuralnet_minst_batch), Execute = CreateContent<neuralnet_minst_batch>().main });
            Contents.Add(ch3);

            var ch4 = new ContensTreeVM { Name = "Ch4" };
            ch4.Nodes.Add(new ContensTreeVM { Name = nameof(gradient_1d), Execute = CreateContent<gradient_1d>().main });
            ch4.Nodes.Add(new ContensTreeVM { Name = nameof(gradient_2d), Execute = CreateContent<gradient_2d>().main });
            ch4.Nodes.Add(new ContensTreeVM { Name = nameof(gradient_method), Execute = CreateContent<gradient_method>().main });
            ch4.Nodes.Add(new ContensTreeVM { Name = nameof(gradient_simplenet), Execute = CreateContent<gradient_simplenet>().main });
            ch4.Nodes.Add(new ContensTreeVM { Name = nameof(global::Contents.ch04.train_neuralnet), Execute = CreateContent<global::Contents.ch04.train_neuralnet>().main });
            Contents.Add(ch4);

            var ch5 = new ContensTreeVM { Name = "Ch5" };
            ch5.Nodes.Add(new ContensTreeVM { Name = nameof(buy_apple), Execute = CreateContent<buy_apple>().main });
            ch5.Nodes.Add(new ContensTreeVM { Name = nameof(buy_apple_orange), Execute = CreateContent<buy_apple_orange>().main });
            ch5.Nodes.Add(new ContensTreeVM { Name = nameof(gradient_check), Execute = CreateContent<gradient_check>().main });
            ch5.Nodes.Add(new ContensTreeVM { Name = nameof(global::Contents.ch05.train_neuralnet), Execute = CreateContent<global::Contents.ch05.train_neuralnet>().main });
            Contents.Add(ch5);
        }

        T CreateContent<T>()
        {
            var content = Activator.CreateInstance<T>();
            foreach (var prop in typeof(T).GetProperties().Where(e => e.GetSetMethod() != null))
            {
                if (prop.PropertyType == typeof(std)) prop.SetValue(content, new std(this));
                else if (prop.PropertyType == typeof(plotlib)) prop.SetValue(content, new plotlib(this));
                else if (prop.PropertyType == typeof(pil)) prop.SetValue(content, new pil(this));
                else if (prop.PropertyType == typeof(minst)) prop.SetValue(content, new minst(this));
            }
            return content;
        }

        void std.INeed.Print(string text)
        {
            MatchContext(() =>
            {
                if (!string.IsNullOrEmpty(Log.Value))
                {
                    Log.Value += Environment.NewLine;
                }
                Log.Value += text;
            });
        }

        string[] std.INeed.ReadAllLines(string path)
             => File.ReadAllLines(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void plotlib.INeed.Plot(double[] x, double[] y, string lineStyle, string label)
        {
            MatchContext(() =>
            {
                if (x.Length != y.Length) throw new NotSupportedException();

                var target = Graph.FirstOrDefault(e => e.Coordinates.Count == 0);
                target.Title.Value = label;
                if (lineStyle.Contains("--"))
                {
                    target.LineStyle.Value = LineStyle.Dash;
                }
                var opt = lineStyle.Replace("--", string.Empty);
                switch (opt)
                {
                    case "k":
                        target.Color.Value = Colors.Black;
                        break;
                    case "b":
                        target.Color.Value = Colors.Blue;
                        break;
                    case "o":
                        target.LineStyle.Value = LineStyle.None;
                        target.MarkerType.Value = MarkerType.Circle;
                        break;
                }
                
                for (int i = 0; i < x.Length; i++)
                {
                    target.Coordinates.Add(new DataPoint(x[i], y[i]));
                }
            });
        }

        void plotlib.INeed.ShowImage(string path)
            => MatchContext(() =>ShowImageFile(Path.Combine(GetImageRoot(), path.Replace("../", ""))));

        void plotlib.INeed.SetXLabel(string label)
           => MatchContext(() => XAxisTitle.Value = label);

        void plotlib.INeed.SetYLabel(string label)
           => MatchContext(() => YAxisTitle.Value = label);

        void plotlib.INeed.SetTitle(string title)
           => MatchContext(() => GraphTitle.Value = title);

        void plotlib.INeed.SetYLim(double min, double max)
        {
            MatchContext(() =>
            {
                YMin.Value = min;
                YMax.Value = max;
            });
        }

        void plotlib.INeed.SetXLim(double min, double max)
        {
            MatchContext(() =>
            {
                XMin.Value = min;
                XMax.Value = max;
            });
        }

        void plotlib.INeed.Quiver(double[] x, double[] y, double[] grad0, double[] grad1, string angles, string color)
            => ((std.INeed)this).Print("グラフは無理。ブレイク張ってコードの動作を確認してください。");

        byte[] minst.INeed.LoadFile(string path) 
            => File.ReadAllBytes(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void pil.INeed.Show(byte[][] bin) 
            => MatchContext(() => ShowImageBinary(bin));

        string GetImageRoot()
            => Path.GetDirectoryName(FindNearFile("Project.sln"));

        static string FindNearFile(string fileName)
        {
            var path = typeof(MainWindowVM).Assembly.Location;
            while (true)
            {
                var filePath = Path.Combine(path, fileName);
                if (File.Exists(filePath)) return filePath;
                path = Path.GetDirectoryName(path);
            }
            throw new NotSupportedException();
        }
    }
}
