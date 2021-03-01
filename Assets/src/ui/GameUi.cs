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

    void LoadCurrentPlayerDetails()
    {
        // Get player details & display them
        Player currentPlayer = controller.CurrentPlayer;
        Debug.Log($"{currentPlayer.Name} at position {controller.CurrentPlayerPos} playing now");

        // Select player's piece
        board.SetSelectedPiece(controller.CurrentPlayerPos);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // TODO: Move to Roll Dice click event
            int diceValue = controller.GenerateDiceValue();
            board.MovePiece(diceValue);

            // TODO: Move to End Turn click event
            bool shouldShowNews = controller.NextTurn();
            if (shouldShowNews)
            {
                // TODO: Display news
            }

            LoadCurrentPlayerDetails();
        }
    }
}
