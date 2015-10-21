using System.Collections.Generic;

namespace Trivia
{
    public class PlayerRepository
    {
        private readonly IList<Player> _players= new List<Player>();

        public bool Contains(Player player)
        {
            return _players.Contains(player);
        }

        public void Add(Player player)
        {
            _players.Add(player);
        }

        public int Count()
        {
            return _players.Count;
        }

        public Player GetPlayerByIndex(int index)
        {
            return _players[index];
        }
    }
}