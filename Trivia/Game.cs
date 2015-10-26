using System;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly PlayerRepository _players = new PlayerRepository();
        private readonly QuestionRepository _questionRepository = new QuestionRepository(50);

        public Game(Action<string> writeLine, string[] players)
        {
            CreatePlayers(players);
            PrintoutPlayers(writeLine);
        }

        private void CreatePlayers(string[] players)
        {
            foreach (var player in players)
            {
                _players.Add(new Player(player, 0, 0));
            }
        }

        private void PrintoutPlayers(Action<string> writeLine)
        {
            for (int i = 0; i < _players.Count(); i++)
            {
                writeLine(_players.GetPlayerByIndex(i) + " was added");
                writeLine("They are player number " + (i+1));
            }
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
                var currentCategory = _questionRepository.CurrentCategory(_players.GetCurentPlayer().Location());
                writeLine(currentPlayer
                                     + "'s new location is "
                                     + currentPlayer.Location());
                writeLine("The category is " + currentCategory.GetDescription());
                writeLine(_questionRepository.GetFirstQuestionBy(currentCategory));
                _questionRepository.RemoveFirstQuestionOf(currentCategory);
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