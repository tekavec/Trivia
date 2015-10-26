using System;
using System.Collections.Generic;
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
        private const string ScienceQuestionText0 = "Science Question 0";
        private const string RockQuestionText= "Rock Question 0";

        [SetUp]
        public void Init()
        {
            _questionRepository = new QuestionRepository(1);
            _popQuestion = new Question(QuestionCategory.Pop, PopQuestionText0);
            _popQuestion1 = new Question(QuestionCategory.Pop, PopQuestionText0);
            _rockQuestion = new Question(QuestionCategory.Rock, RockQuestionText);
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
        public void get_category_name_calculated_on_current_player_location(int location, QuestionCategory category)
        {
            Assert.That(_questionRepository.CurrentCategory(location), Is.EqualTo(category));
        }

        [Test]
        public void get_first_question_by_category()
        {
            Assert.That(_questionRepository.GetFirstQuestionBy(QuestionCategory.Pop), Is.EqualTo(PopQuestionText0));
        }

        [Test]
        public void remove_first_question_of_category()
        {
            _questionRepository.RemoveFirstQuestionOf(QuestionCategory.Pop);

            Assert.That(_questionRepository.Contains(_popQuestion), Is.False);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void throw_a_NullReferenceException_when_trying_to_remove_firstQuestion_from_an_empty_repository()
        {
            var questionRepository = new QuestionRepository(0);

            questionRepository.GetFirstQuestionBy(QuestionCategory.Pop);
        }

        [Test]
        public void populate_itself()
        {
            var questions = new List<Question>
            {
                new Question(QuestionCategory.Pop, "Pop Question 0"),
                new Question(QuestionCategory.Science, "Science Question 0"),
                new Question(QuestionCategory.Sports, "Sports Question 0"),
                new Question(QuestionCategory.Rock, "Rock Question 0")
            };

            foreach (var question in questions)
            {
                Assert.That(_questionRepository.Contains(question), Is.True);
            }
        }

    }
}