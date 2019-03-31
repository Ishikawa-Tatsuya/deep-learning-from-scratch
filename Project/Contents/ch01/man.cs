using Contents.Utility;

namespace Contents.ch01
{
    public class man
    {
        public std std { get; set; }

        class target
        {
            std std;

            string name;

            internal target(std std, string name)
            {
                this.std = std;
                this.name = name;
                std.print("Initilized!");
            }

            internal void hello()
                => std.print("Hello " + name + "!");

            internal void goodbye()
                => std.print("Good-bye " + name + "!");

        }

        public void main()
        {
            var m = new target(std, "David");
            m.hello();
            m.goodbye();
        }
    }
}
