using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using Contents.ch01;
using Contents.ch02;
using Contents.ch03;
using Contents.Utility;
using Contents.Utility.MatPlotLib;
using OxyPlot;
using VVMConnection;

namespace WpfApp
{
    public class MainWindowVM : Std.INeed, Plot.INeed, PIL.INeed, Minst.INeed
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
        public Notify<string> YAxisTitle { get; } = new Notify<string>();
        public Notify<double> YMax { get; } = new Notify<double>();
        public Notify<double> YMin { get; } = new Notify<double>();
        public Notify<string> GraphTitle { get; } = new Notify<string>();

        public Action<string> ShowImageFile { get; set; }
        public Action<byte[][]> ShowImageBinary { get; set; }
        public Action<Action> SafeCall { get; set; }

        public MainWindowVM() => MakeContentsTree();

        public async void Execute()
        {
            //選択されているアイテムを取得
            var node = GetSelectedContentsItem();
            if (node.Execute == null) return;

            //実行中の名前を表示
            CurrentContens.Value = node.Name + " Executing ...";

            //クリア
            Log.Value = string.Empty;
            foreach (var e in Graph)
            {
                e.Title.Value = string.Empty;
                e.Coordinates.Clear();
            }
            XAxisTitle.Value = string.Empty;
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
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(Hungry), Execute = CreateContent<Hungry>().Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(Man), Execute = CreateContent<Man>().Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(SimpleGraph), Execute = CreateContent<SimpleGraph>().Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(SinCosGraph), Execute = CreateContent<SinCosGraph>().Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(ImageShow), Execute = CreateContent<ImageShow>().Execute });
            Contents.Add(ch1);
            
            var ch2 = new ContensTreeVM { Name = "Ch2" };
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(AndGate), Execute = CreateContent<AndGate>().Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(OrGate), Execute = CreateContent<OrGate>().Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(NAndGate), Execute = CreateContent<NAndGate>().Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(XOrGate), Execute = CreateContent<XOrGate>().Execute });
            Contents.Add(ch2);

            var ch3 = new ContensTreeVM { Name = "Ch3" };
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(StepFunction), Execute = CreateContent<StepFunction>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(Sigmoid), Execute = CreateContent<Sigmoid>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(SigStepCompare), Execute = CreateContent<SigStepCompare>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(Relu), Execute = CreateContent<Relu>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(MinstShow), Execute = CreateContent<MinstShow>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(NeuralnetMinst), Execute = CreateContent<NeuralnetMinst>().Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(NeuralnetMinstBatch), Execute = CreateContent<NeuralnetMinstBatch>().Execute });
            Contents.Add(ch3);
        }

        T CreateContent<T>()
        {
            var content = Activator.CreateInstance<T>();
            foreach (var prop in typeof(T).GetProperties().Where(e => e.GetSetMethod() != null))
            {
                if (prop.PropertyType == typeof(Std)) prop.SetValue(content, new Std(this));
                else if (prop.PropertyType == typeof(Plot)) prop.SetValue(content, new Plot(this));
                else if (prop.PropertyType == typeof(PIL)) prop.SetValue(content, new PIL(this));
                else if (prop.PropertyType == typeof(Minst)) prop.SetValue(content, new Minst(this));
            }
            return content;
        }

        void Std.INeed.Print(string text)
        {
            SafeCall(() =>
            {
                if (!string.IsNullOrEmpty(Log.Value))
                {
                    Log.Value += Environment.NewLine;
                }
                Log.Value += text;
            });
        }

        string[] Std.INeed.ReadAllLines(string path)
             => File.ReadAllLines(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void Plot.INeed.Plot(double[] x, double[] y, string lineStyle, string label)
        {
            SafeCall(() =>
            {
                if (x.Length != y.Length) throw new NotSupportedException();

                var target = Graph.FirstOrDefault(e => e.Coordinates.Count == 0);
                target.Title.Value = label;
                switch (lineStyle)
                {
                    case "--":
                    case "k--":
                        target.LineStyle.Value = LineStyle.Dash;
                        break;
                }
                for (int i = 0; i < x.Length; i++)
                {
                    target.Coordinates.Add(new DataPoint(x[i], y[i]));
                }
            });
        }

        void Plot.INeed.ShowImage(string path)
            => SafeCall(() =>ShowImageFile(Path.Combine(GetImageRoot(), path.Replace("../", ""))));

        void Plot.INeed.SetXLabel(string label)
           => SafeCall(() => XAxisTitle.Value = label);

        void Plot.INeed.SetYLabel(string label)
           => SafeCall(() => YAxisTitle.Value = label);

        void Plot.INeed.SetTitle(string title)
           => SafeCall(() => GraphTitle.Value = title);

        void Plot.INeed.SetYLim(double min, double max)
        {
            SafeCall(() =>
            {
                YMin.Value = min;
                YMax.Value = max;
            });
        }

        byte[] Minst.INeed.LoadFile(string path) 
            => File.ReadAllBytes(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void PIL.INeed.Show(byte[][] bin) => SafeCall(() => ShowImageBinary(bin));

        string GetImageRoot() => Path.GetDirectoryName(FindNearFile("Project.sln"));

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
