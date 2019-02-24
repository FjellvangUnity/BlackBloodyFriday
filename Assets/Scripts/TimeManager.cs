using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public GameObject gameOverCanvas;
    public Text gameOverText;

    public float timer = 5f;
    public Text timerText;

    AudioSource audioSource;



    void Start()
    {
        gameOverCanvas.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {

        if (MenuManager.gameHasStarted)
        {
            MenuManager.gameIsOver = false;
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("n0");
        }

        if(timer <= 9)
        {
            audioSource.Play();
        }

        if (timer <= 0) {
            MenuManager.gameIsOver = true;
            gameOverCanvas.SetActive(true);
            timerText.text = 0.ToString("n0");
            gameOverText.text = String.Format("The store is closed! \n you are {0}$ in debt!!!", ScoreManager.Instance.score);
        }

    }
}
