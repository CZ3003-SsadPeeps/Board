using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameInputUi : MonoBehaviour
{
    public InputField[] inputFields;
    public Text[] errorMessages;

    NameInputController controller = new NameInputController();

    public void OnStartButtonClick()
    {
        int i;

        // Dismiss all error messages
        foreach (Text errorMessage in errorMessages)
        {
            errorMessage.gameObject.SetActive(false);
        }

        string[] nameInputs = new string[inputFields.Length];
        for (i = 0; i < inputFields.Length; i++)
        {
            nameInputs[i] = inputFields[i].text;
        }

        bool areNamesValid = true;
        NameValidationResult[] results = controller.SubmitNames(nameInputs);
        NameValidationResult result;
        int otherPos;
        for (i = 0; i < results.Length; i++)
        {
            result = results[i];
            if (result is NameValidationResult.Pass) continue;

            if (result is NameValidationResult.IsBlank)
            {
                areNamesValid = false;
                errorMessages[i].text = "Name cannot be blank!";
                errorMessages[i].gameObject.SetActive(true);
                continue;
            }

            if (result is NameValidationResult.Clash)
            {
                areNamesValid = false;
                otherPos = (result as NameValidationResult.Clash).Pos + 1;
                errorMessages[i].text = $"Name cannot be the same as Player {otherPos}'s name";
                errorMessages[i].gameObject.SetActive(true);
            }
        }

        if (!areNamesValid) return;
        SceneManager.LoadScene("Game");
    }
}
