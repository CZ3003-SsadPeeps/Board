using UnityEngine.UI;

public class PlayerCardSmall : PlayerCard
{
    public Image selectionBackground;

    internal void SetSelected(bool isSelected)
    {
        selectionBackground.gameObject.SetActive(isSelected);
    }
}
