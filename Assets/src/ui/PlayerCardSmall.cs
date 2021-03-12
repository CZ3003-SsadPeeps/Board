using UnityEngine;
using UnityEngine.UI;

public class PlayerCardSmall : MonoBehaviour
{
    public Text credits;
    public Text playerNameText;
    public Image cardBackground, selectionBackground;

    internal void SetPlayerDetails(Player player, Color32 backgroundColor)
    {
        playerNameText.text = player.Name;
        credits.text = $"${player.Credit}";
        cardBackground.color = backgroundColor;
    }
}
