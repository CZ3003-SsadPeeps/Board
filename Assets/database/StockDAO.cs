using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class StockDAO : SqliteHelper
    {
        private const String TABLE_NAME = "Stock";
        private const String KEY_CompanyName = "companyName";
        private const String KEY_Total= "total";
        private const String KEY_Shares = "shares";
        private const String KEY_Price = "price";
        private const String KEY_Day = "day";

        public StockDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_CompanyName + " TEXT, " +
                KEY_Total+ " INT NOT NULL, "+
                KEY_Shares + " INT NOT NULL, " +
                KEY_Day + " INT NOT NULL, " +
                KEY_Price + " REAL NOT NULL, "+
                "PRIMARY KEY ("+  KEY_CompanyName +" , " +  KEY_Day +" )) ";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(Stock Stock)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_CompanyName + ", "
                + KEY_Total+ ", "
                + KEY_Shares + ", "
                + KEY_Day + ", "
                + KEY_Price + " ) "

                + "VALUES ( '"
                + Stock.companyName+ "', '"
                + Stock.total + "', '"
                + Stock.shares + "', '"
                + Stock.day + "', '"
                + Stock.price + "' )";
            dbcmd.ExecuteNonQuery();

        }

        public List<Stock> getDataByString(string str)
        {
                Debug.Log(Tag + "Getting Location: " + str);

                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_CompanyName + " = '" + str + "'";
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            return convertToList(reader);
        }

        public override  void deleteDataByString(string str)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_CompanyName + " = '" + str + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteAllData()
        {
                Debug.Log(Tag + "Deleting Table");

                base.deleteAllData(TABLE_NAME);
        }

        public void doNothing()
        {
            Debug.Log("StockDAO");
        }

        public bool StoreStock(Stock[] Stocks)
        {
            foreach(Stock stock in Stocks)
            {
                addData(stock);
            }
            return true;
        }

        public List<Stock> RetrieveStocks()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            System.Data.IDataReader reader = dbcmd.ExecuteReader();
            return convertToList(reader);
        }

        public List<Stock> convertToList(System.Data.IDataReader reader)
        {
            List<Stock> res = new List<Stock>();
            while(reader.Read())
            {
                res.Add(new Stock(reader[0].ToString(),Convert.ToInt32(reader[1]),Convert.ToInt32(reader[2]),Convert.ToInt32(reader[3]),(float)Convert.ToDecimal(reader[4])));
            }
            return res ;
        }
    }
}