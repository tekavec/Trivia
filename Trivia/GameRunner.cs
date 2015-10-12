using System;

namespace Trivia
{
    public class GameRunner
    {

        public static void Main(string[] args)
        {
            var gameWrapper = new Game();
            gameWrapper.PerformGame();
        }
    }
}