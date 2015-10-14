namespace Trivia
{
    public interface IMyConsole
    {
        void WriteLine(string line);
        void WriteLine();
    }

    public class MyConsole : IMyConsole
    {
        public void WriteLine(string line)
        {
            System.Console.WriteLine(line);
        }

        public void WriteLine()
        {
            System.Console.WriteLine();
        }
    }
}