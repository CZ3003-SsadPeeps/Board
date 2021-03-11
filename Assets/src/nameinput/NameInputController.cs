﻿using System;

class NameInputController
{
    internal NameValidationResult[] SubmitNames(string[] names)
    {
        NameValidationResult[] results = new NameValidationResult[names.Length];

        bool areNamesValid = true;
        string name;
        int i, j;
        for (i = 0; i < names.Length; i++)
        {
            name = names[i];

            // Check if name is blank
            if (!ValidateName(name))
            {
                areNamesValid = false;
                results[i] = new NameValidationResult.IsBlank();
                continue;
            }

            // Check if someone else has the same name
            for (j = i - 1; j >= 0; j--)
            {
                if (name != names[j]) continue;

                areNamesValid = false;
                results[i] = new NameValidationResult.Clash(j);
                break;
            }
        }

        if (areNamesValid)
        {
            GameStore.InitPlayers(names);
        }

        return results;
    }

    bool ValidateName(string name)
    {
        return !String.IsNullOrWhiteSpace(name);
    }
}
