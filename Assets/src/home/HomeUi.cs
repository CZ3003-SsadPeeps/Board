using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUi : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        GameStore.Reset();
        SceneManager.LoadScene("Game");
    }
}
