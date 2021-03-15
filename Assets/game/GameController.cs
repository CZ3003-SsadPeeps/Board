using UnityEngine;
using System;
using System.Collections.Generic;

class GameController
{
    static readonly int GO_PAYOUT = 150;
    // [Note] Must specify System namespace to avoid clash with Unity's Random class
    static readonly System.Random RANDOM = new System.Random();

    IStockTrader stockTrader;
    IPlayerRecordDAO playerRecordDAO;

    public GameController(IStockTrader stockTrader, IPlayerRecordDAO playerRecordDAO)
    {
        this.stockTrader = stockTrader;
        this.playerRecordDAO = playerRecordDAO;
    }

    public List<PlayerStock> GetPlayerStocks()
    {
        return stockTrader.GetPlayerStocks(GameStore.CurrentPlayer.Name);
    }

    public int GenerateDiceValue()
    {
        // [Note] Upper bound is exclusive
        return RANDOM.Next(1, 7);
    }

    public void IssueGoPayout()
    {
        GameStore.CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    public bool NextTurn()
    {
        return GameStore.IncrementTurn();
    }

    public void SavePlayerScores()
    {
        stockTrader.SellAllStocks(GameStore.Players);

        PlayerRecord[] records = new PlayerRecord[GameStore.Players.Length];
        Player player;
        for (int i = 0; i < records.Length; i++)
        {
            player = GameStore.Players[i];

            //needs to add playerID
            records[i] = new PlayerRecord(1,player.Name, player.Credit, (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        Debug.Log("Storing credits to database...");
        playerRecordDAO.StorePlayerRecords(records);
    }
}
