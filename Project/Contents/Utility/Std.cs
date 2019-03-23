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
        public string str<T>(params T[] args) => "[" + string.Join(", ", args) + "]";
        public string[] read_lines(string path) => need.ReadAllLines(path);
    }

    public static class std_ext
    {
        public static double[][] add(this double[][] l, double[] r) => l.Select(e=>e.add(r)).ToArray();

        public static double[] add(this double[] l, double[] r)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            var list = new List<double>();
            for (int i = 0; i < l.Length; i++)
            {
                list.Add(l[i] + r[i]);
            }
            return list.ToArray();
        }

        public static double[] mul(this double[] l, int[] r) => mul(l, r.Select(e=>(double)e).ToArray());

        public static double[] mul(this double[] l, double[] r)
        {
            if (l.Length != r.Length) throw new NotSupportedException();
            var list = new List<double>();
            for (int i = 0; i < l.Length; i++)
            {
                list.Add(l[i] * r[i]);
            }
            return list.ToArray();
        }
        
        public static T[] skip_take<T>(this T[] src, int start, int count)
        {
            var list = new List<T>();
            for (int i = 0; i < count; i++)
            {
                list.Add(src[start + i]);
            }
            return list.ToArray();
        }
    }
}
