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
            var game = new Game(writeLine,new[] { "Chet","Pat", "Sue"});
            //game.AddPlayer( writeLine);
            //game.AddPlayer(writeLine);
            //game.AddPlayer(writeLine);
            do
            {
            } while (game.RollOneRound(rand, writeLine));
        }
    }
}