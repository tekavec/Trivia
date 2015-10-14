using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private int _currentPlayer;
        private readonly bool[] _inPenaltyBox = new bool[6];
        private bool _isGettingOutOfPenaltyBox;
        private readonly int[] _places = new int[6];
        private readonly List<string> _players = new List<string>();
        private readonly int[] _purses = new int[6];
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast("Rock Question " + i);
            }
        }

        public bool AddPlayer(string playerName, Action<string> writeLine)
        {
            _players.Add(playerName);
            _places[_players.Count] = 0;
            _purses[_players.Count] = 0;
            _inPenaltyBox[_players.Count] = false;

            writeLine(playerName + " was added");
            writeLine("They are player number " + _players.Count);
            return true;
        }

        public bool Roll(Random rand, Action<string> writeLine)
        {
            int roll = rand.Next(5) + 1;
            writeLine(_players[_currentPlayer] + " is the current player");
            writeLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                if (roll%2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    writeLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    writeLine(_players[_currentPlayer]
                                      + "'s new location is "
                                      + _places[_currentPlayer]);
                    AskQuestionByCurrentCategory(writeLine);
                }
                else
                {
                    writeLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                writeLine(_players[_currentPlayer]
                                  + "'s new location is "
                                  + _places[_currentPlayer]);
                AskQuestionByCurrentCategory(writeLine);

            }
            if (rand.Next(9) == 7)
            {
                return WrongAnswer(writeLine);
            }
            else
            {
                return WasCorrectlyAnswered(writeLine);
            }

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
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered(Action<string> writeLine)
        {
            bool winner;
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    writeLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    writeLine(_players[_currentPlayer]
                                      + " now has "
                                      + _purses[_currentPlayer]
                                      + " Gold Coins.");

                    winner = _purses[_currentPlayer] != 6;
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;
                return true;
            }
            writeLine("Answer was corrent!!!!");
            _purses[_currentPlayer]++;
            writeLine(_players[_currentPlayer]
                              + " now has "
                              + _purses[_currentPlayer]
                              + " Gold Coins.");

            winner = _purses[_currentPlayer] != 6;
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return winner;
        }

        public bool WrongAnswer(Action<string> writeLine)
        {
            writeLine("Question was incorrectly answered");
            writeLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }
    }
}