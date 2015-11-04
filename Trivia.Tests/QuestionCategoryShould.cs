using NUnit.Framework;
using Trivia.Model.Questions;

namespace Trivia.Tests
{
    [TestFixture]
    public class QuestionCategoryShould
    {

        [TestCase(QuestionCategory.Pop, "Pop")]
        [TestCase(QuestionCategory.Rock, "Rock")]
        [TestCase(QuestionCategory.Science, "Science")]
        [TestCase(QuestionCategory.Sports, "Sports")]
        public void return_friendly_string_representation(QuestionCategory questionCategory, string category)
        {
            Assert.That(questionCategory.GetDescription(), Is.EqualTo(category));
        }
         
    }
}