using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerCard : MonoBehaviour
{
    public Text nameText, creditText;
    public Image cardBackground;

    internal void SetPlayerDetails(Player player, Color32 backgroundColor)
    {
        nameText.text = player.Name;
        creditText.text = $"${player.Credit}";
        cardBackground.color = backgroundColor;
    }

    internal void SetCredit(int credit)
    {
        creditText.text = $"${credit}";
    }
}
