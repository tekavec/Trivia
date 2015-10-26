using System.ComponentModel;

namespace Trivia
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