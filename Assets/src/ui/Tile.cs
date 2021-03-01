using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileType Type;
    public int NumFreeSpace { get; private set; } = 4;

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;
        NumFreeSpace--;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.tag.Contains("Player")) return;
        NumFreeSpace++;
    }
}
