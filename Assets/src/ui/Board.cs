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

    internal void MovePiece(int diceValue)
    {
        Debug.Log($"Moving {diceValue} steps");
        StartCoroutine(EnumMove(diceValue));
    }

    internal bool hasReachedMaxLaps()
    {
        return pieces[currentPieceIndex].NumLaps >= MAX_LAPS;
    }

    IEnumerator EnumMove(int numSteps)
    {
        Piece currentPiece = pieces[currentPieceIndex];
        int currentBoardPos = currentPiece.BoardPos;

        Tile nextTile;
        for (int i = 0; i < numSteps; i++)
        {
            currentBoardPos = (currentBoardPos + 1) % tiles.Length;
            nextTile = tiles[currentBoardPos];

            Vector3 nextPos = nextTile.gameObject.transform.position;
            switch (nextTile.freeSpace())
            {
                case 1:
                    nextPos.Set(nextPos.x + 0.2f, 0, nextPos.z + 0.2f);
                    break;
                case 2:
                    nextPos.Set(nextPos.x + 0.2f, 0, nextPos.z - 0.2f);
                    break;
                case 3:
                    nextPos.Set(nextPos.x - 0.2f, 0, nextPos.z + 0.2f);
                    break;
            }

            while (MoveToNextTile(currentPiece, nextPos))
            {
                yield return null;
            }

            yield return new WaitForSeconds(0.15f);
        }

        currentPiece.UpdateBoardPos(numSteps);
        yield break;
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
