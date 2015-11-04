namespace Trivia.Model.Questions
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

        protected bool Equals(Question other)
        {
            return _category == other._category && string.Equals(_name, other._name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Question) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) _category*397) ^ (_name != null ? _name.GetHashCode() : 0);
            }
        }
    }
}