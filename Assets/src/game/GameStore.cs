public class GameStore
{
    public static Player[] Players { get; private set; }
    public static Player CurrentPlayer
    {
        get { return Players[CurrentPlayerPos]; }
    }

    public static int CurrentPlayerPos { get; private set; } = 0;

    public static int PrevPlayerPos
    {
        get
        {
            return (CurrentPlayerPos == 0 ? Players.Length : CurrentPlayerPos) - 1;
        }
    }

    public static void InitPlayers(string[] names)
    {
        Players = new Player[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            Players[i] = new Player(names[i]);
        }

        CurrentPlayerPos = 0;
    }

    public static void IncrementTurn()
    {
        CurrentPlayerPos = (CurrentPlayerPos + 1) % Players.Length;
    }
}
