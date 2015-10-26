using System;

namespace Trivia
{
    public class Game
    {
        private int _playerIndex;
        private readonly PlayerRepository _players = new PlayerRepository();
        private readonly QuestionRepository _questionRepository = new QuestionRepository();

        public Game(Action<string> writeLine, string[] players)
        {
            CreatePlayers(writeLine, players);
            CreateQuestions();
        }

        private void CreateQuestions()
        {
            for (var i = 0; i < 50; i++)
            {
                _questionRepository.Add(new Question(QuestionCategory.Pop, "Pop Question " + i));
                _questionRepository.Add(new Question(QuestionCategory.Science, "Science Question " + i));
                _questionRepository.Add(new Question(QuestionCategory.Sports, "Sports Question " + i));
                _questionRepository.Add(new Question(QuestionCategory.Rock, "Rock Question " + i));
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
            var location = _players.GetPlayerByIndex(_playerIndex).Location();
            var currentCategory = _questionRepository.CurrentCategory(location);
            writeLine("The category is " + currentCategory.GetDescription());
            writeLine(_questionRepository.GetFirstQuestionBy(currentCategory));
            _questionRepository.RemoveFirstQuestionOf(currentCategory);
        }

    }
}