namespace Contents.PIL
{
    public class Image
    {
        public interface INeed
        {
            void Show(byte[][] bin);
        }

        public static INeed Need { get; set; }

        public static Image fromarray(byte[][] src) => new Image(src);

        private byte[][] _bin;

        public Image(byte[][] src)=> _bin = src;

        public void show() => Need.Show(_bin);
    }
}
