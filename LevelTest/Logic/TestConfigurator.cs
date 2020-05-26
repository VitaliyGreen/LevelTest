using System.Collections.Generic;
using System.Linq;
using LevelTest.Logic.DAL;
using LevelTest.Models;
using LevelTest.Models.Question;
using Microsoft.Extensions.Configuration;

namespace LevelTest.Logic
{
    public class TestConfigurator
    {
        public TestConfiguration TestConfiguration { get; set; }
        public DataAccess DataAccess { get; set; }

        public TestConfigurator(TestConfiguration configuration, DataAccess dataAccess)
        {
            TestConfiguration = configuration;
            DataAccess = dataAccess;
        }

        public TestConfigurator()
        {
        }

        public IEnumerable<Test> GetTests()
        {
            IEnumerable<QuestionBase> questions = GetQuestions();

            IEnumerable<Test> tests = FormTests(questions);

            return tests;
        }

        public IEnumerable<Test> FormTests(IEnumerable<QuestionBase> questions)
        {
            List<Test> tests = new List<Test>();

            foreach (QuestionBase question in questions)
            {
                Test test = new Test
                {
                    Question = question
                };

                tests.Add(test);
            }

            return tests;
        }

        private IEnumerable<QuestionBase> GetQuestions()
        {
            IEnumerable<AudioQuestion> audioQuestions = DataAccess.GetAudioQuestions();
            List<QuestionBase> questions = new List<QuestionBase>();
            questions.AddRange(DataAccess.GetStringQuestions());
            questions.Add(audioQuestions.ElementAt(5));
            questions.Add(audioQuestions.ElementAt(15));

            return questions;
        }
    }
}