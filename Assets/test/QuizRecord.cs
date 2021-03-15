using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizRecord
{
    public int QuizID;
    public string Question;
    public int Reward;
    public string Difficulty;
    public List<QuestionOptionRecord> QuestionOptions;

    public QuizRecord(int QuizID, string Question, int Reward, string Difficulty, List<QuestionOptionRecord> QuestionOptions)
    {
        this.QuizID = QuizID;
        this.Question = Question;
        this.Reward = Reward;
        this.Difficulty = Difficulty;
        this.QuestionOptions = QuestionOptions;
    }
}
