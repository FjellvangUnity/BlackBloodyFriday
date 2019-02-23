using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    

    void Awake()
    {
        Object.DontDestroyOnLoad(transform.gameObject);
        
    }


    //SINGLETON
    private static GameManager instance = null;


    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            instance = new GameManager();
        }

        return instance;
    }

}
