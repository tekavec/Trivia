using System;
using Trivia.Infrastructure;
using Trivia.Model.Players;
using Trivia.Model.Questions;

namespace Trivia.Application
{
    public class Game
    {
        private readonly IPlayers _players;
        private readonly GameEngine _gameEngine;
        private readonly QuestionRepository _questions = new QuestionRepository(50);
        private readonly GamePrinter _gamePrinter;

        public Game(Action<string> writeLine, string[] players)
        {
            _players = new Players();
            CreatePlayers(players);
            _gameEngine = new GameEngine(_players);
            _gamePrinter = new GamePrinter(writeLine);
            _gamePrinter.PrintoutPlayers(players);
        }

        private void CreatePlayers(string[] players)
        {
            Array.ForEach(players, player => _players.Add(new Player(player, 0, 0)));
        }

        public bool RollOneRound(Random rand, Action<string> writeLine)
        {
            int roundRollValue = rand.Next(5) + 1;
            var isLuckyRoll = (roundRollValue % 2 != 0);
            var currentPlayer = _gameEngine.CurrentPlayer();
            _gamePrinter.PrintoutCurrentPlayerRolling(roundRollValue, _gameEngine.GetCurrentPlayerName());

            if (_gameEngine.IsCurrentPlayerInPenaltyBox())
            {
                if (isLuckyRoll)
                {
                    _gamePrinter.PrintoutGettingOutFromPenaltyBox(_gameEngine.GetCurrentPlayerName());
                }
                else
                {
                    _gamePrinter.PrintoutNotGettingOutFromPenaltyBox(_gameEngine.GetCurrentPlayerName());
                }
            }

            if ((_gameEngine.IsCurrentPlayerInPenaltyBox() && isLuckyRoll) || !_gameEngine.IsCurrentPlayerInPenaltyBox())
            {
                _gameEngine.ChangeCurrentPlayerLocation(roundRollValue);
                var currentCategory = _questions.GetCategoryBy(_gameEngine.GetCurrentPlayerLocation());
                _gamePrinter.PrintoutCurrentPlayerNewLocationAndQuestion(
                    _gameEngine.GetCurrentPlayerName(),
                    _gameEngine.GetCurrentPlayerLocation(),
                    _questions.GetCategoryNameBy(_gameEngine.GetCurrentPlayerLocation()),
                    _questions.GetFirstQuestionBy(currentCategory));
                _questions.RemoveFirstQuestionOf(currentCategory);
            }
            if (rand.Next(9) == 7)
            {
                _gamePrinter.PrintoutMessageForIncorrectlyAnsweredQuestion(_gameEngine.GetCurrentPlayerName());
                _gameEngine.PenalizeCurrentPlayer();
                _gameEngine.SetNextCurrentPlayer();
                return true;
            }
            if (_gameEngine.IsCurrentPlayerInPenaltyBox() && isLuckyRoll)
            {
                _gameEngine.GiveCoinToCurrentPlayer();
                _gamePrinter.PrintoutCorrectAnswer(_gameEngine.GetCurrentPlayerName(), _gameEngine.CurrentPlayer().Coins());
                _gameEngine.SetNextCurrentPlayer();
                return currentPlayer.IsNotWinning();
            }
            if (_gameEngine.IsCurrentPlayerInPenaltyBox())
            {
                _gameEngine.SetNextCurrentPlayer();
                return true;
            }
            _gameEngine.GiveCoinToCurrentPlayer();
            _gamePrinter.PrintoutCorrectAnswer(_gameEngine.GetCurrentPlayerName(), _gameEngine.CurrentPlayer().Coins());
            _gameEngine.SetNextCurrentPlayer();
            return currentPlayer.IsNotWinning();
        }
    }
}