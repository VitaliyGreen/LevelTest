using System.Collections.Generic;
using System.Linq;
using LevelTest.Models.Enums;
using LevelTest.Models.Question;

namespace LevelTest.UnitTesting.Mocks
{
    public static class DataBaseMock
    {
        private static readonly IEnumerable<QuestionBase> _allQuestions = new List<QuestionBase>
        {
            new StringQuestion
            {
                QuestionType = QuestionType.StringQuestion,
                Theme = new Theme{ Level = Level.A1, ThemeName = "Simple Tense"}
            },
            new StringQuestion
            {
                QuestionType = QuestionType.StringQuestion,
                Theme = new Theme{ Level = Level.A1, ThemeName = "Simple Tense"}
            },
            new StringQuestion
            {
                QuestionType = QuestionType.StringQuestion,
                Theme = new Theme{ Level = Level.A1, ThemeName = "Countable & uncountable nouns"}
            },
            new StringQuestion
            {
                QuestionType = QuestionType.StringQuestion,
                Theme = new Theme{ Level = Level.A2, ThemeName = "Word order"}
            },
            new StringQuestion
            {
                QuestionType = QuestionType.StringQuestion,
                Theme = new Theme{ Level = Level.A2, ThemeName = "Continuous tense"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.A2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.B2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C1, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C2, ThemeName = "Listening"}
            },
            new AudioQuestion
            {
                QuestionType = QuestionType.Audio,
                Theme = new Theme {Level = Level.C2, ThemeName = "Listening"}
            }
        };

        public static IEnumerable<QuestionBase> AllQuestions => _allQuestions;

        public static IEnumerable<AudioQuestion> AudioQuestions =>
            (IEnumerable<AudioQuestion>) AllQuestions.Where(q => q.QuestionType == QuestionType.Audio);

        public static IEnumerable<AudioQuestion> StringQuestions =>
            (IEnumerable<AudioQuestion>)AllQuestions.Where(q => q.QuestionType == QuestionType.StringQuestion);

        public static IEnumerable<QuestionBase> GetQuestionsByLevel(Level level)
        {
            return AllQuestions.Where(q => q.Theme.Level == level);
        }
    }
}