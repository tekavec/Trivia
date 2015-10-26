using System;

namespace Trivia
{
    public class GamePrinter
    {
        private readonly Action<string> _writeLine;
        private readonly PlayerRepository _players;
        private readonly QuestionRepository _questions;

        public GamePrinter(
            Action<string> writeLine, 
            PlayerRepository players, 
            QuestionRepository questions)
        {
            _writeLine = writeLine;
            _players = players;
            _questions = questions;
        }

        public void PrintoutPlayers()
        {
            for (int i = 0; i < _players.Count(); i++)
            {
                _writeLine(_players.GetPlayerByIndex(i) + " was added");
                _writeLine("They are player number " + (i + 1));
            }
        }
    }
}