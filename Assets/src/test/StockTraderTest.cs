using UnityEngine;
using System.Collections.Generic;

class StockTraderTest : IStockTrader
{
    public List<PlayerStock> GetPlayerStocks(string playerName)
    {
        return new List<PlayerStock>();
    }

    public void SellAllStocks(Player[] players)
    {
        foreach (Player player in Players)
        {
            Debug.Log($"Selling stock of player {player.Name}");
        }
    }
}
