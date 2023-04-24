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
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;

    [Header(header: "Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    [Header(header: "Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [Header(header: "Timer")]

    [SerializeField] Image TimerImage;
    Timer timer;
    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header(header: "ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete = false;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindAnyObjectByType<ScoreKeeper>();
        progressBar.maxValue = this.questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        float fill = this.timer.fillFraction;
        this.TimerImage.fillAmount = fill;
        if (this.timer.loadNextQuestion)
        {
            if (this.progressBar.value == this.progressBar.maxValue)
            {
                this.isComplete = true;
                return;
            }
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
        this.scoreText.text = "Score: " + this.scoreKeeper.CalculateScore() + "%";

    }

    private void DisplayAnser(int index)
    {
        Image buttonInamge;
        if (index == this.currentQuestion.getCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonInamge = answerButtons[index].GetComponent<Image>();
            buttonInamge.sprite = correctAnswerSprite;
            this.scoreKeeper.IncrementCorrctAnswers();
        }
        else
        {
            this.correctAnswerIndex = this.currentQuestion.getCorrectAnswerIndex();
            string correctAnswer = currentQuestion.getAnswer(correctAnswerIndex);
            questionText.text = "Wrong!, the correct answer is:\n" + correctAnswer;
            buttonInamge = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonInamge.sprite = correctAnswerSprite;
        }
    }

    public void DisplayQuestion()
    {
        this.questionText.text = this.currentQuestion.getQuestion();
        for (int i = 0; i < this.answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonElement = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            string answer = this.currentQuestion.getAnswer(i);
            buttonElement.text = answer;
        }
    }

    private void GetNextQuestion()
    {
        if (this.questions.Count > 0)
        {
            this.setButtonState(true);
            this.SetDefaultButtonSprites();
            this.GetRandomQuestion();
            this.DisplayQuestion();
            this.scoreKeeper.IncrementQuestionSeens();
            progressBar.value++;
        }
    }

    private void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, this.questions.Count);
        currentQuestion = this.questions[index];
        if (this.questions.Contains(currentQuestion))
        {
            this.questions.Remove(currentQuestion);
        }
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
            Image buttonInamge = answerButtons[i].GetComponent<Image>();
            buttonInamge.sprite = this.defaultAnswerSprite;
        }
    }
}
