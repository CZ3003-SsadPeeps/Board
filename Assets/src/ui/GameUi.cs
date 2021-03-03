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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    void LoadCurrentPlayerDetails()
    {
        // Get player details & display them
        Player currentPlayer = controller.CurrentPlayer;

        // Select player's piece
        board.SetSelectedPiece(controller.CurrentPlayerPos);
    }

    void RollDice()
    {
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
        bool shouldShowNews = controller.NextTurn();
        if (shouldShowNews) DisplayNews();

        LoadCurrentPlayerDetails();
    }

    void DisplayNews() {}

    void EndGame()
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

        // TODO: Remove when end turn button is implemented
        EndTurn();

        yield break;
    }
}
