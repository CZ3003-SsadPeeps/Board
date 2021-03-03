using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUi : MonoBehaviour
{
    public Board board;

    private GameController controller = new GameController();

    void Start()
    {
        controller.SetPlayerNames(new string[] { "Apple", "Banana", "Cherry", "Mewtwo" });
    }

    void Update()
    {
        // TODO: Remove when roll dice button is implemented
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    void LoadCurrentPlayerDetails()
    {
        // TODO: Display player details
        Player currentPlayer = controller.CurrentPlayer;

        // Select player's piece
        board.SetSelectedPiece(controller.CurrentPlayerPos);

        // TODO: Disable clicks on end turn button
    }

    void RollDice()
    {
        // TODO: Disable clicks on roll dice button
        StartCoroutine(PerformDiceRoll());
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

        // TODO: Remove when end turn button is implemented
        EndTurn();

        // TODO: Enable clicks on end turn button

        yield break;
    }
}
