using UnityEngine;
using System.Collections.Generic;

class StockTraderTest : IStockTrader
{
    public List<PlayerStock> GetPlayerStocks(string playerName)
    {
        return new List<PlayerStock>() {
            new PlayerStock("Apple", 12, 44, 63),
            new PlayerStock("Tesla", 16, 33, 40),
            new PlayerStock("AMD", 22, 16, 55),
            new PlayerStock("Raid Shadow Legends", 35, 21, 69),
            new PlayerStock("Gamestop", 88, 69, 420),
            new PlayerStock("NTU", 1, 6, 2),
        };
    }

    public void SellAllStocks(Player[] players)
    {
        foreach (Player player in players)
        {
            Debug.Log($"Selling stock of player {player.Name}");
        }
    }
}
