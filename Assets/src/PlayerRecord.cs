class PlayerRecord
{
    public string Name { get; }
    public int CreditEarned { get; }
    public int DateAchieved { get; }

    public PlayerRecord(string name, int credit, int dateAchieved)
    {
        this.Name = name;
        this.CreditEarned = credit;
        this.DateAchieved = dateAchieved;
    }
}
