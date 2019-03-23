using Contents.Utility;

namespace Contents.ch01
{
    public class Hungry
    {
        public Std std { get; set; }

        public void Execute()
            => std.print("I'm hungry!");
    }
}
