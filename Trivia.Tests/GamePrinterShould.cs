using NSubstitute;
using NUnit.Framework;
using Trivia.Infrastructure;
using Trivia.Model.Questions;

namespace Trivia.Tests
{
    [TestFixture]
    public class GamePrinterShould
    {
        private readonly IMyConsole _console = Substitute.For<IMyConsole>();
        private QuestionRepository _questions;
        private GamePrinter _gamePrinter;
        private string _playerName = "ana";
        private int _roundRollValue = 42;
        private int _aLocation = 33;
        private int _numberOfCoins = 5;
        private string _aCategoryName = "Pop";
        private string _aQuestion = "A question";

        [SetUp]
        public void Init()
        {
            _questions = new QuestionRepository(1);
            _gamePrinter = new GamePrinter(_console.WriteLine);
        }

        [Test]
        public void printout_players_setup()
        {
            _gamePrinter.PrintoutPlayers(new []{ _playerName, _playerName });

            Received.InOrder(() =>
            {
                _console.WriteLine($"{_playerName} was added");
                _console.WriteLine("They are player number 1");
                _console.WriteLine($"{_playerName} was added");
                _console.WriteLine("They are player number 2");
            }
                );
        }

        [Test]
        public void printout_current_player_rolling()
        {
            _gamePrinter.PrintoutCurrentPlayerRolling(_roundRollValue, _playerName);

            Received.InOrder(() =>
            {
                _console.WriteLine($"{_playerName} is the current player");
                _console.WriteLine($"They have rolled a {_roundRollValue}");
            }
            );
        }

        [Test]
        public void printout_messages_for_the_correct_answer()
        {

            _gamePrinter.PrintoutCorrectAnswer(_playerName, _numberOfCoins);

            Received.InOrder(() =>
            {
                _console.WriteLine("Answer was correct!!!!");
                _console.WriteLine($"ana now has {_numberOfCoins} Gold Coins.");
            }
            );
        }

        [Test]
        public void printout_message_for_getting_out_of_the_penalty_box()
        {
            _gamePrinter.PrintoutGettingOutFromPenaltyBox(_playerName);

            _console.Received().WriteLine($"{_playerName} is getting out of the penalty box");
        }

        [Test]
        public void printout_message_for_not_getting_out_of_the_penalty_box()
        {
            _gamePrinter.PrintoutNotGettingOutFromPenaltyBox(_playerName);

            _console.Received().WriteLine($"{_playerName} is not getting out of the penalty box");
        }

        [Test]
        public void printout_message_for_changing_current_player_location_and_asking_a_question_from_the_chosen_category()
        {
            _gamePrinter.PrintoutCurrentPlayerNewLocationAndQuestion(_playerName, _aLocation, _aCategoryName, _aQuestion);

            Received.InOrder(() =>
            {
                _console.WriteLine($"{_playerName}'s new location is {_aLocation}");
                _console.WriteLine($"The category is {_aCategoryName}");
                _console.WriteLine(_aQuestion);
            });
        }

        [Test]
        public void printout_message_for_incorrectly_answered_question()
        {
            _gamePrinter.PrintoutMessageForIncorrectlyAnsweredQuestion(_playerName);

            Received.InOrder(() =>
            {
                _console.WriteLine("Question was incorrectly answered");
                _console.WriteLine($"{_playerName} was sent to the penalty box");
            });
        }

    }
}