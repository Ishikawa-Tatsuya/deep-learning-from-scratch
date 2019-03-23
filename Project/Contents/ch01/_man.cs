using Contents.Utility;

namespace Contents.ch01
{
    public class man
    {
        public std std { get; set; }

        public class target
        {
            std std;

            string name;

            public target(std std, string name)
            {
                this.std = std;
                this.name = name;
                std.print("Initilized!");
            }

            public void hello()
                => std.print("Hello " + name + "!");

            public void goodbye()
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
