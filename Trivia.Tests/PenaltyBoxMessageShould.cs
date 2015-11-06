using System.Text;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PenaltyBoxMessageShould
    {
        private bool _isInPenaltyBox = true;
        private bool _isLuckyRoll = true;
        private bool _isNotInPenaltyBox = false;
        private bool _isNotLuckyRoll = false;
        private string _playerName = "ana";
        private StringBuilder _messageCollector;

        [SetUp]
        public void Init()
        {
            _messageCollector = new StringBuilder();
        }

        [Test]
        public void append_a_message_to_message_collector_for_getting_out_from_it()
        {
            var expectedMessageCollector = new StringBuilder().AppendLine($"{_playerName} is getting out of the penalty box");
            var penaltyBoxMessage = new PenaltyBoxMessage(_isInPenaltyBox, _isLuckyRoll, _playerName);

            penaltyBoxMessage.AppendMessageTo(_messageCollector);

            Assert.That(_messageCollector.ToString(), Is.EqualTo(expectedMessageCollector.ToString()));
        }

        [Test]
        public void append_a_message_to_message_collector_for_not_getting_out_from_it()
        {
            var expectedMessageCollector = new StringBuilder().AppendLine($"{_playerName} is not getting out of the penalty box");
            var penaltyBoxMessage = new PenaltyBoxMessage(_isInPenaltyBox, _isNotLuckyRoll, _playerName);

            penaltyBoxMessage.AppendMessageTo(_messageCollector);

            Assert.That(_messageCollector.ToString(), Is.EqualTo(expectedMessageCollector.ToString()));
        }
        [Test]
        public void append_a_message_to_message_collector_if_player_is_not_in_the_penalty_box()
        {
            var expectedMessageCollector = new StringBuilder();
            var penaltyBoxMessage = new PenaltyBoxMessage(_isNotInPenaltyBox, _isLuckyRoll, _playerName);

            penaltyBoxMessage.AppendMessageTo(_messageCollector);

            Assert.That(_messageCollector.ToString(), Is.EqualTo(expectedMessageCollector.ToString()));
        }
    }
}