using NUnit.Framework;
using Trivia.Model.Players;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayersShould
    {
        private IPlayers _players;
        private IPlayer _playerA;
        private IPlayer _playerB;
        private int _defaultLocation = 0;
        private int _defaultNumberOfCoins = 0;

        [SetUp]
        public void Init()
        {
            _players = new Players();
            _playerA = new Player("ana", _defaultLocation, _defaultNumberOfCoins);
            _playerB = new Player("bob", _defaultLocation, _defaultNumberOfCoins);
        }

        [Test]
        public void store_a_player()
        {
            _players.Add(_playerA);

            Assert.That(_players.GetNextPlayer(), Is.EqualTo(_playerA));
        }

        [Test]
        public void get_next_player()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            _players.GetNextPlayer();

            Assert.That(_players.GetNextPlayer(), Is.EqualTo(_playerB));
        }

    }
}