﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IReset
{
    private static GameManager _instance;
    private GameObject player;
    private Timer timer;
    private int currentStage;
    private PlayerData checkpointInfo;

    class PlayerData
    {
        public float hp, radiation;
        public int food;
    }

    //private GameObject[] allFoods;
    [SerializeField] private GameObject playerPrefab;

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

    public GameObject Player
    {
        get => player;
    }
    public Timer Timer
    {
        get => timer;
    }
    public int CurrentStage
    {
        get => currentStage; set => currentStage = value;
    }

    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    void Awake()
    {
        currentStage = SceneManager.GetActiveScene().buildIndex;
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
        }
        timer = gameObject.GetComponent<Timer>();
        checkpointInfo = new PlayerData();
        checkpointInfo.hp = player.GetComponent<PlayerLogicController>().TotalHp;
    }


    public void Reset() // have this with a Reset interface instead (done)
    {
        try
        {
            var playerController = player.GetComponent<PlayerLogicController>();
            playerController.Reset();
            timer.Reset();
        }
        catch
        {
            Debug.Log("null player controller (you are probably in a non-stage scene)");
        }
    }

    // Return to title (reset all player stats i.e. destroy the player object)
    public void LoadMainMenu()
    {
        this.Reset();
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadStartStage()
    {
        if (timer != null)
        {
            timer.Reset();
            timer.IsActive = true;
        }
        if (player != null)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.transform.position = new Vector3(0, 0, -1);
        }
        SceneManager.LoadSceneAsync("Stage1v2");
        //https://gamedev.stackexchange.com/questions/153707/how-to-get-current-scene-name
        currentStage = SceneManager.GetSceneByName("Stage1v2").buildIndex;
        Debug.Log("stage at start: " + currentStage);
    }

    public void LoadGameOverScreen()
    {
        // Don't destroy the player just yet in case they want to start from a checkpoint.
        // player.GetComponent<PlayerLogicController>().CurrentHp = player.GetComponent<PlayerLogicController>().TotalHp;
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void LoadCurrentStage()
    {
        player.GetComponent<PlayerLogicController>().Reset();
        player.GetComponent<PlayerLogicController>().CurrentHp = checkpointInfo.hp;
        player.GetComponent<PlayerLogicController>().CurrentRadiation = checkpointInfo.radiation;
        player.GetComponent<PlayerLogicController>().FoodAmount = checkpointInfo.food;
        timer.Reset();
        SceneManager.LoadSceneAsync(currentStage);
        Debug.Log("stage from load: " + currentStage);
        //currentStage = SceneManager.GetActiveScene().buildIndex;
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        // Only reset HP upon advancing to next stage and set player to starting position
        player.GetComponent<PlayerLogicController>().CurrentHp += 20f;
        timer.Reset();
        DontDestroyOnLoad(gameObject);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 0, -1);

        checkpointInfo.hp = player.GetComponent<PlayerLogicController>().CurrentHp;
        checkpointInfo.radiation = player.GetComponent<PlayerLogicController>().CurrentRadiation;
        checkpointInfo.food = player.GetComponent<PlayerLogicController>().FoodAmount;
        SceneManager.LoadSceneAsync(currentStage + 1);
        currentStage++;
        Debug.Log("next stage: " + currentStage);
    }
}