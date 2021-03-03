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

    internal IEnumerator MovePiece(int diceValue)
    {
        Piece currentPiece = pieces[currentPieceIndex];
        int currentBoardPos = currentPiece.BoardPos;

        Tile nextTile;
        for (int i = 0; i < numSteps; i++)
        {
            // Get next tile to move to
            currentBoardPos = (currentBoardPos + 1) % tiles.Length;
            nextTile = tiles[currentBoardPos];

            // Get final position
            Vector3 nextPos = nextTile.GetEmptySlotForPiece(currentPiece);

            // Perform movement
            while (MoveToNextTile(currentPiece, nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);
        }

        currentPiece.UpdateBoardPos(numSteps);
        yield break;
    }

    internal Tile getCurrentTile()
    {
        int boardPos = pieces[currentPieceIndex].BoardPos;
        return tiles[boardPos];
    }

    internal bool hasReachedMaxLaps()
    {
        return pieces[currentPieceIndex].NumLaps >= MAX_LAPS;
    }

    bool MoveToNextTile(Piece piece, Vector3 target)
    {
        return target != (piece.transform.position = Vector3.MoveTowards(piece.transform.position, target, 2f * Time.deltaTime));
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
