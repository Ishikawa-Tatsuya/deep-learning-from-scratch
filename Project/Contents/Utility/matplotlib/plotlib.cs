namespace Contents.Utility.matplotlib
{
    public class plotlib
    {
        public interface INeed
        {
            void Plot(double[] x, double[] y, string lineStyle, string label);
            void SetXLabel(string label);
            void SetYLabel(string label);
            void SetTitle(string title);
            void SetYLim(double min, double max);
            void ShowImage(string path);
        }

        INeed need;

        public plotlib(INeed need) => this.need = need;

        public void imshow(image img) => need.ShowImage(img.Path);

        public void ylim(double min, double max) => need.SetYLim(min, max);

        public void plot(double[] x, double[] y) => need.Plot(x, y, string.Empty, string.Empty);

        public void title(string label) => need.SetTitle(label);

        public void xlabel(string label) => need.SetXLabel(label);

        public void ylabel(string title) => need.SetYLabel(title);

        public void plot(double[] x, double[] y, string linestyle = "", string label = "") => need.Plot(x, y, linestyle, label);

        public void show() { }

        public void legend() { }
    }
}
