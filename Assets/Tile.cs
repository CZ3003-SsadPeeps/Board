using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Event Event;

    public Player[] playerOnTileArr = new Player[4];

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < playerOnTileArr.Length; i++)
            {
                if (playerOnTileArr[i] == null)
                {
                    playerOnTileArr[i] = other.GetComponentInParent<Player>();
                    break;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < playerOnTileArr.Length; i++)
            {
                if (playerOnTileArr[i] == other.GetComponentInParent<Player>())
                {
                    playerOnTileArr[i] = null;
                    break;
                }
            }
        }
    }

    public int freeSpace()
    {
        for (int i = 0; i < playerOnTileArr.Length; i++)
        {
            if (playerOnTileArr[i] == null)
            {
                return i;
            }
        }

        return 0;
    }
}
