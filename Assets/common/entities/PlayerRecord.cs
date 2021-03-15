public class PlayerRecord
{
    public int PlayerID { get; }

    public string Name { get; }

    public int CreditEarned { get; }

    public long DateAchieved { get; }

    public PlayerRecord(string name, int credit, long dateAchieved): this(-1, name, credit, dateAchieved) {}

    public PlayerRecord(int ID, string name, int credit, long dateAchieved)
    {
        this.PlayerID = ID;
        this.Name = name;
        this.CreditEarned = credit;
        this.DateAchieved = dateAchieved;
    }
}
