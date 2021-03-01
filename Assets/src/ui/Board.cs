using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private static readonly int MAX_LAPS = 14;

    public Piece[] pieces;
    public Tile[] tiles;

    int currentPieceIndex = 0;

    internal void SetSelectedPiece(int index)
    {
        currentPieceIndex = index;
    }

    internal int MovePiece(int diceValue)
    {
        // TODO: Replace with actual logic
        return 0;
    }

    internal bool hasReachedMaxLaps()
    {
        // TODO: Replace with actual logic
        return false;
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.green;

        //for (int i = 0; i < tiles.Length; i++)
        //{
        //    Vector3 currentPos = tiles[i].gameObject.transform.position;

        //    if (i > 0)
        //    {
        //        Vector3 previousPos = tiles[i - 1].gameObject.transform.position;
        //        Gizmos.DrawLine(previousPos, currentPos);
        //    }
        //}
    }
}
