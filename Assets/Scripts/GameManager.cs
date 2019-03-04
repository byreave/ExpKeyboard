using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public string text;
    [HideInInspector]
    public int scores;

    private int level = 0;

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        LoadFile();

    }

    private void Start()
    {
    }

    private void Update()
    {



        if(level == 1)
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Application.Quit();
            else if (Input.GetKeyDown(KeyCode.R))
                RestartGame();
        }
    }
    void LoadFile()
    {
        TextAsset programData = Resources.Load("Programming") as TextAsset;
        text = programData.text;
    }

    public void GameEnd()
    {
        level = 1;
        SceneManager.LoadScene("EndGame");
    }

    void RestartGame()
    {
        level = 0;
        scores = 0;
        SceneManager.LoadScene("MainScene");
        
    }
}
