using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class QuizRecordDAO : SqliteHelper
    {

        private const String TABLE_NAME = "Quiz";
        private const String KEY_QuizID = "QuizID";
        private const String KEY_Question = "Question";
        private const String KEY_Reward = "Reward";
        private const String KEY_Difficulty = "Difficulty";

        private const String TABLE_NAME2 = "QuestionOption";
        private const String KEY_Content = "Content";
        private const String KEY_isCorrect = "isCorrect";

        public QuizRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_QuizID + " INT PRIMARY KEY, " +
                KEY_Question + " TEXT NOT NULL, " +
                KEY_Reward + " INT NOT NULL, " +
                KEY_Difficulty + " TEXT NOT NULL)";
            dbcmd.ExecuteNonQuery();

            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME2 + " ( " +
                KEY_QuizID + " INT, " +
                KEY_Content + " TEXT NOT NULL, " +
                KEY_isCorrect + " INT NOT NULL, " +
                "FOREIGN KEY(" + KEY_QuizID + ") REFERENCES " + TABLE_NAME + "(" + KEY_QuizID + "))";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(QuizRecord quizRecord)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_QuizID + ", "
                    + KEY_Question + ", "
                    + KEY_Reward + ", "
                    + KEY_Difficulty + " ) "

                    + "VALUES ( '"
                    + quizRecord.QuizID + "', '"
                    + quizRecord.Question + "', '"
                    + quizRecord.Reward + "', '"
                    + quizRecord.Difficulty + "' )";
                dbcmd.ExecuteNonQuery();

                foreach (QuestionOptionRecord option in quizRecord.QuestionOptions)
                {
                    if (option.isCorrect)
                    {
                        dbcmd.CommandText =
                   "INSERT INTO " + TABLE_NAME2
                   + " ( "
                   + KEY_QuizID + ", "
                   + KEY_Content + ", "
                   + KEY_isCorrect + " ) "

                   + "VALUES ( '"
                   + quizRecord.QuizID + "', '"
                   + option.Content + "', '"
                   + 1 + "' )";
                        dbcmd.ExecuteNonQuery();
                    }
                    else
                    {
                        dbcmd.CommandText =
                   "INSERT INTO " + TABLE_NAME2
                   + " ( "
                   + KEY_QuizID + ", "
                   + KEY_Content + ", "
                   + KEY_isCorrect + " ) "

                   + "VALUES ( '"
                   + quizRecord.QuizID + "', '"
                   + option.Content + "', '"
                   + 0 + "' )";
                        dbcmd.ExecuteNonQuery();
                    }
                    
                }
            }
            catch
            {
                Debug.Log("Invalid data key because repeated. Please change a new key");
            }
            
           
        }

        public List<QuizRecord> RetrieveQuizRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            List<QuizRecord> res = new List<QuizRecord>();
            while (reader.Read())
            {
                IDbCommand dbcmd2 = getDbCommand();
                dbcmd2.CommandText = "SELECT * FROM " + TABLE_NAME2 + " WHERE " + KEY_QuizID + " = '" + reader[0].ToString() + "'";
                System.Data.IDataReader reader2 = dbcmd2.ExecuteReader();
                List<QuestionOptionRecord> questionOptions = new List<QuestionOptionRecord>();
                while (reader2.Read())
                {
                    questionOptions.Add(new QuestionOptionRecord(reader2[1].ToString(), Convert.ToBoolean(reader2[2])));
                }
                res.Add(new QuizRecord(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), reader[3].ToString(),questionOptions));
            }
            return res;
        }

    }
}