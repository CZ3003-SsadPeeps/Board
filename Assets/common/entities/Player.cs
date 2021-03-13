public class Player
{
    public string Name { get; }
    public int Credit { get; private set; } = 1024;

    public Player(string name)
    {
        Name = name;
    }

    public void AddCredit(int amount)
    {
        Credit += amount;
    }
}
