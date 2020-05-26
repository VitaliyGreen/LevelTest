using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace LevelTest.Models
{
    [Serializable]
    public class TestConfiguration
    {
        public int A1QuestionsNumber { get; set; }
        public int A2QuestionsNumber { get; set; }
        public int B1QuestionsNumber { get; set; }
        public int B2QuestionsNumber { get; set; }
        public int C1QuestionsNumber { get; set; }
        public int C2QuestionsNumber { get; set; }

        public int TotalQuestionsNumber => A1QuestionsNumber + A2QuestionsNumber + 
                                           B1QuestionsNumber + B2QuestionsNumber + 
                                           C1QuestionsNumber + C2QuestionsNumber;

        public static TestConfiguration GetConfigurations(IConfiguration configuration)
        {
            TestConfiguration testConfiguration = new TestConfiguration
            {
                A1QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:A1QuestionsNumber"]),
                A2QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:A2QuestionsNumber"]),
                B1QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:B1QuestionsNumber"]),
                B2QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:B2QuestionsNumber"]),
                C1QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:C1QuestionsNumber"]),
                C2QuestionsNumber = Convert.ToInt32(configuration["TestConfiguration:C2QuestionsNumber"]),
            };

            return testConfiguration;
        }

    }
}