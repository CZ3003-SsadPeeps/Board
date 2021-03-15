using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionOptionRecord
{
    public string Content;
    public bool isCorrect;

    public QuestionOptionRecord(string Content, bool isCorrect)
    {
        this.Content = Content;
        this.isCorrect = isCorrect;
    }
}
