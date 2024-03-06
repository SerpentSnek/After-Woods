﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, Reset
{
    private static GameManager _instance;
    public Timer TimerObject;
    protected GameObject Player
    {
        get
        {
            return GameObject.FindWithTag("Player");
        }
    }
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("null game manager");
            }

            return _instance;
        }
    }
    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    private void Start()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            this.LoadStartScreen();
        }
        if (TimerObject == null)
        {
            TimerObject = new Timer(20f);
        }

        this.ResetStats();
    }

    public void ResetStats(GameObject obj = null) // have this with a Reset interface instead (done)
    {
        Instance.Player.GetComponent<PlayerController>().CurrentHp =
            Instance.Player.GetComponent<PlayerController>().TotalHp;
        Instance.Player.GetComponent<PlayerController>().TotalRadiation = 0;
        TimerObject.ResetStats();
    }

    // Main menu is just a placeholder to test scene switching;
    // may turn into pause screen or something 
    public void LoadMainMenu()
    {
        DontDestroyOnLoad(gameObject);
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        SceneManager.LoadSceneAsync("MainMenuPlaceholder");
    }

    public void LoadStartScreen()
    {
        DontDestroyOnLoad(gameObject);
        // SceneManager.LoadSceneAsync("whatever start scene is named");
    }

    public void LoadGameOverScreen()
    {
        DontDestroyOnLoad(gameObject);
        this.ResetStats();
        Instance.Player.GetComponent<PlayerController>().FoodAmount = 0;
        // SceneManager.LoadSceneAsync("whatever game over scene is named");
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        this.ResetStats();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Instance.Player);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}