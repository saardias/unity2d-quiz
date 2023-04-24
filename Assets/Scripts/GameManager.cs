using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endScreen;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindAnyObjectByType<EndScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false);
    }


    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.ShowFinalScore();
            endScreen.gameObject.SetActive(true);
        }
    }

    public void onReplayLavel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
