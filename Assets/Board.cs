using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    Tile[] tileObjects;
    public List<Tile> tileList = new List<Tile>();

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        FillTiles();

        for (int i = 0; i < tileList.Count; i++)
        {
            Vector3 currentPos = tileList[i].gameObject.transform.position;

            if (i > 0)
            {
                Vector3 previousPos = tileList[i - 1].gameObject.transform.position;
                Gizmos.DrawLine(previousPos, currentPos);
            }
        }
    }

    void FillTiles()
    {
        tileList.Clear();
        tileObjects = GetComponentsInChildren<Tile>();

        foreach (Tile tile in tileObjects)
        {
            tileList.Add(tile);
        }
    }
}
