public class PlayerRecord
{
    public int PlayerID { get; }
    public string Name { get; }
    public int CreditEarned { get; }
    public long DateAchieved { get; }

    public PlayerRecord(int ID,string name, int credit, long dateAchieved)
    {
        PlayerID = ID;
        Name = name;
        CreditEarned = credit;
        DateAchieved = dateAchieved;
    }
}
