namespace Contents.Utility
{
    public class pil
    {
        public interface INeed
        {
            void Show(byte[][] bin);
        }

        INeed need;
        public pil(INeed need) => this.need = need;
        public Image fromarray(byte[][] src) => new Image(need, src);

        public class Image
        {

            INeed _need { get; }
            byte[][] _bin;

            public Image(INeed need, byte[][] src)
            {
                _need = need;
                _bin = src;
            }

            public void show() => _need.Show(_bin);
        }
    }
}
