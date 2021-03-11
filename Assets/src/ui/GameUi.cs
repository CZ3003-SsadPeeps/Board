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
        GeneratePlayerCards();
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

    public void ShowStockMarket()
    {
        // TODO: Show view stock market UI
        Debug.Log("Launching stock market UI...");
    }

    public void ShowLeaderBoard()
    {
        // TODO: Show leaderboard UI
        Debug.Log("Launching leaderboard Ui...");
    }

    // [NOTE] This function corresponds to clicking the end game button. This is NOT the same as
    // the first player reaching 14 laps
    public void OnEndGameButtonClick()
    {
        // TODO: Display confirmation message, then end game
        Debug.Log("Ending game...");
    }

    void LoadCurrentPlayerDetails()
    {
        Player currentPlayer = GameStore.CurrentPlayer;
        Debug.Log($"Player[{currentPlayer.Name}, ${currentPlayer.Credit}]");
        DisplayCurrentPlayerDetails();

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

    void DisplayNews()
    {
        // TODO: Show news UI
        Debug.Log("Launching news UI...");
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

    void GeneratePlayerCards()
    {
        Color32[] cardColors = new Color32[] { new Color32(240, 98, 146, 255), new Color32(186, 102, 199, 255), new Color32(125, 133, 201, 255), new Color32(145, 164, 174, 255) };

        Player player;
        GameObject cardObject;
        PlayerCardSmall smallPlayerCard;
        PlayerCardBig bigPlayerCard;
        for (int i = 0; i < GameStore.Players.Length; i++)
        {
            player = GameStore.Players[i];

            // Create small player card
            cardObject = Instantiate(PlayerCardSmallPrefab);
            cardObject.transform.SetParent(canvas.transform, false);
            cardObject.GetComponent<RectTransform>().localPosition = new Vector3(-400f, -90 * (i + 1), 0f);

            smallPlayerCard = cardObject.GetComponent<PlayerCardSmall>();
            smallPlayerCard.cardBackground.color = cardColors[i];
            smallPlayerCard.playerNameText.text = player.Name;
            smallPlayerCard.credits.text = player.Credit.ToString();

            listPlayerCardsSmall.Add(cardObject);

            // Create big player card
            cardObject = Instantiate(PlayerCardBigPrefab);
            cardObject.transform.SetParent(canvas.transform, false);
            cardObject.GetComponent<Image>().color = cardColors[i];
            cardObject.SetActive(false);

            bigPlayerCard = cardObject.GetComponent<PlayerCardBig>();
            bigPlayerCard.playerNameText.text = player.Name;
            listPlayerCardsBig.Add(cardObject);
        }
    }

    void DisplayCurrentPlayerDetails() //Cycles the player cards
    {
        listPlayerCardsSmall[GameStore.CurrentPlayerPos].GetComponent<PlayerCardSmall>().selectionBackground.gameObject.SetActive(true);
        listPlayerCardsBig[GameStore.CurrentPlayerPos].SetActive(true);

        listPlayerCardsSmall[GameStore.PrevPlayerPos].GetComponent<PlayerCardSmall>().selectionBackground.gameObject.SetActive(false);
        listPlayerCardsBig[GameStore.PrevPlayerPos].SetActive(false);
        PopulatePlayerCard();
    }

    void PopulatePlayerCard()
    {
        PlayerCardBig playerCard = listPlayerCardsBig[GameStore.CurrentPlayerPos].GetComponent<PlayerCardBig>();

        Transform parent = playerCard.content;
        GameObject textObject;
        Text headerText;

        // Headers
        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(parent, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Company";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(parent, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Quantity";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(parent, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Average Purchase Price";

        textObject = Instantiate(TextPrefab);
        textObject.transform.SetParent(parent, false);
        headerText = textObject.GetComponent<Text>();
        headerText.fontStyle = FontStyle.Bold;
        headerText.text = "Current Stock Price";

        // Stock info
        List<PlayerStock> stocks = controller.GetPlayerStocks();
        foreach (PlayerStock stock in stocks)
        {
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(parent, false);
            textObject.GetComponent<Text>().text = stock.CompanyName;
            
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(parent, false);
            textObject.GetComponent<Text>().text = stock.Quantity.ToString();
            
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(parent, false);
            textObject.GetComponent<Text>().text = $"${stock.AvgPurchasePrice}";
            
            textObject = Instantiate(TextPrefab);
            textObject.transform.SetParent(parent, false);
            textObject.GetComponent<Text>().text = $"${stock.CurrentStockPrice}";
        }

        // Scroll layout by default scrolls to the middle of the list, so must scroll back to top
        playerCard.scrollView.normalizedPosition = new Vector2(0, 1);
    }
}
