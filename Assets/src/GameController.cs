class GameController
{
    private static readonly int GO_PAYOUT = 150;
    // [Note] Must specify System namespace to avoid clash with Unity's Random class
    private static readonly System.Random RANDOM = new System.Random();

    public Player[] Players { get; private set; }
    public Player CurrentPlayer
    {
        get { return Players[CurrentPlayerPos]; }
    }

    public int CurrentPlayerPos { get; private set; } = 0;

    public void SetPlayerNames(string[] names)
    {
        Players = new Player[names.Length];
        for (int i = 0; i < names.Length; i++)
        {
            Players[i] = new Player(names[i]);
        }
    }

    public int GenerateDiceValue()
    {
        // [Note] Upper bound is exclusive
        return RANDOM.Next(1, 7);
    }

    public void IssueGoPayout()
    {
        CurrentPlayer.AddCredit(GO_PAYOUT);
    }

    public bool NextTurn()
    {
        CurrentPlayerPos = (CurrentPlayerPos + 1) % Players.Length;
        return CurrentPlayerPos == 0;
    }

    public void SavePlayerScores()
    {
        foreach (Player player in Players)
        {
            // Sell all stocks & add to score
        }

        //    // Store all scores to database
    }
}
