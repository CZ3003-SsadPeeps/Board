using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class NewsRecordDAO : SqliteHelper
    {

        private const String TABLE_NAME = "News";
        private const String KEY_NewsID = "NewsID";
        private const String KEY_CompanyName = "CompanyName";
        private const String KEY_Content = "Content";
        private const String KEY_FluctuationRate = "FluctuationRate";


        public NewsRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_NewsID + " INT NOT NULL, " +
                KEY_CompanyName + " TEXT NOT NULL, " +
                KEY_Content + " TEXT NOT NULL, " +
                KEY_FluctuationRate + " REAL NOT NULL,"+
                "PRIMARY KEY("+ KEY_NewsID+", "+ KEY_CompanyName+")";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(NewsRecord news)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_NewsID + ", "
                    + KEY_CompanyName + ", "
                    + KEY_Content + ", "
                    + KEY_FluctuationRate + " ) "

                    + "VALUES ( '"
                    + news.NewsID + "', '"
                    + news.CompanyName + "', '"
                    + news.Content + "', '"
                    + news.FluctuationRate + "' )";
                dbcmd.ExecuteNonQuery();
            }
            catch
            {
                Debug.Log("Invalid data key because repeated. Please change a new key");

            }

        }

        public List<NewsRecord> RetrieveNewsRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            List<NewsRecord> res = new List<NewsRecord>();
            while (reader.Read())
            {
                res.Add(new NewsRecord(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(),float.Parse(reader[3].ToString())));
            }
            return res;
        }

    }
}