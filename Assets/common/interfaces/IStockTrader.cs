using System.Collections.Generic;

interface IStockTrader
{
    List<PlayerStock> GetPlayerStocks(string playerName);

    void SellAllStocks(Player[] players);
}
