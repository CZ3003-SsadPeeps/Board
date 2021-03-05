using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

class GameController
{
    private static readonly int GO_PAYOUT = 150;
    // [Note] Must specify System namespace to avoid clash with Unity's Random class
    private static readonly System.Random RANDOM = new System.Random();

    IStockTrader stockTrader;
    IPlayerRecordDAO playerRecordDAO;
    public Player[] Players { get; private set; }
    public Player CurrentPlayer
    {
        get { return Players[CurrentPlayerPos]; }
    }

    public int CurrentPlayerPos { get; private set; } = 0;

    public GameController(IStockTrader stockTrader, IPlayerRecordDAO playerRecordDAO)
    {
        this.stockTrader = stockTrader;
        this.playerRecordDAO = playerRecordDAO;
    }

    public void SetPlayerNames(string[] names)
    {
        Players = new Player[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            Players[i] = new Player(names[i]);
        }
    }

    public List<PlayerStock> GetPlayerStocks()
    {
        return stockTrader.GetPlayerStocks(CurrentPlayer.Name);
    }

    public int GenerateDiceValue()
    {
        // [Note] Upper bound is exclusive
        return RANDOM.Next(1, 7);
    }

    public void IssueGoPayout()
    {
        CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    public bool NextTurn()
    {
        CurrentPlayerPos = (CurrentPlayerPos + 1) % Players.Length;
        return CurrentPlayerPos == 0;
    }

    public void SavePlayerScores()
    {
        stockTrader.SellAllStocks(Players);

        PlayerRecord[] records = new PlayerRecord[Players.Length];
        Player player;
        for (int i = 0; i < Players.Length; i++)
        {
            player = Players[i];
            records[i] = new PlayerRecord(player.Name, player.Credit, (long)(DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        Debug.Log("Storing credits to database...");
        playerRecordDAO.StorePlayerRecords(records);
    }
}
