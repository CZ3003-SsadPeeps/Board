using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUi : MonoBehaviour
{
    static readonly Color32[] CARD_COLORS = new Color32[] {
        new Color32(240, 98, 146, 255),
        new Color32(186, 102, 199, 255),
        new Color32(125, 133, 201, 255),
        new Color32(145, 164, 174, 255)
    };

    public Board board;
    public Canvas canvas;
    public Button rollDiceButton, endTurnButton, leaderboardButton, homeButton;
    public Text endGameText;
    public Image endGameBackground;
    public GameObject passedGoPopup, PlayerCardSmallPrefab, PlayerCardBigPrefab;

    // TODO: Replace with actual StockTrader & PlayerRecordDAO classes from stock system
    GameController controller = new GameController(new StockTraderTest(), new PlayerRecordDAOTest());
    List<GameObject> listPlayerCardsSmall = new List<GameObject>();
    List<GameObject> listPlayerCardsBig = new List<GameObject>();

    void Start()
    {
        // Uncomment when testing Game UI only
        //GameStore.InitPlayers(new string[] { "Abu", "Banana", "Cherry", "Mewtwo" });
        GeneratePlayerCards();

        // Ensures popup is displayed on top of everything else. Must be done after player cards are generated
        passedGoPopup.transform.SetAsLastSibling();

        LoadCurrentPlayerDetails();
    }

    void Update()
    {
        int currentPlayerCredit = GameStore.CurrentPlayer.Credit;
        listPlayerCardsSmall[GameStore.CurrentPlayerPos].GetComponent<PlayerCardSmall>().SetCredit(currentPlayerCredit);
        listPlayerCardsBig[GameStore.CurrentPlayerPos].GetComponent<PlayerCardBig>().SetCredit(currentPlayerCredit);
    }

    public void RollDice()
    {
        StartCoroutine(PerformDiceRoll());
    }

    public void EndTurn()
    {
        if (board.HasReachedMaxLaps())
        {
            listPlayerCardsBig[GameStore.CurrentPlayerPos].SetActive(false);

            GameObject smallPlayerCard;
            for (int i = 0; i < listPlayerCardsSmall.Count; i++)
            {
                smallPlayerCard = listPlayerCardsSmall[i];
                smallPlayerCard.GetComponent<PlayerCardSmall>().SetPosition(new Vector3(-300 + (200 * i), -180, 0f));
            }

            // Disable all buttons except leaderboard & back
            rollDiceButton.gameObject.SetActive(false);
            endTurnButton.gameObject.SetActive(false);

            endGameBackground.gameObject.SetActive(true);
            endGameText.gameObject.SetActive(true);
            leaderboardButton.gameObject.SetActive(true);
            homeButton.gameObject.SetActive(true);

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

    public void OnHomeButtonClick()
    {
        SceneManager.LoadScene("Home");
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

        listPlayerCardsSmall[GameStore.PrevPlayerPos].GetComponent<PlayerCardSmall>().SetSelected(false);
        listPlayerCardsBig[GameStore.PrevPlayerPos].SetActive(false);

        // Reload player details in case number of credits change
        PlayerCardSmall smallPlayerCard = listPlayerCardsSmall[GameStore.CurrentPlayerPos].GetComponent<PlayerCardSmall>();
        smallPlayerCard.SetSelected(true);

        listPlayerCardsBig[GameStore.CurrentPlayerPos].SetActive(true);
        List<PlayerStock> stocks = controller.GetPlayerStocks();
        PlayerCardBig playerCard = listPlayerCardsBig[GameStore.CurrentPlayerPos].GetComponent<PlayerCardBig>();
        playerCard.SetStockDetails(stocks);

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

            // Display passed GO popup
            RectTransform goPopupTransform = passedGoPopup.GetComponent<RectTransform>();

            yield return StartCoroutine(MovePopupToPos(goPopupTransform, Vector2.zero));
            yield return new WaitForSeconds(3f);
            yield return StartCoroutine(MovePopupToPos(goPopupTransform, new Vector2(0f, -760f)));

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

            smallPlayerCard = cardObject.GetComponent<PlayerCardSmall>();
            smallPlayerCard.SetPosition(new Vector3(-400f, -90 * (i + 1), 0f));
            smallPlayerCard.SetPlayerDetails(player, CARD_COLORS[i]);
            listPlayerCardsSmall.Add(cardObject);

            // Create big player card
            cardObject = Instantiate(PlayerCardBigPrefab);
            cardObject.transform.SetParent(canvas.transform, false);
            cardObject.SetActive(false);

            bigPlayerCard = cardObject.GetComponent<PlayerCardBig>();
            bigPlayerCard.SetPlayerDetails(player, CARD_COLORS[i]);
            listPlayerCardsBig.Add(cardObject);
        }
    }

    IEnumerator MovePopupToPos(RectTransform goPopupTransform, Vector2 targetPos)
    {
        do
        {
            // Current position must be passed using RectTransform property. Otherwise popup will keep jumping back & forth between current & target positions
            goPopupTransform.anchoredPosition = Vector2.MoveTowards(goPopupTransform.anchoredPosition, targetPos, 10f);
            yield return null;
        } while (goPopupTransform.anchoredPosition != targetPos);
        yield break;
    }
}
