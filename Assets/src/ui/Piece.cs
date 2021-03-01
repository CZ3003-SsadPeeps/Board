using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public int NumLaps { get; private set; } = 0;
    public int BoardPos { get; private set; } = 0;

    internal void UpdateBoardPos(int numTilesMoved)
    {
        int prevBoardPos = BoardPos;
        BoardPos = (BoardPos + numTilesMoved) % 12;
        if (BoardPos < prevBoardPos) NumLaps++;
    }
}
