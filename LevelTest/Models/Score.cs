using System;
using LevelTest.Models.Enums;

namespace LevelTest.Models
{
    public class Score
    {
        public double Threshold { get; set; } = 70d;

        public Level Level { get; set; }

        public int TotalQuestionsNumber { get; set; }

        public int CorrectQuestionsNumber { get; set; }

        public double CorrectPercentage
        {
            get
            {
                if (CorrectQuestionsNumber > TotalQuestionsNumber)
                {
                    return -1;
                }

                return (double)CorrectQuestionsNumber / (double)TotalQuestionsNumber * 100d;
            }
        }

        public bool IsSatisfactory => CorrectPercentage >= Threshold;

    }
}