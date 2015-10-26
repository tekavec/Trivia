using System;
using NSubstitute;
using NUnit.Framework;

namespace Trivia.Tests
{
    [TestFixture]
    public class QuestionRepositoryShould
    {
        private QuestionRepository _questionRepository;
        private Question _popQuestion;
        private Question _popQuestion1;
        private Question _rockQuestion;
        private const string PopQuestionText0 = "Pop Question 0";
        private const string PopQuestionText1 = "Pop Question 1";
        private const string RockQuestionText= "Rock Question 0";

        [TestFixtureSetUp]
        public void Init()
        {
            _questionRepository = new QuestionRepository();
            _popQuestion = new Question(QuestionCategory.Pop, PopQuestionText0);
            _popQuestion1 = new Question(QuestionCategory.Pop, PopQuestionText0);
            _rockQuestion = new Question(QuestionCategory.Rock, RockQuestionText);
        }

        [Test]
        public void store_a_question()
        {
            _questionRepository.Add(_popQuestion);

            Assert.That(_questionRepository.Contains(_popQuestion), Is.True);
        }

        [TestCase(0, QuestionCategory.Pop)]
        [TestCase(4, QuestionCategory.Pop)]
        [TestCase(8, QuestionCategory.Pop)]
        [TestCase(1, QuestionCategory.Science)]
        [TestCase(5, QuestionCategory.Science)]
        [TestCase(9, QuestionCategory.Science)]
        [TestCase(2, QuestionCategory.Sports)]
        [TestCase(6, QuestionCategory.Sports)]
        [TestCase(10, QuestionCategory.Sports)]
        public void get_category_name_calculated_on_current_player_location(int location, string category)
        {
            Assert.That(_questionRepository.CurrentCategory(location), Is.EqualTo(category));
        }

        [TestCase(QuestionCategory.Pop, PopQuestionText0)]
        [TestCase(QuestionCategory.Rock, RockQuestionText)]
        public void get_first_question_by_category(QuestionCategory category, string expectedQuestion)
        {
            _questionRepository.Add(_popQuestion);
            _questionRepository.Add(_popQuestion1);
            _questionRepository.Add(_rockQuestion);

            Assert.That(_questionRepository.GetFirstQuestionBy(category), Is.EqualTo(expectedQuestion));
        }

        [Test]
        public void remove_first_question_of_category(QuestionCategory category, string expectedQuestion)
        {
            _questionRepository.Add(_popQuestion);
            _questionRepository.Add(_popQuestion1);
            _questionRepository.Add(_rockQuestion);

            _questionRepository.RemoveFirstQuestionOf(QuestionCategory.Pop);

            Assert.That(_questionRepository.Contains(_popQuestion), Is.False);
        }


    }
}