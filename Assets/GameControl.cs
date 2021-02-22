using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    Player[] playerObjects;
    public List<Player> playerList = new List<Player>();
    public int playerTurn;

    void Start()
    {
        playerObjects = GameControl.FindObjectsOfType<Player>();

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
}
