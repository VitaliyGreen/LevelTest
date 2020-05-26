using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using LevelTest.Models.Enums;
using LevelTest.Models.Question;

namespace LevelTest.Logic.DAL
{
    public class DataAccess
    {
        private readonly string _connectionString;
        private readonly string _sqlExpression = "select q.TypeId, q.ThemeId, q.CorrectAnswer, q.Question, q.FilePath, q.Variants, " +
                                                 "t.Name as ThemeName,t.LevelId from Question as q " +
                                                 "inner join Theme as t on q.ThemeId = t.Id " +
                                                 "inner join Level as l on t.LevelId = l.Id " +
                                                 "where TypeId = ";

        private const int _stringQuestionTypeId = 1;
        private const int _audioQuestionTypeId = 2;

        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<QuestionBase> GetAllQuestions()
        {
            List<QuestionBase> allQuestions = new List<QuestionBase>();
            allQuestions.AddRange(GetAudioQuestions());
            allQuestions.AddRange(GetStringQuestions());

            return allQuestions;
        }

        public IEnumerable<StringQuestion> GetStringQuestions()
        {
            List<StringQuestion> stringQuestions = new List<StringQuestion>();

            string sqlExpression = _sqlExpression + _stringQuestionTypeId.ToString();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Theme theme = new Theme
                        {
                            Level = (Level) reader.GetInt32(7),
                            ThemeName = reader.GetString(6)
                        };

                        StringQuestion question = new StringQuestion
                        {
                            Theme = theme,
                            QuestionType = (QuestionType) reader.GetInt32(0),
                            CorrectAnswer = reader.GetString(2),
                            QuestionString = reader.GetString(3),
                            Variants = reader.GetString(5).Split(';')
                        };

                        stringQuestions.Add(question);
                    }
                }
            }

            return stringQuestions;
        }

        public IEnumerable<AudioQuestion> GetAudioQuestions()
        {
            List<AudioQuestion> audioQuestions = new List<AudioQuestion>();

            string sqlExpression = _sqlExpression + _audioQuestionTypeId.ToString();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Theme theme = new Theme
                        {
                            Level = (Level)reader.GetInt32(7),
                            ThemeName = reader.GetString(6)
                        };

                        AudioQuestion question = new AudioQuestion
                        {
                            Theme = theme,
                            QuestionType = (QuestionType)reader.GetInt32(0),
                            CorrectAnswer = reader.GetString(2),
                            AudioQuestionPath = reader.GetString(4)
                        };

                        audioQuestions.Add(question);
                    }
                }
            }

            return audioQuestions;
        }

        public IEnumerable<QuestionBase> GetQuestionsByLevel(Level level)
        {
            IEnumerable<QuestionBase> allQuestions = GetAllQuestions();

            return GetQuestionsByLevel(level, allQuestions);
        }

        public IEnumerable<QuestionBase> GetQuestionsByLevel(Level level, IEnumerable<QuestionBase> collection)
        {
            return collection.Where(q => q.Theme.Level == level);
        }

    }
}