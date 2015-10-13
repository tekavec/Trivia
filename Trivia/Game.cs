using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private int currentPlayer;

        private readonly bool[] inPenaltyBox = new bool[6];
        private bool isGettingOutOfPenaltyBox;

        private readonly int[] places = new int[6];


        private readonly List<string> players = new List<string>();

        private readonly LinkedList<string> popQuestions = new LinkedList<string>();
        private readonly int[] purses = new int[6];
        private readonly LinkedList<string> rockQuestions = new LinkedList<string>();
        private readonly LinkedList<string> scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> sportsQuestions = new LinkedList<string>();

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                popQuestions.AddLast("Pop Question " + i);
                scienceQuestions.AddLast(("Science Question " + i));
                sportsQuestions.AddLast(("Sports Question " + i));
                rockQuestions.AddLast(createRockQuestion(i));
            }
        }

        public string createRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool isPlayable()
        {
            return (howManyPlayers() >= 2);
        }

        public bool add(string playerName)
        {
            players.Add(playerName);
            places[howManyPlayers()] = 0;
            purses[howManyPlayers()] = 0;
            inPenaltyBox[howManyPlayers()] = false;

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + players.Count);
            return true;
        }

        public int howManyPlayers()
        {
            return players.Count;
        }

        public void roll(int roll, Action<string> writeLine)
        {
            writeLine(players[currentPlayer] + " is the current player");
            writeLine("They have rolled a " + roll);

            if (inPenaltyBox[currentPlayer])
            {
                if (roll%2 != 0)
                {
                    isGettingOutOfPenaltyBox = true;

                    writeLine(players[currentPlayer] + " is getting out of the penalty box");
                    places[currentPlayer] = places[currentPlayer] + roll;
                    if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                    writeLine(players[currentPlayer]
                                      + "'s new location is "
                                      + places[currentPlayer]);
                    writeLine("The category is " + currentCategory());
                    if (currentCategory() == "Pop")
                    {
                        writeLine(popQuestions.First());
                        popQuestions.RemoveFirst();
                    }
                    if (currentCategory() == "Science")
                    {
                        writeLine(scienceQuestions.First());
                        scienceQuestions.RemoveFirst();
                    }
                    if (currentCategory() == "Sports")
                    {
                        writeLine(sportsQuestions.First());
                        sportsQuestions.RemoveFirst();
                    }
                    if (currentCategory() == "Rock")
                    {
                        writeLine(rockQuestions.First());
                        rockQuestions.RemoveFirst();
                    }
                }
                else
                {
                    writeLine(players[currentPlayer] + " is not getting out of the penalty box");
                    isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                places[currentPlayer] = places[currentPlayer] + roll;
                if (places[currentPlayer] > 11) places[currentPlayer] = places[currentPlayer] - 12;

                writeLine(players[currentPlayer]
                                  + "'s new location is "
                                  + places[currentPlayer]);
                writeLine("The category is " + currentCategory());
                if (currentCategory() == "Pop")
                {
                    writeLine(popQuestions.First());
                    popQuestions.RemoveFirst();
                }
                if (currentCategory() == "Science")
                {
                    writeLine(scienceQuestions.First());
                    scienceQuestions.RemoveFirst();
                }
                if (currentCategory() == "Sports")
                {
                    writeLine(sportsQuestions.First());
                    sportsQuestions.RemoveFirst();
                }
                if (currentCategory() == "Rock")
                {
                    writeLine(rockQuestions.First());
                    rockQuestions.RemoveFirst();
                }
            }
        }


        private string currentCategory()
        {
            if (places[currentPlayer] == 0) return "Pop";
            if (places[currentPlayer] == 4) return "Pop";
            if (places[currentPlayer] == 8) return "Pop";
            if (places[currentPlayer] == 1) return "Science";
            if (places[currentPlayer] == 5) return "Science";
            if (places[currentPlayer] == 9) return "Science";
            if (places[currentPlayer] == 2) return "Sports";
            if (places[currentPlayer] == 6) return "Sports";
            if (places[currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool wasCorrectlyAnswered(Action<string> writeLine)
        {
            bool winner;
            if (inPenaltyBox[currentPlayer])
            {
                if (isGettingOutOfPenaltyBox)
                {
                    writeLine("Answer was correct!!!!");
                    purses[currentPlayer]++;
                    writeLine(players[currentPlayer]
                                      + " now has "
                                      + purses[currentPlayer]
                                      + " Gold Coins.");

                    winner = didPlayerWin();
                    currentPlayer++;
                    if (currentPlayer == players.Count) currentPlayer = 0;

                    return winner;
                }
                currentPlayer++;
                if (currentPlayer == players.Count) currentPlayer = 0;
                return true;
            }
            writeLine("Answer was corrent!!!!");
            purses[currentPlayer]++;
            writeLine(players[currentPlayer]
                              + " now has "
                              + purses[currentPlayer]
                              + " Gold Coins.");

            winner = didPlayerWin();
            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;

            return winner;
        }

        public bool wrongAnswer(Action<string> writeLine)
        {
            writeLine("Question was incorrectly answered");
            writeLine(players[currentPlayer] + " was sent to the penalty box");
            inPenaltyBox[currentPlayer] = true;

            currentPlayer++;
            if (currentPlayer == players.Count) currentPlayer = 0;
            return true;
        }


        private bool didPlayerWin()
        {
            return purses[currentPlayer] != 6;
        }
    }
}