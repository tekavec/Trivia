using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    internal class QuestionRepository
    {
        private readonly IList<Question> _questions = new List<Question>();

        public void Add(Question question)
        {
            _questions.Add(question);
        }

        public bool Contains(Question question)
        {
            return _questions.Contains(question);
        }

        public QuestionCategory CurrentCategory(int location)
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

        public string GetFirstQuestionBy(QuestionCategory questionCategory)
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