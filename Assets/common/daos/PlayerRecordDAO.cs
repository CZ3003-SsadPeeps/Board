using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class PlayerRecordDAO : SqliteHelper, IPlayerRecordDAO
    {
        private const String TABLE_NAME = "PlayerRecord";
        private const String KEY_PlayerID = "PlayerID";
        private const String KEY_Name = "Name";
        private const String KEY_DateAchieved = "DateAchieved";
        private const String KEY_CreditEarned = "CreditEarned";

        public PlayerRecordDAO() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_PlayerID + " INTEGER PRIMARY KEY," +
                KEY_Name + " TEXT NOT NULL, " +
                KEY_DateAchieved + " INTEGER NOT NULL, " +
                KEY_CreditEarned + " INTEGER NOT NULL)";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(PlayerRecord player)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                // + KEY_PlayerID + ", "
                + KEY_Name + ", "
                + KEY_DateAchieved + ", "
                + KEY_CreditEarned + " ) "

                + "VALUES ( '"
                // + "NULL" + "', '"
                + player.Name + "', '"
                + player.DateAchieved + "', '"
                + player.CreditEarned + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public List<PlayerRecord> getDataByString(string str)
        {
            Debug.Log(Tag + "Getting Location: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_PlayerID + " = '" + str + "'";
            return convertToList(dbcmd.ExecuteReader());
        }

        public override void deleteDataByString(string str)
        {
            IDbCommand dbcmd = db_connection.CreateCommand();
            dbcmd.CommandText = "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_PlayerID + " = '" + str + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public void doNothing()
        {
            Debug.Log("PlayerRecordDAO");
        }

        public bool StorePlayerRecords(PlayerRecord[] playerRecords)
        {
            foreach (PlayerRecord player in playerRecords)
            {
                addData(player);
            }
            return true;
        }

        public List<PlayerRecord> RetrievePlayerRecords()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME;
            IDataReader reader = dbcmd.ExecuteReader();
            return convertToList(reader);
        }

        public List<PlayerRecord> convertToList(IDataReader reader)
        {
            List<PlayerRecord> res = new List<PlayerRecord>();
            while (reader.Read())
            {
                res.Add(new PlayerRecord(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32((string) reader[3]), (long)reader[2]));
            }
            return res;
        }
    }
}