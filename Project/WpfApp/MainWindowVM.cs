using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contents;
using Contents.ch01;
using Contents.ch02;
using Contents.ch03;
using Contents.MatPlotLib;
using OxyPlot;
using VVMConnection;

namespace WpfApp
{
    public class MainWindowVM : Std.INeed, Plot.INeed, Contents.PIL.Image.INeed, Minst.INeed
    {
        public List<ContensTreeVM> Contents { get; } = new List<ContensTreeVM>();
        public Notify<string> CurrentContens { get; } = new Notify<string>();
        public Func<ContensTreeVM> GetSelectedContentsItem { get; set; }

        public Notify<string> Log { get; } = new Notify<string>();

        public LineSeriesViewModel[] Graph { get; } = Enumerable.Range(0, 10).Select(e => new LineSeriesViewModel()).ToArray();
        public Notify<string> XAxisTitle { get; } = new Notify<string>();
        public Notify<string> YAxisTitle { get; } = new Notify<string>();
        public Notify<double> YMax { get; } = new Notify<double>();
        public Notify<double> YMin { get; } = new Notify<double>();
        public Notify<string> GraphTitle { get; } = new Notify<string>();

        public Action<string> ShowImageFile { get; set; }
        public Action<byte[][]> ShowImageBinary { get; set; }

        public MainWindowVM()
        {
            MakeContentsTree();
            Std.Need = this;
            global::Contents.MatPlotLib.Plot.Need = this;
            global::Contents.PIL.Image.Need = this;
            global::Contents.Minst.Need = this;
        }

        public void Execute()
        {
            //選択されているアイテムを取得
            var node = GetSelectedContentsItem();
            if (node.Execute == null) return;

            //実行中の名前を表示
            CurrentContens.Value = node.Name;

            //クリア
            Log.Value = string.Empty;
            foreach (var e in Graph)
            {
                e.Coordinates.Clear();
            }
            XAxisTitle.Value = string.Empty;
            YAxisTitle.Value = string.Empty;
            YMin.Value = double.NaN;
            YMax.Value = double.NaN;
            GraphTitle.Value = string.Empty;

            //実行
            node.Execute();
        }

        void MakeContentsTree()
        {
            var ch1 = new ContensTreeVM { Name = "Ch1" };
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(Hungry), Execute = Hungry.Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(Man), Execute = Man.Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(SimpleGraph), Execute = SimpleGraph.Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(SinCosGraph), Execute = SinCosGraph.Execute });
            ch1.Nodes.Add(new ContensTreeVM { Name = nameof(ImageShow), Execute = ImageShow.Execute });
            Contents.Add(ch1);
            
            var ch2 = new ContensTreeVM { Name = "Ch2" };
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(AndGate), Execute = AndGate.Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(OrGate), Execute = OrGate.Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(NAndGate), Execute = NAndGate.Execute });
            ch2.Nodes.Add(new ContensTreeVM { Name = nameof(XOrGate), Execute = XOrGate.Execute });
            Contents.Add(ch2);

            var ch3 = new ContensTreeVM { Name = "Ch3" };
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(StepFunction), Execute = StepFunction.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(Sigmoid), Execute = Sigmoid.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(SigStepCompare), Execute = SigStepCompare.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(Relu), Execute = Relu.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(MinstShow), Execute = MinstShow.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(NeuralnetMinst), Execute = NeuralnetMinst.Execute });
            ch3.Nodes.Add(new ContensTreeVM { Name = nameof(NeuralnetMinstBatch), Execute = NeuralnetMinstBatch.Execute });
            Contents.Add(ch3);
        }
        
        void Std.INeed.Print(string text)
        {
            if (!string.IsNullOrEmpty(Log.Value))
            {
                Log.Value += Environment.NewLine;
            }
            Log.Value += text;
        }

        string[] Std.INeed.ReadAllLines(string path)
             => File.ReadAllLines(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void Plot.INeed.Plot(double[] x, double[] y, string lineStyle, string label)
        {
            if (x.Length != y.Length) throw new NotSupportedException();

            var target = Graph.FirstOrDefault(e => e.Coordinates.Count == 0);
            for (int i = 0; i < x.Length; i++)
            {
                target.Coordinates.Add(new DataPoint(x[i], y[i]));
            }
        }

        void Plot.INeed.ShowImage(string path)=> ShowImageFile(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void Plot.INeed.SetXLabel(string label) => XAxisTitle.Value = label;

        void Plot.INeed.SetYLabel(string label) => YAxisTitle.Value = label;

        void Plot.INeed.SetTitle(string title) => GraphTitle.Value = title;

        void Plot.INeed.SetYLim(double min, double max)
        {
            YMin.Value = min;
            YMax.Value = max;
        }

        byte[] Minst.INeed.LoadFile(string path) => File.ReadAllBytes(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void Contents.PIL.Image.INeed.Show(byte[][] bin) => ShowImageBinary(bin);

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
