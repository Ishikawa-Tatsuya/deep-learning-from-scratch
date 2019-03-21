namespace Contents.MatPlotLib
{
    public static class Plot
    {
        public interface INeed
        {
            void Plot(double[] x, double[] y, string lineStyle, string label);
            void SetXLabel(string label);
            void SetYLabel(string label);
            void SetTitle(string title);
            void ShowImage(string path);
        }

        public static INeed Need { get; set; }

        internal static void imshow(Image img) => Need.ShowImage(img.Path);

        internal static void plot(double[] x, double[] y) => Need.Plot(x, y, string.Empty, string.Empty);

        internal static void title(string label) => Need.SetTitle(label);

        internal static void xlabel(string label) => Need.SetXLabel(label);

        internal static void ylabel(string title) => Need.SetYLabel(title);

        internal static void plot(double[] x, double[] y, string label) => Need.Plot(x, y, string.Empty, label);
        internal static void plot(double[] x, double[] y, string linestyle, string label) => Need.Plot(x, y, linestyle, label);

        internal static void show() { }
        internal static void legend() { }
    }
}
