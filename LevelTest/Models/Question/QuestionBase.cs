using LevelTest.Models.Enums;

namespace LevelTest.Models.Question
{
    public abstract class QuestionBase
    {
        public Theme Theme { get; set; }
        public QuestionType QuestionType { get; set; }
        public string CorrectAnswer { get; set; }
    }
}