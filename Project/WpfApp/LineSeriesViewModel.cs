using OxyPlot;
using System.Windows.Media;
using VVMConnection;

namespace WpfApp
{
    public class LineSeriesViewModel
    {
        public ObservableCollectionEx<DataPoint> Coordinates { get; set; } = new ObservableCollectionEx<DataPoint>();
        public Notify<string> Title { get; set; } = new Notify<string>(string.Empty);
        public Notify<Color> Color { get; set; } = new Notify<Color>(Colors.Black);
        public MarkerType MarkerType { get; set; } = MarkerType.Plus;
    }
}
