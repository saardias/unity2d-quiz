using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionSeen = 0;


    public int GetCorrectAnswers()
    {
        return this.correctAnswers;
    }
    public void IncrementCorrctAnswers()
    {
        this.correctAnswers++;
    }

    public int GetQuestionSeen()
    {
        return this.questionSeen;
    }
    public void IncrementQuestionSeens()
    {
        this.questionSeen++;
    }

    public int CalculateScore()
    {
        return Mathf.RoundToInt(this.correctAnswers / (float)this.questionSeen * 100);
    }
}
