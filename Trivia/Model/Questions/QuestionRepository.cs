using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia.Model.Questions
{
    public class QuestionRepository
    {
        private readonly IList<Question> _questions = new List<Question>();

        public QuestionRepository(int capacity)
        {
            for (var i = 0; i < capacity; i++)
            {
                _questions.Add(new Question(QuestionCategory.Pop, "Pop Question " + i));
                _questions.Add(new Question(QuestionCategory.Science, "Science Question " + i));
                _questions.Add(new Question(QuestionCategory.Sports, "Sports Question " + i));
                _questions.Add(new Question(QuestionCategory.Rock, "Rock Question " + i));
            }
        }

        public bool Contains(Question question)
        {
            return _questions.Contains(question);
        }

        public QuestionCategory GetCategoryBy(int location)
        {
            if (location == 0) return QuestionCategory.Pop;
            if (location == 4) return QuestionCategory.Pop;
            if (location == 8) return QuestionCategory.Pop;
            if (location == 1) return QuestionCategory.Science;
            if (location == 5) return QuestionCategory.Science;
            if (location == 9) return QuestionCategory.Science;
            if (location == 2) return QuestionCategory.Sports;
            if (location == 6) return QuestionCategory.Sports;
            if (location == 10) return QuestionCategory.Sports;
            return QuestionCategory.Rock;
        }

        public string GetCategoryNameBy(int location)
        {
            return GetCategoryBy(location).GetDescription();
        }

        public string GetNextQuestionBy(QuestionCategory questionCategory)
        {
            var question
                = _questions.FirstOrDefault(a => a.Category() == questionCategory);
            if (question != null)
                return question.Name();
            throw new NullReferenceException();
        }

        public void RemoveFirstQuestionOf(QuestionCategory questionCategory)
        {
            var question
                = _questions.FirstOrDefault(a => a.Category() == questionCategory);
            if (question != null)
                _questions.Remove(question);
        }
    }
}