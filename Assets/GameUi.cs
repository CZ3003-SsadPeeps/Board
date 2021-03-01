using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    // UI Components
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
        Debug.Log($"Dice value = {diceValue}");
        rollDiceButton.GetComponentInChildren<Text>().text = diceValue.ToString();
        StartCoroutine(ResetRollDiceButtonText());
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
