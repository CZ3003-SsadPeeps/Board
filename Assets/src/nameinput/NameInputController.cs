using System;

class NameInputController
{
    internal NameValidationResult[] ValidateNames(string[] names)
    {
        NameValidationResult[] results = new NameValidationResult[names.Length];

        string name;
        int i, j;
        for (i = 0; i < names.Length; i++)
        {
            name = names[i];

            // Check if name is blank
            if (!ValidateName(name))
            {
                results[i] = new NameValidationResult.IsBlank();
                continue;
            }

            // Check if someone else has the same name
            for (j = i - 1; j >= 0; j--)
            {
                if (name != names[j]) continue;

                results[i] = new NameValidationResult.Clash(j);
                break;
            }
        }

        return results;
    }

    private bool ValidateName(string name)
    {
        return !String.IsNullOrWhiteSpace(name);
    }
}
