public class Player
{
    public string Name { get; }
    public int Credit { get; private set; } = 1024;

    public Player(string name)
    {
        this.Name = name;
    }

    public void addCredit(int amount)
    {
        Credit += amount;
    }
}
