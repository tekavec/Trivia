using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerRepositoryShould
    {
        private PlayerRepository _players;
        private Player _playerA;
        private Player _playerB;

        [SetUp]
        public void Init()
        {
            _players = new PlayerRepository();
            _playerA = new Player("ana", 0, 0);
            _playerB = new Player("bob", 0, 0);
        }

        [Test]
        public void store_a_player()
        {
            _players.Add(_playerA);

            Assert.That(_players.Contains(_playerA), Is.True);
        }

        [Test]
        public void count_all_players()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            Assert.That(_players.Count(), Is.EqualTo(2));
        }

        [Test]
        public void get_player_by_index()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            Assert.That(_players.GetPlayerByIndex(1), Is.EqualTo(_playerB));
        }

        [Test]
        public void get_default_current_player()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            Assert.That(_players.GetCurentPlayer(), Is.EqualTo(_playerA));
        }

        [Test]
        public void change_current_player()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            _players.SetNextCurrentPlayer();

            Assert.That(_players.GetCurentPlayer(), Is.EqualTo(_playerB));
        }

        [Test]
        public void change_current_player_to_first_if_next_player_index_is_greater_than_player_count()
        {
            _players.Add(_playerA);
            _players.Add(_playerB);

            _players.SetNextCurrentPlayer();
            _players.SetNextCurrentPlayer();

            Assert.That(_players.GetCurentPlayer(), Is.EqualTo(_playerA));
        }

    }
}