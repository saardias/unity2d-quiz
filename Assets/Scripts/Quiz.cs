using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [Header(header: "Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

    [Header(header: "Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;
    [Header(header: "Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header(header: "Timer")]

    [SerializeField] Image TimerImage;
    Timer timer;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        this.DisplayQuestion();
    }

    private void Update()
    {
        float fill = this.timer.fillFraction;
        this.TimerImage.fillAmount = fill;
        if (this.timer.loadNextQuestion)
        {
            this.hasAnsweredEarly = false;
            this.GetNextQuestion();
            this.timer.loadNextQuestion = false;
        }
        else if (!this.hasAnsweredEarly && !this.timer.isAnsweringQuestion)
        {
            DisplayAnser(-1);
            setButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        this.hasAnsweredEarly = true;
        DisplayAnser(index);
        this.setButtonState(false);
        timer.CancelTimer();
    }

    private void DisplayAnser(int index)
    {
        Image buttonInamge;
        if (index == this.question.getCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonInamge = answerButtons[index].GetComponent<Image>();
            buttonInamge.sprite = correctAnswerSprite;
        }
        else
        {
            this.correctAnswerIndex = this.question.getCorrectAnswerIndex();
            string correctAnswer = question.getAnswer(correctAnswerIndex);
            questionText.text = "Wrong!, the correct answer is:\n" + correctAnswer;
            buttonInamge = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonInamge.sprite = correctAnswerSprite;
        }
    }

    public void DisplayQuestion()
    {
        this.questionText.text = this.question.getQuestion();
        for (int i = 0; i < this.answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonElement = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answer = this.question.getAnswer(i);
            buttonElement.text = answer;
        }
    }

    private void GetNextQuestion()
    {
        this.setButtonState(true);
        this.SetDefaultButtonSprites();
        this.DisplayQuestion();
    }


    private void setButtonState(bool state)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    private void SetDefaultButtonSprites()
    {
        for (int i = 0; i < this.answerButtons.Length; i++)
        {
            Image buttonInamge = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonInamge.sprite = this.defaultAnswerSprite;
        }
    }
}
