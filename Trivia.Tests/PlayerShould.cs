using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerShould
    {
        private Player _playerA;

        [TestFixtureSetUp]
        public void Init()
        {
            _playerA = new Player("ana", 1, 0);
        }

        [Test]
        public void be_in_the_penalty_box()
        {
            _playerA.Penalize();

            Assert.That(_playerA.IsInPenaltyBox(), Is.True);
        }

        [Test]
        public void change_the_current_location_if_new_location_is_less_or_equal_than_11()
        {

            _playerA.ChangeLocationBy(4);

            Assert.That(_playerA.Location(), Is.EqualTo(5));
        }

        [Test]
        public void decrease_location_by_12_if_location_is_greater_than_11()
        {
            var player = new Player("bob", 2, 0);

            player.ChangeLocationBy(13);

            Assert.That(player.Location(), Is.EqualTo(3));
        }

        [Test]
        public void increase_coins_by_one()
        {
            _playerA.IncreaseCoinsByOne();

            Assert.That(_playerA.Coins(), Is.EqualTo(1));
        }

        [Test]
        public void check_if_it_is_not_winning_if_it_does_not_have_6_coins()
        {
            var player = new Player("bob", 0, 5);

            Assert.That(player.IsWinning(), Is.False);
        }

        [Test]
        public void check_if_it_is_winning_if_it_has_6_coins()
        {
            var player = new Player("bob", 0, 6);

            Assert.That(player.IsWinning(), Is.True);
        }

    }
}