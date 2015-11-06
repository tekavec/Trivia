using System;
using System.Text;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class AskQuestionMessageShould
    {
        [Test]
        public void append_a_message_to_message_collector_asking_a_question_for_new_location()
        {
            var randomLocation = new Random().Next(5) + 1;
            var playerName = "a player";
            var categoryName = "a category";
            var question = "a question";
            var messageCollector = new StringBuilder();
            var expectedMessageCollector = new StringBuilder()
                .AppendLine($"{playerName}'s new location is {randomLocation}")
                .AppendLine($"The category is {categoryName}")
                .AppendLine(question);

            var penaltyBoxMessage = new AskQuestionMessage(
                randomLocation, 
                playerName,
                categoryName, question);
            penaltyBoxMessage.AppendMessageTo(messageCollector);

            Assert.That(messageCollector.ToString(), Is.EqualTo(expectedMessageCollector.ToString()));
        }

    }
}