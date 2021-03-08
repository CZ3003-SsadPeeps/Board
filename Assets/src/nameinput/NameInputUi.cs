using System;
using UnityEngine;
using UnityEngine.UI;

public class NameInputUi : MonoBehaviour
{
    public InputField[] inputFields;
    public Text[] errorMessages;

    public void OnStartButtonClick()
    {
        // Dismiss all error messages
        foreach(Text errorMessage in errorMessages)
        {
            errorMessage.gameObject.SetActive(false);
        }

        bool areNamesValid = true;
        string inputFieldText;
        int i, j;
        for(i = 0; i < inputFields.Length; i++)
        {
            inputFieldText = inputFields[i].text;
            Debug.Log($"Text at position {i} = {inputFieldText}");

            // Check if name is blank
            if (!ValidateName(inputFieldText))
            {
                areNamesValid = false;
                errorMessages[i].text = "Name cannot be blank!";
                errorMessages[i].gameObject.SetActive(true);
                continue;
            }

            // Check if someone else has the same name
            for (j = i - 1; j >= 0; j--)
            {
                if (inputFieldText != inputFields[j].text) continue;

                areNamesValid = false;
                errorMessages[i].text = $"Name cannot be the same as Player {j + 1}'s name";
                errorMessages[i].gameObject.SetActive(true);
                break;
            }
        }

        Debug.Log($"Are names valid? {areNamesValid}");

        if (!areNamesValid) return;

        // TODO: Go to Game scene
        Debug.Log("Loading game...");
    }

    private bool ValidateName(string name)
    {
        return !String.IsNullOrWhiteSpace(name);
    }
}
