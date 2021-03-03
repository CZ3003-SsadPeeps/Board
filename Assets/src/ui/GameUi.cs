﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public Board board;
    public Button rollDiceButton;

    private GameController controller = new GameController();

    void Start()
    {
        // TODO: Remove when name input UI is implemented
        controller.SetPlayerNames(new string[] { "Apple", "Banana", "Cherry", "Mewtwo" });
    }

    public void RollDice()
    {
        // TODO: Disable clicks on roll dice button
        StartCoroutine(PerformDiceRoll());
    }

    void LoadCurrentPlayerDetails()
    {
        // TODO: Display player details
        Player currentPlayer = controller.CurrentPlayer;

        // Select player's piece
        board.SetSelectedPiece(controller.CurrentPlayerPos);

        // TODO: Disable clicks on end turn button
    }

    void OnQuizTileActivated()
    {
        // TODO: Launch quiz UI
    }

    void OnEventTileActivated()
    {
        // TODO: Launch event UI
    }

    void ShowStockMarket()
    {
        // TODO: Show view stock market UI
    }

    void EndTurn()
    {
        if (board.HasReachedMaxLaps())
        {
            // TODO: Disable all buttons except leaderboard & back
            controller.SavePlayerScores();

            // TODO: Display all players score
            Player[] players = controller.Players;
            return;
        }

            bool shouldShowNews = controller.NextTurn();

        if (shouldShowNews) DisplayNews();

        LoadCurrentPlayerDetails();

        // TODO: Enable clicks on roll dice button
    }

    void DisplayNews() {}

    // [NOTE] This function corresponds to clicking the end game button. This is NOT the same as
    // the first player reaching 14 laps
    void OnEndGameButtonClick()
    {
        // TODO: Display confirmation message, then end game
    }

    void DisplayFinalScores() {}

    void ShowLeaderBoard()
    {
        // TODO: Show leaderboard UI
    }

    // Must be performed in coroutine to wait for piece to move before performing additional operations
    IEnumerator PerformDiceRoll()
    {
        int diceValue = controller.GenerateDiceValue();
        rollDiceButton.GetComponentInChildren<Text>().text = diceValue.ToString();

        yield return StartCoroutine(board.MovePiece(diceValue));

        // Check if piece landed or passed through GO tile
        int currentPiecePos = board.GetCurrentPiecePos();
        if (currentPiecePos < diceValue)
        {
            controller.IssueGoPayout();
            // TODO: Display GO payout
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

        // TODO: Remove when end turn button is implemented
        EndTurn();

        // TODO: Enable clicks on end turn button

        yield break;
    }
}
