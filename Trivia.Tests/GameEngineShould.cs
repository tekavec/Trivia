using NSubstitute;
using NUnit.Framework;
using Trivia.Application;
using Trivia.Model.Players;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameEngineShould
    {
        private GameEngine _gameEngine;
        private Player _playerA;
        private Player _playerB;
        private int _defaultLocation = 0;
        private int _newLocation = 5;
        private int _defaultNumberOfCoins = 0;
        private int _defaultPlayerIndex = 0;
        private IPlayer _mockedPlayer;
        private IPlayers _players;

        [SetUp]
        public void Init()
        {
            _mockedPlayer = Substitute.For<IPlayer>();
            _players = Substitute.For<IPlayers>();
            _gameEngine = new GameEngine(_players);
            _playerA = new Player("ana", _defaultLocation, _defaultNumberOfCoins);
            _playerB = new Player("bob", _defaultLocation, _defaultNumberOfCoins);
        }

        [Test]
        public void set_next_current_player()
        {
            _gameEngine.SetNextCurrentPlayer();

            _players.Received().GetNextPlayer();
        }

        [Test]
        public void get_current_players_name()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.GetCurrentPlayerName();

            _mockedPlayer.Received().Name();
        }

        [Test]
        public void check_if_the_current_player_is_not_in_the_penalty_box()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.IsCurrentPlayerInPenaltyBox();

            _mockedPlayer.Received().IsInPenaltyBox();
        }

        [Test]
        public void get_the_current_player_location()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.GetCurrentPlayerLocation();

            _mockedPlayer.Received().Location();
        }

        [Test]
        public void change_the_current_player_location()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.ChangeCurrentPlayerLocation(_newLocation);

            _mockedPlayer.Received().ChangeLocation(_newLocation);
        }

        [Test]
        public void penalize_the_current_player()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.PenalizeCurrentPlayer();

            _mockedPlayer.Received().Penalize();
        }

        [Test]
        public void give_a_coin_to_the_current_player()
        {
            _players.GetNextPlayer().Returns(_mockedPlayer);
            _gameEngine = new GameEngine(_players);

            _gameEngine.GiveCoinToCurrentPlayer();

            _mockedPlayer.Received().IncreaseCoinsByOne();
        }

        //[Test]
        //public void conclude_the_current_player_is_not_winning()
        //{
        //    _players.GetNextPlayer().Returns(_mockedPlayer);
        //    _gameEngine = new GameEngine(_players);

        //    _gameEngine.GiveCoinToCurrentPlayer();

        //    _mockedPlayer.Received().IsNotWinning();
        //}

    }
}