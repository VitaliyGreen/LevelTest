using System.Collections.Generic;
using System.Linq;
using LevelTest.Logic;
using LevelTest.Logic.DAL;
using LevelTest.Logic.Evaluation;
using LevelTest.Models;
using LevelTest.Models.Enums;
using LevelTest.Models.Question;
using LevelTest.UnitTesting.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelTest.UnitTesting
{
    [TestClass]
    public class LogicTests
    {
        private const string connectionString = @"Server=VITALIIRO\\SQLEXPRESS;Database=LevelTest;Trusted_Connection=True;";
        private Evaluator _evaluator;
        private DataAccess _dataAccess;
        private TestConfigurator _testConfigurator;

        [TestInitialize]
        public void Init()
        {
            _evaluator = new Evaluator();
            _dataAccess = new DataAccess(connectionString);

            TestConfiguration config = new TestConfiguration
            {
                A1QuestionsNumber = 3,
                A2QuestionsNumber = 5,
                B1QuestionsNumber = 5,
                B2QuestionsNumber = 5,
                C1QuestionsNumber = 6,
                C2QuestionsNumber = 6
            };
            _testConfigurator = new TestConfigurator(config, _dataAccess);
        }

        [TestMethod]
        public void EvaluateMethodReturnsLevelType()
        {
            List<Test> tests = new List<Test>();

            var level = _evaluator.GetLevel(tests);

            Assert.IsInstanceOfType(level, typeof(Level));
        }

        [TestMethod]
        public void EvaluateMethodReturnsUndefinedLevel()
        {
            IEnumerable<QuestionBase> A1Questions = DataBaseMock.GetQuestionsByLevel(Level.A1);
            IEnumerable<Test> tests = _testConfigurator.FormTests(A1Questions);
            
            Level evaluatedLevel = _evaluator.GetLevel(tests);
            Level expectedLevel = Level.Undefined;


            Assert.AreEqual(expectedLevel, evaluatedLevel);
        }

        [TestMethod]
        public void EvaluateMethodReturnsA1Level()
        {
            IEnumerable<QuestionBase> A1Questions = DataBaseMock.GetQuestionsByLevel(Level.A1);
            IEnumerable<Test> tests = _testConfigurator.FormTests(A1Questions);

            foreach (Test test in tests)
            {
                test.AnsweredCorrect = true;
            }

            Level evaluatedLevel = _evaluator.GetLevel(tests);
            Level expectedLevel = Level.A1;


            Assert.AreEqual(expectedLevel, evaluatedLevel);
        }

        [TestMethod]
        public void EvaluateMethodReturnsMaximumLevel()
        {
            IEnumerable<QuestionBase> allQuestions = DataBaseMock.AllQuestions;
            IEnumerable<Test> tests = _testConfigurator.FormTests(allQuestions);

            foreach (Test test in tests)
            {
                test.AnsweredCorrect = true;
            }

            Level evaluatedLevel = _evaluator.GetLevel(tests);
            Level expectedLevel = Level.C2;


            Assert.AreEqual(expectedLevel, evaluatedLevel);
        }

        [TestMethod]
        public void EvaluateMethodReturnsMiddleLevel()
        {
            IEnumerable<QuestionBase> allQuestions = DataBaseMock.AllQuestions;
            IEnumerable<Test> tests = _testConfigurator.FormTests(allQuestions);

            foreach (Test test in tests)
            {
                if (test.Question.Theme.Level != Level.B1)
                {
                    test.AnsweredCorrect = true;
                }
            }

            Level evaluatedLevel = _evaluator.GetLevel(tests);
            Level expectedLevel = Level.B2;


            Assert.AreEqual(expectedLevel, evaluatedLevel);
        }

        [TestMethod]
        public void EvaluateMethodReturnsMiddleLevelWithSeveralGaps()
        {
            IEnumerable<QuestionBase> allQuestions = DataBaseMock.AllQuestions;
            IEnumerable<Test> tests = _testConfigurator.FormTests(allQuestions);

            foreach (Test test in tests)
            {
                if ((new [] {Level.A2, Level.B2, Level.C2}).Contains(test.Question.Theme.Level))
                {
                    test.AnsweredCorrect = true;
                }
            }

            Level evaluatedLevel = _evaluator.GetLevel(tests);
            Level expectedLevel = Level.B1;


            Assert.AreEqual(expectedLevel, evaluatedLevel);
        }
    }
}
