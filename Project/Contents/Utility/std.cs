using System;
using System.Collections.Generic;
using System.Linq;

namespace Contents.Utility
{
    public class std
    {
        public interface INeed
        {
            void Print(string text);
            string[] ReadAllLines(string path);
        }

        INeed need;

        public std(INeed need) => this.need = need;
        public void print(object text) => need.Print(text.ToString());
        public void print<T>(T[] text) => need.Print(str(text));
        public void print<T>(T[][] text) => need.Print(string.Join(Environment.NewLine, text.Select(e => str(e))));
        public string str<T>(params T[] args) => "[" + string.Join(", ", args) + "]";
        public string[] read_lines(string path) => need.ReadAllLines(path);
    }

    public static class std_ext
    {
        public static double[][] add(this double[][] l, double[] r) => l.calc(r, (x, y) => x + y);

        public static double[] add(this double[] l, double[] r) => l.calc(r, (x, y) => x + y);

        public static double[][] mul(this double[][] l, double r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, double r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, int[] r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, double[] r) => l.calc(r, (x, y) => x * y);

        public static double[] minus(this double[] src) => src.Select(e => -e).ToArray();

        public static double[][] minus(this double[][] l, double[][] r) => l.calc(r, (x, y) => x - y);

        public static double[] minus(this double[] l, double[] r) => l.calc(r, (x, y) => x - y);

        public static T[] get_col<T>(this T[][] rowCol, int index) => rowCol.Select(e => e[index]).ToArray();

        public static T[] skip_take<T>(this T[] src, int start, int count)
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(src[start + i]);
            }
            return list.ToArray();
        }

        static T1[] calc<T1, T2>(this T1[] l, T2 r, Func<T1, T2, T1> f) => l.Select(e => f(e, r)).ToArray();

        static T1[][] calc<T1, T2>(this T1[][] l, T2 r, Func<T1, T2, T1> f) => l.Select(e => e.calc(r, f)).ToArray();

        static T1[][] calc<T1, T2>(this T1[][] l, T2[] r, Func<T1, T2, T1> f) => l.Select(e => e.calc(r, f)).ToArray();

        static T1[][] calc<T1, T2>(this T1[][] l, T2[][] r, Func<T1, T2, T1> f)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            return l.Select((x, i) => x.calc(r[i], f)).ToArray();
        }

        static T1[] calc<T1, T2>(this T1[] l, T2[] r, Func<T1, T2, T1> f)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            return l.Select((x, i) => f(x, r[i])).ToArray();
        }

    }
}
