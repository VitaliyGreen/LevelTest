using System.Collections.Generic;

namespace LevelTest.Models.Question
{
    public class StringQuestion : QuestionBase
    {
        public string QuestionString { get; set; }
        public IEnumerable<string> Variants { get; set; }
    }
}