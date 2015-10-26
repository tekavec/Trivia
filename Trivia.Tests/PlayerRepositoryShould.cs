using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerRepositoryShould
    {
        private PlayerRepository _playerRepository;
        private Player _playerA;
        private Player _playerB;

        [SetUp]
        public void Init()
        {
            _playerRepository = new PlayerRepository();
            _playerA = new Player("ana", 0, 0);
            _playerB = new Player("bob", 0, 0);
        }

        [Test]
        public void store_a_player()
        {
            _playerRepository.Add(_playerA);

            Assert.That(_playerRepository.Contains(_playerA), Is.True);
        }

        [Test]
        public void count_all_players()
        {
            _playerRepository.Add(_playerA);
            _playerRepository.Add(_playerB);

            Assert.That(_playerRepository.Count(), Is.EqualTo(2));
        }

        [Test]
        public void get_player_by_index()
        {
            _playerRepository.Add(_playerA);
            _playerRepository.Add(_playerB);

            Assert.That(_playerRepository.GetPlayerByIndex(1), Is.EqualTo(_playerB));
        }

        [Test]
        public void get_default_current_player()
        {
            _playerRepository.Add(_playerA);
            _playerRepository.Add(_playerB);

            Assert.That(_playerRepository.GetCurentPlayer(), Is.EqualTo(_playerA));
        }

        [Test]
        public void change_current_player()
        {
            _playerRepository.Add(_playerA);
            _playerRepository.Add(_playerB);

            _playerRepository.SetNextCurrentPlayer();

            Assert.That(_playerRepository.GetCurentPlayer(), Is.EqualTo(_playerB));
        }

        [Test]
        public void change_current_player_to_first_if_next_player_index_is_greater_than_player_count()
        {
            _playerRepository.Add(_playerA);
            _playerRepository.Add(_playerB);

            _playerRepository.SetNextCurrentPlayer();
            _playerRepository.SetNextCurrentPlayer();

            Assert.That(_playerRepository.GetCurentPlayer(), Is.EqualTo(_playerA));
        }

    }
}