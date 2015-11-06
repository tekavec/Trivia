using System;
using System.Text;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class PlayerRollingMessageShould
    {

        [Test]
        public void append_a_message_to_message_collector_for_player_rolling()
        {
            var randomLocation = new Random().Next(5) + 1;
            var playerName = "a player";
            var messageCollector = new StringBuilder();
            var expectedMessageCollector = new StringBuilder()
                .AppendLine($"{playerName} is the current player")
                .AppendLine($"They have rolled a {randomLocation}");

            var penaltyBoxMessage = new PlayerRollingMessage(randomLocation, playerName);
            penaltyBoxMessage.AppendMessageTo(messageCollector);

            Assert.That(messageCollector.ToString(), Is.EqualTo(expectedMessageCollector.ToString()));
        }
    }
}