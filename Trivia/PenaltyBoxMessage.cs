using System.Text;

namespace Trivia
{
    public class PenaltyBoxMessage
    {
        private readonly bool _isInPenaltyBox;
        private readonly bool _isLuckyRoll;
        private readonly string _playerName;

        public PenaltyBoxMessage(bool isInPenaltyBox, bool isLuckyRoll, string playerName)
        {
            _isInPenaltyBox = isInPenaltyBox;
            _isLuckyRoll = isLuckyRoll;
            _playerName = playerName;
        }

        public void AppendMessageTo(StringBuilder messageCollector)
        {
            if (_isInPenaltyBox && _isLuckyRoll)
            {
                messageCollector.AppendLine($"{_playerName} is getting out of the penalty box");
            }
            if (_isInPenaltyBox && !_isLuckyRoll)
            {
                messageCollector.AppendLine($"{_playerName} is not getting out of the penalty box");
            }
        }
    }
}