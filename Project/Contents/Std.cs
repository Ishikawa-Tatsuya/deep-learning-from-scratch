using System;

namespace Contents
{
    public static class Std
    {
        public interface INeed
        {
            void Print(string text);
        }

        public static INeed Need { get; set; }
        
        public static void print(string text) => Need.Print(text);
    }
}
