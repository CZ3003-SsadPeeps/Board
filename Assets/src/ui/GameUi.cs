using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Board board;
    public Canvas canvas;
    public Button rollDiceButton, endTurnButton;
    public GameObject PlayerCardSmallPrefab, PlayerCardBigPrefab, TextPrefab;

    // TODO: Replace with actual StockTrader & PlayerRecordDAO classes from stock system
    GameController controller = new GameController(new StockTraderTest(), new PlayerRecordDAOTest());
    List<GameObject> listPlayerCardsSmall = new List<GameObject>();
    List<GameObject> listPlayerCardsBig = new List<GameObject>();

    void Start()
    {
        LoadPlayerCard();
        LoadCurrentPlayerDetails();
    }

    public void RollDice()
    {
        StartCoroutine(PerformDiceRoll());
    }

    public void EndTurn()
    {
        if (board.HasReachedMaxLaps())
        {
            // TODO: Disable all buttons except leaderboard & back
            controller.SavePlayerScores();
            DisplayFinalScores();
            return;
        }

        bool shouldShowNews = controller.NextTurn();
        if (shouldShowNews) DisplayNews();

        LoadCurrentPlayerDetails();
        rollDiceButton.interactable = true;
    }

    void LoadCurrentPlayerDetails()
    {
        Player currentPlayer = GameStore.CurrentPlayer;
        Debug.Log($"Player[{currentPlayer.Name}, ${currentPlayer.Credit}]");
        MovePlayerCard();
        PopulatePlayerCard();

        // Select player's piece
        board.SetSelectedPiece(GameStore.CurrentPlayerPos);
        endTurnButton.interactable = false;
    }

    void OnQuizTileActivated()
    {
        // TODO: Launch quiz UI
        Debug.Log("Launching quiz UI...");
    }

    void OnEventTileActivated()
    {
        // TODO: Launch event UI
        Debug.Log("Launching event UI...");
    }

    void ShowStockMarket()
    {
        // TODO: Show view stock market UI
        Debug.Log("Launching stock market UI...");
    }

    void DisplayNews()
    {
        // TODO: Show news UI
        Debug.Log("Launching news UI...");
    }

    // [NOTE] This function corresponds to clicking the end game button. This is NOT the same as
    // the first player reaching 14 laps
    void OnEndGameButtonClick()
    {
        // TODO: Display confirmation message, then end game
        Debug.Log("Ending game...");
    }

    void DisplayFinalScores()
    {
        // TODO: Display all players score
        Player[] players = GameStore.Players;
        foreach (Player player in players)
        {
            Debug.Log($"Player[{player.Name}, ${player.Credit}]");
        }
    }

    void ShowLeaderBoard()
    {
        // TODO: Show leaderboard UI
        Debug.Log("Launching leaderboard Ui...");
    }

    // Must be performed in coroutine to wait for piece to move before performing additional operations
    IEnumerator PerformDiceRoll()
    {
        rollDiceButton.interactable = false;

        int diceValue = controller.GenerateDiceValue();
        rollDiceButton.GetComponentInChildren<Text>().text = diceValue.ToString();

        yield return StartCoroutine(board.MovePiece(diceValue));

        // Check if piece landed or passed through GO tile
        int currentPiecePos = board.GetCurrentPiecePos();
        if (currentPiecePos < diceValue)
        {
            controller.IssueGoPayout();
            // TODO: Display GO payout
            Debug.Log("Received GO payout");
        }

        // Launch tile event if needed
        Tile currentTile = board.tiles[currentPiecePos];
        // No need check for GO tile. Already handled by code above
        switch (currentTile.Type)
        {
            case TileType.Quiz:
                OnQuizTileActivated();
                break;
            case TileType.Event:
                OnEventTileActivated();
                break;
        }

        rollDiceButton.GetComponentInChildren<Text>().text = "Roll Dice";
        endTurnButton.interactable = true;

        yield break;
    }

    void LoadPlayerCard()
    {
        Vector3[] PlayerCardSmallVector = new Vector3[] { new Vector3(-400f, -180f, 0f), new Vector3(-400f, -270f, 0), new Vector3(-400f, -360f, 0), new Vector3(-400f, -450f, 0) };
        Color32[] cardColors = new Color32[] { new Color32(0, 0, 0, 50), new Color32(255, 0, 0, 50), new Color32(0, 255, 0, 50), new Color32(0, 0, 255, 50) };

        for (int i = 0; i < GameStore.Players.Length; i++)
        {
            listPlayerCardsSmall.Add(Instantiate(PlayerCardSmallPrefab) as GameObject);
            listPlayerCardsSmall[i].transform.SetParent(canvas.transform, false);
            listPlayerCardsSmall[i].GetComponent<Image>().color = cardColors[i];
            listPlayerCardsSmall[i].GetComponent<PlayerCardSmall>().playerNameText.text = GameStore.Players[i].Name;
            listPlayerCardsSmall[i].GetComponent<PlayerCardSmall>().credits.text = GameStore.Players[i].Credit.ToString();
            listPlayerCardsSmall[i].GetComponent<RectTransform>().localPosition = PlayerCardSmallVector[i];

            listPlayerCardsBig.Add(Instantiate(PlayerCardBigPrefab) as GameObject);
            listPlayerCardsBig[i].transform.SetParent(canvas.transform, false);
            listPlayerCardsBig[i].GetComponent<Image>().color = cardColors[i];
            listPlayerCardsBig[i].SetActive(false);
        }
    }

    void MovePlayerCard() //Cycles the player cards
    {
        listPlayerCardsBig[GameStore.CurrentPlayerPos].SetActive(true);
        if (GameStore.CurrentPlayerPos == 0)
        {
            listPlayerCardsBig[GameStore.Players.Length - 1].SetActive(false);
        } else
        {
            listPlayerCardsBig[GameStore.CurrentPlayerPos - 1].SetActive(false);
        }
    }

    void PopulatePlayerCard()
    {
        // TODO: Populate big player card with PlayerStock data
        List<PlayerStock> stocks = controller.GetPlayerStocks();

        Transform parent = listPlayerCardsBig[GameStore.CurrentPlayerPos].GetComponent<PlayerCardBig>().content;
        GameObject text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
        text = Instantiate(TextPrefab) as GameObject;
        text.transform.SetParent(parent, false);
    }
}
