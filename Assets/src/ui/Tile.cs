using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType Type;
    private List<Piece> pieces = new List<Piece>(4);

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Piece") return;
        pieces.Add(other.GetComponentInParent<Piece>());
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag != "Piece") return;
        pieces.RemoveAt(0);
    }

    public int freeSpace()
    {
        return pieces.Count < 4 ? pieces.Count : 0;
    }
}
