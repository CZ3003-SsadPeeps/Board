using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType Type;
    public int NumPiecesOnTile { get; private set; } = 0;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;
        NumPiecesOnTile++;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;
        NumPiecesOnTile--;
    }
}
