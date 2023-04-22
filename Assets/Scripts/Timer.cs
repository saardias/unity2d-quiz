using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    public bool isAnsweringQuestion { get; set; }
    public bool loadNextQuestion { get; set; }
    float timerValue;
    public float fillFraction { get; set; }

    // Update is called once per frame
    void Update()
    {
        this.updateTimer();
    }

    public void CancelTimer()
    {
        this.timerValue = 0;
    }

    private void updateTimer()
    {
        this.timerValue -= Time.deltaTime;

        if (this.isAnsweringQuestion)
        {
            if (this.timerValue > 0)
            {
                this.fillFraction = this.timerValue / this.timeToCompleteQuestion;
            }
            else
            {
                this.isAnsweringQuestion = false;
                this.timerValue = timeToCompleteQuestion;

            }
        }
        else
        {
            if (this.timerValue > 0)
            {
                this.fillFraction = this.timerValue / this.timeToShowCorrectAnswer;
            }
            else
            {

                this.isAnsweringQuestion = true;
                this.timerValue = timeToCompleteQuestion;
                this.loadNextQuestion = true;
            }
        }

        Debug.Log(isAnsweringQuestion + ":" + timerValue + " = " + fillFraction);
    }


}
