using System;

namespace Trivia.Infrastructure
{
    public class GamePrinter
    {
        private readonly Action<string> _writeLine;

        public GamePrinter(
            Action<string> writeLine)
        {
            _writeLine = writeLine;
        }

        public void PrintoutPlayers(string[] players)
        {
            for (var i = 0; i < players.Length; i++)
            {
                _writeLine(players[i] + " was added");
                _writeLine("They are player number " + (i + 1));
            }
        }

        public void PrintoutCurrentPlayerRolling(
            int roundRollValue,
            string playerName)
        {
            _writeLine(playerName + " is the current player");
            _writeLine("They have rolled a " + roundRollValue);
        }

        public void PrintoutCorrectAnswer(
            string playerName,
            int coins)
        {
            _writeLine("Answer was correct!!!!");
            _writeLine($"{playerName} now has {coins} Gold Coins.");
        }

        public void PrintoutGettingOutFromPenaltyBox(string playerName)
        {
            _writeLine($"{playerName} is getting out of the penalty box");
        }

        public void PrintoutNotGettingOutFromPenaltyBox(string playerName)
        {
            _writeLine($"{playerName} is not getting out of the penalty box");
        }

        public void PrintoutCurrentPlayerNewLocationAndQuestion(
            string playerName,
            int playerLocation, 
            string categoryName, 
            string question)
        {
            _writeLine($"{playerName}'s new location is {playerLocation}");
            _writeLine($"The category is {categoryName}");
            _writeLine(question);
        }

        public void PrintoutMessageForIncorrectlyAnsweredQuestion(
            string playerName)
        {
            _writeLine("Question was incorrectly answered");
            _writeLine($"{playerName} was sent to the penalty box");
        }
    }
}