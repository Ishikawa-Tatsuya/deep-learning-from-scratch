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
        public static T[] choice<T>(this T[] src, int[] mask)
        {
            var dst = new T[mask.Length];
            for (int i = 0; i < mask.Length; i++)
            {
                dst[i] = src[mask[i]];
            }
            return dst;
        }

        public static double[][] add(this double[][] l, byte[] r) => l.calc(r, (x, y) => x + y);
        public static double[][] add(this double[][] l, double[] r) => l.calc(r, (x, y) => x + y);

        public static double[] add(this double[] l, double[] r) => l.calc(r, (x, y) => x + y);

        public static Array mul(this Array l, double r)
        {
            if (l is double[] a1) return a1.mul(r);
            if (l is double[][] a2) return a2.mul(r);
            throw new NotSupportedException();
        }

        public static double[][] mul(this double[][] l, double[][] r) => l.calc(r, (x, y) => x * y);

        public static double[][] mul(this double[][] l, double r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, double r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, int[] r) => l.calc(r, (x, y) => x * y);

        public static double[] mul(this double[] l, double[] r) => l.calc(r, (x, y) => x * y);


        public static Array div(this Array l, double r)
        {
            if (l is double[] a1) return a1.div(r);
            if (l is double[][] a2) return a2.div(r);
            throw new NotSupportedException();
        }

        //0割？
        public static double[] div(this double[] l, double r) => l.calc(r, (x, y) => x / y);
        public static double[][] div(this double[][] l, double r) => l.calc(r, (x, y) => x / y);
        public static double[][] div(this double[][] l, double[] r) => l.calc(r, (x, y) => x / y);

        public static Array minus(this Array l, Array r)
        {
            if (l is double[] a1) return a1.minus((double[])r);
            if (l is double[][] a2) return a2.minus((double[][])r);
            throw new NotSupportedException();
        }

        public static double[] minus(this double[] src) => src.Select(e => -e).ToArray();

        public static double[][] minus(this double[][] l, double[][] r) => l.calc(r, (x, y) => x - y);

        public static double[][] minus(this double[][] l, double[] r) => l.calc(r, (x, y) => x - y);

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

        public static double[][] todouble(this byte[][] src) => src.Select(e1 => e1.Select(e2 => (double)e2).ToArray()).ToArray();

        public static Tuple<T1, T2>[] pair<T1, T2>(this T1[] a1, T2[] a2)
        {
            if (a1.Length != a2.Length) throw new NotSupportedException();
            return a1.Select((e, i) => Tuple.Create(e, a2[i])).ToArray();
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
