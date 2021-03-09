using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    static readonly int MAX_LAPS = 14;

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
        for (int i = 0; i < diceValue; i++)
        {
            // Get next tile to move to
            currentBoardPos = (currentBoardPos + 1) % tiles.Length;
            nextTile = tiles[currentBoardPos];

            // Get final position
            Vector3 nextPos = nextTile.GetEmptySlotForPiece(currentPiece);

            // Perform movement
            yield return HopTowards(currentPiece, nextPos, 0.5f);

            yield return new WaitForSeconds(0.1f);
        }

        currentPiece.UpdateBoardPos(diceValue);
        yield break;
    }

    internal int GetCurrentPiecePos()
    {
        return pieces[currentPieceIndex].BoardPos;
    }

    internal bool HasReachedMaxLaps()
    {
        return pieces[currentPieceIndex].NumLaps >= MAX_LAPS;
    }

    IEnumerator HopTowards(Piece piece, Vector3 target, float time)
    {
        float elapsedTime = 0;

        Vector3 center = (piece.transform.position + target) * 0.5f;

        center -= new Vector3(0, 0.5f, 0); //Adjust y value here to increase or decrease arc height (Smaller is higher)

        Vector3 startRelPosition = piece.transform.position - center;
        Vector3 endRelPosition = target - center;

        while(elapsedTime < time)
        {
            piece.transform.position = Vector3.Slerp(startRelPosition, endRelPosition, elapsedTime/time);
            piece.transform.position += center;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        piece.transform.position = target; //Snap to final target in case
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
