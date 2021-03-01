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
        Debug.Log($"{controller.CurrentPlayer.Name} at position {controller.CurrentPlayerPos} playing now");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            board.MovePiece();
        }
    }
}
