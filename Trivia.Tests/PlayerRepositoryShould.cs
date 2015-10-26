using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerRepositoryShould
    {
        private PlayerRepository _playerRepository;
        private Player _playerA;
        private Player _playerB;

        [TestFixtureSetUp]
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
    }
}