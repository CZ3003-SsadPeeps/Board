using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    // UI Components
    public Board board;
    public Button rollDiceButton;

    private GameController controller = new GameController();
    Player[] playerObjects;
    public List<Player> playerList = new List<Player>();
    public int playerTurn;

    void Start()
    {
        playerObjects = GameUi.FindObjectsOfType<Player>();

        playerTurn = 0;

        int playerNumber = 1;

        foreach (Player player in playerObjects)
        {
            player.playerNumber = playerNumber;
            playerNumber++;
            playerList.Add(player);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player " + playerList[playerTurn].playerNumber + "'s turn");
            playerList[playerTurn].piece.Move();
            playerTurn++;
            playerTurn %= 4;
        }
    }

    public void OnRollDiceButtonClick()
    {
        int diceValue = controller.GenerateDiceValue();

        // Display dice value
        Debug.Log($"Dice value = {diceValue}");
        rollDiceButton.GetComponentInChildren<Text>().text = diceValue.ToString();
        StartCoroutine(ResetRollDiceButtonText());

        // Inform board to move piece
        Tile landedTile = board.MovePiece(diceValue);

        // Check if player has passed GO
        // If so, add to player credit & display popup

        // Check tile type & launch relevant event
        // For now it's a random check to see if we indeed get a Tile object
        Debug.Log($"Max players on tile = {landedTile.playerOnTileArr.Length}");
    }

    private IEnumerator ResetRollDiceButtonText()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        rollDiceButton.GetComponentInChildren<Text>().text = "Roll Dice";

        // Stop this coroutine, otherwise will loop forever
        yield break;
    }
}
