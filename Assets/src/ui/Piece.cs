using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    int numLaps = 0;
    public int BoardPos { get; private set; } = 0;

    internal void UpdateBoardPos(int numTilesMoved)
    {
        BoardPos += numTilesMoved;
    }

    //public void Move()
    //{
    //    if (!isMoving)
    //    {
    //        steps = Random.Range(1, 7);
    //        Debug.Log("Dice rolled " + steps + ", Current Position " + boardPosition + ", Next Position " + ((boardPosition + steps) % currentBoard.tileList.Count));
    //        StartCoroutine(EnumMove());
    //    }
    //}

    //IEnumerator EnumMove()
    //{
    //    if (isMoving)
    //    {
    //        yield break;
    //    }

    //    isMoving = true;

    //    while (steps > 0)
    //    {
    //        boardPosition++;
    //        boardPosition %= currentBoard.tileList.Count;

    //        Vector3 nextPos = currentBoard.tileList[boardPosition].gameObject.transform.position;

    //        switch (currentBoard.tileList[boardPosition].freeSpace())
    //        {
    //            case 1:
    //                nextPos.Set(nextPos.x + 0.2f, 0, nextPos.z + 0.2f);
    //                break;
    //            case 2:
    //                nextPos.Set(nextPos.x + 0.2f, 0, nextPos.z - 0.2f);
    //                break;
    //            case 3:
    //                nextPos.Set(nextPos.x - 0.2f, 0, nextPos.z + 0.2f);
    //                break;
    //        }

    //        while (MoveToNextTile(nextPos))
    //        {
    //            yield return null;
    //        }

    //        yield return new WaitForSeconds(0.15f);

    //        steps--;
    //    }

    //    isMoving = false;
    //}

    //bool MoveToNextTile(Vector3 target)
    //{
    //    return target != (transform.position = Vector3.MoveTowards(transform.position, target, 2f * Time.deltaTime));
    //}
}
