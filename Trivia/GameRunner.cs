using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;

        public static void Main(string[] args)
        {
            RunGame(new Random(), Console.WriteLine);
        }

        public static void RunGame(Random rand, Action<string> writeLine)
        {
            var aGame = new Game();

            aGame.add("Chet");
            aGame.add("Pat");
            aGame.add("Sue");
            do
            {
                var roll = rand.Next(5) + 1;
                aGame.roll(roll, writeLine);

                var next = rand.Next(9);
                if (next == 7)
                {
                    _notAWinner = aGame.wrongAnswer(writeLine);
                }
                else
                {
                    _notAWinner = aGame.wasCorrectlyAnswered(writeLine);
                }
            } while (_notAWinner);
        }
    }
}