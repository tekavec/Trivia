using System.ComponentModel;

namespace Trivia.Model.Questions
{
    public enum QuestionCategory
    {
        [Description("Pop")]
        Pop,
        [Description("Rock")]
        Rock,
        [Description("Science")]
        Science,
        [Description("Sports")]
        Sports
    }
}