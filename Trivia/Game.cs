using System;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerRepository _players = new PlayerRepository();
        private readonly QuestionRepository _questions = new QuestionRepository(50);
        private GamePrinter _gamePrinter;

        public Game(Action<string> writeLine, string[] players)
        {
            CreatePlayers(players);
            _gamePrinter = new GamePrinter(writeLine, _players, _questions);
            _gamePrinter.PrintoutPlayers();
        }

        private void CreatePlayers(string[] players)
        {
            Array.ForEach(players, player => _players.Add(new Player(player, 0, 0)));
        }

        public bool RollOneRound(Random rand, Action<string> writeLine)
        {
            int roundRollValue = rand.Next(5) + 1;
            var isGettingOutOfPenaltyBox = (roundRollValue % 2 != 0);
            var currentPlayer = _players.GetCurentPlayer();
            writeLine(currentPlayer + " is the current player");
            writeLine("They have rolled a " + roundRollValue);

            if (currentPlayer.IsInPenaltyBox())
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writeLine(currentPlayer + " is getting out of the penalty box");
                }
                else
                {
                    writeLine(currentPlayer + " is not getting out of the penalty box");
                }
            }

            if (currentPlayer.IsInPenaltyBox() && isGettingOutOfPenaltyBox || !currentPlayer.IsInPenaltyBox())
            {
                currentPlayer.ChangeLocationBy(roundRollValue);
                var currentCategory = _questions.CurrentCategory(_players.GetCurentPlayer().Location());
                writeLine(currentPlayer
                                     + "'s new location is "
                                     + currentPlayer.Location());
                writeLine("The category is " + currentCategory.GetDescription());
                writeLine(_questions.GetFirstQuestionBy(currentCategory));
                _questions.RemoveFirstQuestionOf(currentCategory);
            }
            if (rand.Next(9) == 7)
            {
                writeLine("Question was incorrectly answered");
                writeLine(currentPlayer + " was sent to the penalty box");
                currentPlayer.Penalize();
                _players.SetNextCurrentPlayer();
                return true;
            }
            bool roundDoesNotHaveWinner;
            if (currentPlayer.IsInPenaltyBox())
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writeLine("Answer was correct!!!!");
                    currentPlayer.IncreaseCoinsByOne();
                    writeLine(currentPlayer
                              + " now has "
                              + currentPlayer.Coins()
                              + " Gold Coins.");

                    roundDoesNotHaveWinner = !currentPlayer.IsWinning();
                    _players.SetNextCurrentPlayer();
                    return roundDoesNotHaveWinner;
                }
                _players.SetNextCurrentPlayer();
                return true;
            }
            writeLine("Answer was corrent!!!!");
            currentPlayer.IncreaseCoinsByOne();
            writeLine(currentPlayer
                      + " now has "
                      + currentPlayer.Coins()
                      + " Gold Coins.");
            roundDoesNotHaveWinner = !currentPlayer.IsWinning();
            _players.SetNextCurrentPlayer();
            return roundDoesNotHaveWinner;
        }
    }
}