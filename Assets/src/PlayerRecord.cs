class PlayerRecord
{
    public string Name { get; }
    public int CreditEarned { get; }
    public long DateAchieved { get; }

    public PlayerRecord(string name, int credit, long dateAchieved)
    {
        this.Name = name;
        this.CreditEarned = credit;
        this.DateAchieved = dateAchieved;
    }
}
