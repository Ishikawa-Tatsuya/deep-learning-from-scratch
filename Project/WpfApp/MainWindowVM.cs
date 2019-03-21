﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Contents;
using Contents.ch01;
using Contents.MatPlotLib;
using OxyPlot;
using VVMConnection;

namespace WpfApp
{
    public class MainWindowVM : Std.INeed, Plot.INeed
    {
        public List<ContensTreeVM> Contents { get; } = new List<ContensTreeVM>();
        public Notify<string> CurrentContens { get; } = new Notify<string>();
        public Func<ContensTreeVM> GetSelectedContentsItem { get; set; }

        public Notify<string> Log { get; } = new Notify<string>();

        public LineSeriesViewModel[] Graph { get; } = Enumerable.Range(0, 10).Select(e => new LineSeriesViewModel()).ToArray();
        public Notify<string> XAxisTitle { get; } = new Notify<string>();
        public Notify<string> YAxisTitle { get; } = new Notify<string>();
        public Notify<string> GraphTitle { get; } = new Notify<string>();

        public Action<string> ShowImage { get; set; }

        public MainWindowVM()
        {
            MakeContentsTree();
            Std.Need = this;
            global::Contents.MatPlotLib.Plot.Need = this;
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

            //実行
            node.Execute();
        }

        void MakeContentsTree()
        {
            var ch0 = new ContensTreeVM { Name = "Ch0" };
            ch0.Nodes.Add(new ContensTreeVM { Name = nameof(Hungry), Execute = Hungry.Execute });
            ch0.Nodes.Add(new ContensTreeVM { Name = nameof(SimpleGraph), Execute = SimpleGraph.Execute });
            ch0.Nodes.Add(new ContensTreeVM { Name = nameof(ImageShow), Execute = ImageShow.Execute });
            ch0.Nodes.Add(new ContensTreeVM { Name = nameof(Man), Execute = Man.Execute });
            ch0.Nodes.Add(new ContensTreeVM { Name = nameof(SinCosGraph), Execute = SinCosGraph.Execute });
            Contents.Add(ch0);
        }

        void Std.INeed.Print(string text)
        {
            if (!string.IsNullOrEmpty(Log.Value))
            {
                Log.Value += Environment.NewLine;
            }
            Log.Value += text;
        }

        void Plot.INeed.Plot(double[] x, double[] y, string lineStyle, string label)
        {
            if (x.Length != y.Length) throw new NotSupportedException();

            var target = Graph.FirstOrDefault(e => e.Coordinates.Count == 0);
            for (int i = 0; i < x.Length; i++)
            {
                target.Coordinates.Add(new DataPoint(x[i], y[i]));
            }
        }

        void Plot.INeed.ShowImage(string path)=> ShowImage(Path.Combine(GetImageRoot(), path.Replace("../", "")));

        void Plot.INeed.SetXLabel(string label) => XAxisTitle.Value = label;

        void Plot.INeed.SetYLabel(string label) => YAxisTitle.Value = label;

        void Plot.INeed.SetTitle(string title) => GraphTitle.Value = title;

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
