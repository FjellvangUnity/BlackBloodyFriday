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
    private bool activated = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        activated = false;
    }

    void Start()
    {
        gameOverCanvas.SetActive(false);
    }


    void Update()
    {

        if (MenuManager.gameHasStarted)
        {
            MenuManager.gameIsOver = false;
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("n0");
        }

        if(timer <= 9.0f)
        {
            if(!activated)
            {
            audioSource.Play();
            activated = true;
            }
        }

        if (timer <= 0) {
            MenuManager.gameIsOver = true;
            gameOverCanvas.SetActive(true);
            timerText.text = 0.ToString("n0");
			gameOverText.text = GetText(ScoreManager.Instance.score);// String.Format("The store is closed! \n you are {0}$ in debt!!!", ScoreManager.Instance.score);
        }

    }

	string GetText(int score)
	{
		if (score == 100)
		{
			return "The store is closed. But you didnt save any money ?\nBlack friday is about saving money";
		}
		else if (score > -1500)
		{
			return string.Format("POOR!\n You suck! you almost didn't save any money. You're only {0} in debt", score);
		}

		else if (score > -6000)
		{
			return string.Format("BROKE!\n Hmm - you could have saved alot more\n You're {0} in debt", score);
		}
		else
		{
			return string.Format("BANKRUPT!\n Congratulations, you totally saved TONS of money!\n You will never pay back the {0} you're in debt", score);
		}
	}


	/*
	 * BANKRUPT!
CONGRATULATIONS - You totally saved TONS of money!

RUINED!
Its OK - You saved A LOT of money!��

BROKE!
Hmmm - You could have saved much more!��

POOR!
You suck! You almost didn't save any money.
	 * */
}
