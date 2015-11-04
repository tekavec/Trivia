using Trivia.Model.Players;

namespace Trivia.Application
{
    public class GameEngine
    {
        private readonly IPlayers _players;
        private IPlayer _currentPlayer;

        public GameEngine(IPlayers players)
        {
            _players = players;
            _currentPlayer = players.GetNextPlayer();
        }

        public void SetNextCurrentPlayer()
        {
            _currentPlayer = _players.GetNextPlayer();
        }

        public string GetCurrentPlayerName()
        {
            return _currentPlayer.Name();
        }

        public bool IsCurrentPlayerInPenaltyBox()
        {
            return _currentPlayer.IsInPenaltyBox();
        }

        public int GetCurrentPlayerLocation()
        {
            return _currentPlayer.Location();
        }

        public void ChangeCurrentPlayerLocation(int toNewLocation)
        {
            _currentPlayer.ChangeLocation(toNewLocation);
        }

        public void PenalizeCurrentPlayer()
        {
            _currentPlayer.Penalize();
        }

        public void GiveCoinToCurrentPlayer()
        {
            _currentPlayer.IncreaseCoinsByOne();
        }

        public bool IsCurrentPlayerNotWinning()
        {
            return _currentPlayer.IsNotWinning();
        }

        public IPlayer CurrentPlayer()
        {
            return _currentPlayer;
        }
    }
}