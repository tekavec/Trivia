using System.Collections.Generic;

namespace Trivia.Model.Players
{
    public class Players : IPlayers
    {
        private readonly Queue<IPlayer> _players = new Queue<IPlayer>();

        public void Add(IPlayer player)
        {
            _players.Enqueue(player);
        }

        public IPlayer GetNextPlayer()
        {
            var nextPlayer = _players.Dequeue();
            _players.Enqueue(nextPlayer);
            return nextPlayer;
        }
    }
}