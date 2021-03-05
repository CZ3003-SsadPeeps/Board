class PlayerStock
{
    public string CompanyName { get; }

    public int Quantity { get; }

    public int AvgPurchasePrice { get; }

    public int CurrentStockPrice { get; }

    public PlayerStock(string companyName, int quantity, int avgPurchasePrice, int currentStockPrice)
    {
        this.CompanyName = companyName;
        this.Quantity = quantity;
        this.AvgPurchasePrice = avgPurchasePrice;
        this.CurrentStockPrice = currentStockPrice;
    }
}
