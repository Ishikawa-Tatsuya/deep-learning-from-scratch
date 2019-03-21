using static Contents.Std;

namespace Contents.ch01
{
    public class Man
    {
        string name;

        public Man(string name)
        {
            this.name = name;
            print("Initilized!");
        }

        public void hello()
            => print("Hello " + name + "!");

        public void goodbye()
            => print("Good-bye " + name + "!");

        public static void Execute()
        {
            var m = new Man("David");
            m.hello();
            m.goodbye();
        }
    }
}
