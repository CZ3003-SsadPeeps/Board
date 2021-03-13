using UnityEngine;
using UnityEngine.UI;

public class PlayerCardSmall : PlayerCard
{
    public Image selectionBackground;

    internal void SetPosition(Vector2 pos)
    {
        transform.localPosition = pos;
    }

    internal void SetSelected(bool isSelected)
    {
        selectionBackground.gameObject.SetActive(isSelected);
    }
}
