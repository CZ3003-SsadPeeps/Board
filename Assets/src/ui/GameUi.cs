﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Board board;
    public Canvas canvas;
    public Button rollDiceButton, endTurnButton;
    public GameObject PlayerCardSmallPrefab, PlayerCardBigPrefab;

    private GameController controller = new GameController();
    private List<GameObject> playerCardsSmall = new List<GameObject>();

    void Start()
    {
        // TODO: Remove when name input UI is implemented
        controller.SetPlayerNames(new string[] { "Apple", "Banana", "Cherry", "Mewtwo" });
        LoadCurrentPlayerDetails();
        LoadPlayerCard();
        MovePlayerCard();
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
        MovePlayerCard();
    }

    void LoadCurrentPlayerDetails()
    {
        // TODO: Display player details
        Player currentPlayer = controller.CurrentPlayer;
        Debug.Log($"Player[{currentPlayer.Name}, ${currentPlayer.Credit}]");

        // Select player's piece
        board.SetSelectedPiece(controller.CurrentPlayerPos);
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
        Player[] players = controller.Players;
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
        Color32[] cardColors = new Color32[4] { new Color32(0, 0, 0, 50), new Color32(255, 0, 0, 50), new Color32(0, 255, 0, 50), new Color32(0, 0, 255, 50) };

        for (int i = 0; i < controller.Players.Length; i++)
        {
            playerCardsSmall.Add(Instantiate(PlayerCardSmallPrefab) as GameObject);
            playerCardsSmall[i].transform.SetParent(canvas.transform, false);
            playerCardsSmall[i].GetComponent<Image>().color = cardColors[i];
            playerCardsSmall[i].GetComponent<PlayerCardSmall>().playerNameText.text = controller.Players[i].Name;
            playerCardsSmall[i].GetComponent<PlayerCardSmall>().credits.text = controller.Players[i].Credit.ToString();
        }
    }

    void MovePlayerCard() //Cycles the small player cards
    {
        Vector3[] PlayerCardSmallVector = new Vector3[3] { new Vector3(-400f, -120f, 0f), new Vector3(-400f, -210f, 0), new Vector3(-400f, -300f, 0) };

        int slot = 0;

        int i = 1;

        playerCardsSmall[controller.CurrentPlayerPos].SetActive(false);

        while (slot < 3)
        {
            playerCardsSmall[(controller.CurrentPlayerPos + i) % 4].SetActive(true);
            playerCardsSmall[(controller.CurrentPlayerPos + i) % 4].GetComponent<RectTransform>().localPosition = PlayerCardSmallVector[slot];
            i++;
            slot++;
        }

    }
}
