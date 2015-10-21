using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private int _playerIndex;
        private readonly PlayerRepository _players = new PlayerRepository();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();

        public Game(Action<string> writeLine, string[] players)
        {
            CreatePlayers(writeLine, players);
            CreateQuestions();
        }

        private void CreateQuestions()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast("Rock Question " + i);
            }
        }

        private void CreatePlayers(Action<string> writeLine, string[] players)
        {
            foreach (var player in players)
            {
                AddPlayer(player, writeLine);
            }
        }

        private void AddPlayer(string playerName, Action<string> writeLine)
        {
            _players.Add(new Player(playerName, 0, 0));
            writeLine(playerName + " was added");
            writeLine("They are player number " + _players.Count());
        }

        public bool RollOneRound(Random rand, Action<string> writeLine)
        {
            int roundRollValue = rand.Next(5) + 1;
            var currentPlayer = _players.GetPlayerByIndex(_playerIndex);
            writeLine(currentPlayer + " is the current player");
            writeLine("They have rolled a " + roundRollValue);
            var isGettingOutOfPenaltyBox = (roundRollValue % 2 != 0);

            if (currentPlayer.IsInPenaltyBox())
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writeLine(currentPlayer + " is getting out of the penalty box");
                    RearrangePlaces(writeLine, roundRollValue, currentPlayer);
                    AskQuestionByCurrentCategory(writeLine);
                }
                else
                {
                    writeLine(currentPlayer + " is not getting out of the penalty box");
                }
            }
            if (!currentPlayer.IsInPenaltyBox())
            {
                RearrangePlaces(writeLine, roundRollValue, currentPlayer);
                AskQuestionByCurrentCategory(writeLine);
            }
            if (rand.Next(9) == 7)
            {
                writeLine("Question was incorrectly answered");
                writeLine(currentPlayer + " was sent to the penalty box");
                currentPlayer.Penalize();

                _playerIndex++;
                if (_playerIndex == _players.Count()) _playerIndex = 0;
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
                    IncreasePlayerIndex();
                    return roundDoesNotHaveWinner;
                }
                IncreasePlayerIndex();
                return true;
            }
            writeLine("Answer was corrent!!!!");
            currentPlayer.IncreaseCoinsByOne();
            writeLine(currentPlayer
                      + " now has "
                      + currentPlayer.Coins()
                      + " Gold Coins.");
            roundDoesNotHaveWinner = !currentPlayer.IsWinning();
            IncreasePlayerIndex();
            return roundDoesNotHaveWinner;
        }

        private void IncreasePlayerIndex()
        {
            _playerIndex++;
            if (_playerIndex == _players.Count())
            {
                _playerIndex = 0;
            }
        }

        private void RearrangePlaces(
            Action<string> writeLine, 
            int roundRollValue, 
            Player currentPlayer)
        {
            currentPlayer.ChangeLocationBy(roundRollValue);
            writeLine(currentPlayer
                      + "'s new location is "
                      + currentPlayer.Location());
        }

        private void AskQuestionByCurrentCategory(Action<string> writeLine)
        {
            var currentCategory = CurrentCategory();
            writeLine("The category is " + currentCategory);
            if (currentCategory == "Pop")
            {
                writeLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (currentCategory == "Science")
            {
                writeLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (currentCategory == "Sports")
            {
                writeLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (currentCategory == "Rock")
            {
                writeLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            var currentPlayer = _players.GetPlayerByIndex(_playerIndex);
            if (currentPlayer.Location() == 0) return "Pop";
            if (currentPlayer.Location() == 4) return "Pop";
            if (currentPlayer.Location() == 8) return "Pop";
            if (currentPlayer.Location() == 1) return "Science";
            if (currentPlayer.Location() == 5) return "Science";
            if (currentPlayer.Location() == 9) return "Science";
            if (currentPlayer.Location() == 2) return "Sports";
            if (currentPlayer.Location() == 6) return "Sports";
            if (currentPlayer.Location() == 10) return "Sports";
            return "Rock";
        }
    }
}