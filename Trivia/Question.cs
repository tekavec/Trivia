namespace Trivia
{
    public class Question
    {
        private readonly QuestionCategory _category;
        private readonly string _name;

        public Question(QuestionCategory category, string name)
        {
            _category = category;
            _name = name;
        }

        public QuestionCategory Category()
        {
            return _category;
        }

        public string Name()
        {
            return _name;
        }
    }
}