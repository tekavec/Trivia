using System.Text;

namespace Trivia
{
    public class AskQuestionMessage
    {
        private readonly int _location;
        private readonly string _playerName;
        private readonly string _categoryName;
        private readonly string _question;

        public AskQuestionMessage(int location, string playerName, string categoryName, string question)
        {
            _location = location;
            _playerName = playerName;
            _categoryName = categoryName;
            _question = question;
        }

        public void AppendMessageTo(StringBuilder messageCollector)
        {
            messageCollector.AppendLine($"{_playerName}'s new location is {_location}")
                .AppendLine($"The category is {_categoryName}")
                .AppendLine(_question);
        }
    }
}