using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //SINGLETON
    public static ScoreManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }


    public int valuePickedUp = 0;

    public int totalItems = 0;
    public int maxItems = 3;

    public int score = 100;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !MenuManager.gameIsOver) {

            print("clicked");

            score++;

        }
    }

    public void AddValue(int value)
    {
        valuePickedUp += value;
        totalItems++;
    }

    public void BuyItems()
    {
        score -= valuePickedUp;
        valuePickedUp = 0;
    }

}
