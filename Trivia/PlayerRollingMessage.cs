using System.Text;

namespace Trivia
{
    public class PlayerRollingMessage
    {
        private readonly int _location;
        private readonly string _playerName;

        public PlayerRollingMessage(int location, string playerName)
        {
            _location = location;
            _playerName = playerName;
        }

        public void AppendMessageTo(StringBuilder messageCollector)
        {
            messageCollector.AppendLine($"{_playerName} is the current player");
            messageCollector.AppendLine($"They have rolled a {_location}");
        }
    }
}