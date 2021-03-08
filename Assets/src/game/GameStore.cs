public class GameStore
{
    public static Player[] Players { get; private set; }
    public static Player CurrentPlayer
    {
        get { return Players[CurrentPlayerPos]; }
    }

    public static int CurrentPlayerPos { get; private set; } = 0;

    public static void Reset()
    {
        Players = new Player[4];
        CurrentPlayerPos = 0;
    }

    public static void IncrementTurn()
    {
        CurrentPlayerPos = (CurrentPlayerPos + 1) % Players.Length;
    }
}
