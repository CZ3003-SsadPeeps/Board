using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Piece[] pieces;
    public Tile[] tileObjects;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < tileObjects.Length; i++)
        {
            Vector3 currentPos = tileObjects[i].gameObject.transform.position;

            if (i > 0)
            {
                Vector3 previousPos = tileObjects[i - 1].gameObject.transform.position;
                Gizmos.DrawLine(previousPos, currentPos);
            }
        }
    }

    internal void MovePiece()
    {

    }
}
