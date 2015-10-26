using NSubstitute;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class GamePrinterShould
    {
        private readonly IMyConsole _console = Substitute.For<IMyConsole>();
        private PlayerRepository _players;
        private QuestionRepository _questions;
        private GamePrinter _gamePrinter;

        [SetUp]
        public void Init()
        {
            _questions = new QuestionRepository(1);
            _players = new PlayerRepository();
            _gamePrinter = new GamePrinter(_console.WriteLine, _players, _questions);
            
        }
        [Test]
        public void printout_players_setup()
        {
            _players.Add(new Player("ana", 0, 0));

            _gamePrinter.PrintoutPlayers();

            Received.InOrder(() =>
                {
                    _console.WriteLine("ana was added");
                    _console.WriteLine("They are player number 1");
                }
            );
        }
    }
}