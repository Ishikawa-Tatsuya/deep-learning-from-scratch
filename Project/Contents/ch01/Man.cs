using Contents.Utility;

namespace Contents.ch01
{
    public class Man
    {
        public Std std { get; set; }

        public class Target
        {
            Std std;

            string name;

            public Target(Std std, string name)
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

        public void Execute()
        {
            var m = new Target(std, "David");
            m.hello();
            m.goodbye();
        }
    }
}
