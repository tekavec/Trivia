using System;
using System.Text;
using Trivia.Infrastructure;
using Trivia.Model.Players;
using Trivia.Model.Questions;

namespace Trivia.Application
{
    public class Game
    {
        private readonly GameEngine _gameEngine;
        private readonly GamePrinter _gamePrinter;
        private readonly IPlayers _players;
        private readonly QuestionRepository _questions = new QuestionRepository(50);

        public Game(Action<string> writeLine, string[] players)
        {
            _players = new Players();
            CreatePlayers(players);
            _gameEngine = new GameEngine(_players, new GamePrinter(writeLine));
            _gamePrinter = new GamePrinter(writeLine);
            _gamePrinter.PrintoutPlayers(players);
        }

        private void CreatePlayers(string[] players)
        {
            Array.ForEach(players, player => _players.Add(new Player(player, 0, 0)));
        }

        public bool RollOneRound(Random rand, Action<string> writeLine)
        {
            var newLocation = rand.Next(5) + 1;
            var isLuckyRoll = (newLocation % 2 != 0);
            var isPenalizingRoll = rand.Next(9) == 7;
            var _messageCollector = new StringBuilder();

            var currentPlayer = _gameEngine.CurrentPlayer();

            new PlayerRollingMessage(newLocation, _gameEngine.GetCurrentPlayerName()).AppendMessageTo(_messageCollector);
            new PenaltyBoxMessage(_gameEngine.IsCurrentPlayerInPenaltyBox(), isLuckyRoll, _gameEngine.GetCurrentPlayerName())
                        .AppendMessageTo(_messageCollector);

            var report = _messageCollector.ToString();
            writeLine(report.Remove(report.LastIndexOf(Environment.NewLine)));
            
            if ((_gameEngine.IsCurrentPlayerInPenaltyBox() && isLuckyRoll) 
                || !_gameEngine.IsCurrentPlayerInPenaltyBox())
            {
                _gameEngine.ChangeCurrentPlayerLocation(newLocation);
                var currentCategory = _questions.GetCategoryBy(_gameEngine.GetCurrentPlayerLocation());
                _gamePrinter.PrintoutCurrentPlayerNewLocationAndQuestion(
                    _gameEngine.GetCurrentPlayerName(),
                    _gameEngine.GetCurrentPlayerLocation(),
                    _questions.GetCategoryNameBy(_gameEngine.GetCurrentPlayerLocation()),
                    _questions.GetNextQuestionBy(currentCategory));
                _questions.RemoveFirstQuestionOf(currentCategory);
            }
            if (isPenalizingRoll)
            {
                _gamePrinter.PrintoutMessageForIncorrectlyAnsweredQuestion(_gameEngine.GetCurrentPlayerName());
                _gameEngine.PenalizeCurrentPlayer();
                _gameEngine.SetNextCurrentPlayer();
                return true;
            }
            if (_gameEngine.IsCurrentPlayerInPenaltyBox() && isLuckyRoll)
            {
                _gameEngine.GiveCoinToCurrentPlayer();
                _gamePrinter.PrintoutCorrectAnswer(_gameEngine.GetCurrentPlayerName(),
                    _gameEngine.CurrentPlayer().Coins());
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