using System;
using System.Collections.Generic;
using System.Linq;
using LevelTest.Models;
using LevelTest.Models.Enums;

namespace LevelTest.Logic.Evaluation
{
    public class Evaluator
    {
        private readonly Score[] _scoreArray;

        public Evaluator()
        {
            _scoreArray = new []
            {
                new Score {Level = Level.A1},
                new Score {Level = Level.A2},
                new Score {Level = Level.B1},
                new Score {Level = Level.B2},
                new Score {Level = Level.C1},
                new Score {Level = Level.C2}
            };
        }

        public Level GetLevel(IEnumerable<Test> tests)
        {
            FillScoreList(tests);
            Level level = Evaluate();

            return level;
        }

        private void FillScoreList(IEnumerable<Test> tests)
        {
            foreach (Test test in tests)
            {
                Score score = _scoreArray.First(s => s.Level == test.Question.Theme.Level);
                score.TotalQuestionsNumber++;

                if (test.AnsweredCorrect)
                {
                    score.CorrectQuestionsNumber++;
                }
            }
        }

        private Level Evaluate()
        {
            bool isGap = IsGap(out Level downSuccessfulBorder, out Level upSuccessfulBorder);

            if (isGap)
            {
                int levelDifference = upSuccessfulBorder - downSuccessfulBorder;
                int median = (int) Math.Ceiling(levelDifference / 2d);
                Level mediumLevel = downSuccessfulBorder + median;

                return mediumLevel;

            }

            return upSuccessfulBorder;
        }

        private bool IsGap(out Level downSuccessfulBorder, out Level upSuccessfulBorder)
        {
            Score[] successfulLevels = _scoreArray.Where(t => t.IsSatisfactory).ToArray();
            Score[] unsuccessfulLevels = _scoreArray.Where(t => t.IsSatisfactory == false).ToArray();

            upSuccessfulBorder = successfulLevels.Any() ? successfulLevels.Last().Level : Level.Undefined;

            if (unsuccessfulLevels.Any())
            {
                int firstUnsuccessfulLevel = (int)unsuccessfulLevels[0].Level;
                downSuccessfulBorder = (Level)firstUnsuccessfulLevel - 1;
            }
            else
            {
                downSuccessfulBorder = upSuccessfulBorder;
            }

            if (upSuccessfulBorder == downSuccessfulBorder)
            {
                return false;
            }

            return true;
        }
    }
}