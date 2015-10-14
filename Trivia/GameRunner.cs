using System;

namespace Trivia
{
    public class GameRunner
    {

        public static void Main(string[] args)
        {
            RunGame(new Random(), Console.WriteLine);
        }

        public static void RunGame(Random rand, Action<string> writeLine)
        {
            var game = new Game();
            game.AddPlayer("Chet", writeLine);
            game.AddPlayer("Pat", writeLine);
            game.AddPlayer("Sue", writeLine);
            do
            {
            } while (game.Roll(rand, writeLine));
        }
    }
}