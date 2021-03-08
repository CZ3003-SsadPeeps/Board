using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUi : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
