
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour

{



    public GameObject mainMenuCanvas;

    public static bool gameHasStarted = false;
    public static bool gameIsPaused = false;
    public static bool gameIsOver = false;

    static bool menuIsActive = true;

    Scene currentScene;


    public  GameObject startGameButton;
    public GameObject restartGameButton;
    public GameObject continueButton;
    public GameObject highScoreButton;


    AudioSource audioSource;
    public AudioClip backgroundMusic;



    void Awake()
    {
        //Object.DontDestroyOnLoad(transform.gameObject);

        currentScene = SceneManager.GetActiveScene();

        audioSource = GetComponent<AudioSource>();

        if (currentScene.name == "Menu")
        {
            menuIsActive = true;
            gameHasStarted = false;

            restartGameButton.SetActive(false);
            continueButton.SetActive(false);
            highScoreButton.SetActive(true);
        }
        else if (currentScene.name == "TestStartGame")
        {

            GameObject.Find("Menu").SetActive(true);
           
            mainMenuCanvas.SetActive(false);
            menuIsActive = false;
            gameHasStarted = true;
            startGameButton.SetActive(false);
            highScoreButton.SetActive(false);

            audioSource.PlayOneShot(backgroundMusic);

        }


     

    }
    
    void Update()
    {

       // print("game is over: " + MenuManager.gameIsOver);


        if (gameHasStarted) {

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                menuIsActive = !menuIsActive;

                if (menuIsActive)
                {
                    mainMenuCanvas.SetActive(true);
                    Time.timeScale = 0;
                }
                else
                {
                    UnpauseGame();
                }
            }
        }       
        
    }


    public void StartGame() {
        gameHasStarted = true;
        gameIsOver = false;
        SceneManager.LoadScene("TestStartGame");
        ScoreManager.score = 0;
        

    }

    public void ContinueGame() {
        UnpauseGame();
    }


    void UnpauseGame() {
        mainMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame() {
        
        SceneManager.LoadScene("TestStartGame");
        gameIsOver = false;
        //mainMenuCanvas.SetActive(false);
    }


    public void GetHighScore() {


    }

    public void QuitGame() {
        print("Quit");
        Application.Quit();
    }





    //SINGLETON
    private static MenuManager instance = null;


    public static MenuManager GetInstance()
    {
        if (instance == null)
        {
            instance = new MenuManager();
        }

        return instance;
    }
}
