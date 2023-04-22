using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string question;
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswerIndex;


    public string getQuestion()
    {
        return this.question;
    }
    public string getAnswer(int index)
    {
        if (index < this.answers.Length)
        {
            return this.answers[index];
        }
        return "invalid answer";
    }

    public int getCorrectAnswerIndex()
    {
        return this.correctAnswerIndex;
    }
}
