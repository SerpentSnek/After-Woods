using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IReset
{
    private static GameManager _instance;
    private GameObject player;
    //private bool isNewGame;
    private Timer timer;
    private int currentStage;
    private GameObject ui;

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
            //return;
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);

            //this.LoadStartStage();
        }

        //isNewGame = false;
        player = GameObject.FindWithTag("Player");
        ui = GameObject.Find("UI v2");

        if (timer == null)
        {
            timer = gameObject.GetComponent<Timer>();
        }

        //currentStage = SceneManager.GetSceneByName("MainMenu");

        //this.Reset();
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

    // Main menu is just a placeholder to test scene switching;
    // may turn into pause screen or something 
    public void LoadMainMenu()
    {
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        DontDestroyOnLoad(gameObject);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
    }

    public void LoadStartStage()
    {
        if (timer != null)
        {
            timer.Reset();
        }
        if (player != null)
        {
            //player.transform.position = new Vector2(0, 0);
            DontDestroyOnLoad(player);
            //    Destroy(this.player);
            //    Destroy(this.gameObject);
        }
        //else
        //{
        //DontDestroyOnLoad(player);
        //}

        SceneManager.LoadSceneAsync("Stage1v2");
        //https://gamedev.stackexchange.com/questions/153707/how-to-get-current-scene-name
        currentStage = SceneManager.GetSceneByName("Stage1v2").buildIndex;
        Debug.Log("stage at start: " + currentStage);
    }

    public void LoadGameOverScreen()
    {
        this.Reset();
        this.player.transform.position = Vector2.zero;
        //DontDestroyOnLoad(player);
        //DontDestroyOnLoad(gameObject);
        //currentStage = SceneManager.GetSceneByName("GameOver").buildIndex;
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void LoadCurrentStage()
    {
        this.player.transform.position = Vector2.zero;
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
        //currentStage = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentStage);
        Debug.Log("stage from load: " + currentStage);
        //currentStage = SceneManager.GetActiveScene().buildIndex;
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        // Only reset HP upon advancing to next stage and set player to starting position
        player.GetComponent<PlayerLogicController>().CurrentHp = player.GetComponent<PlayerLogicController>().TotalHp;
        timer.Reset();
        player.transform.position = Vector2.zero;
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
        //currentStage += 1;
        currentStage = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentStage + 1);
        currentStage = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("next stage: " + currentStage);

    }
    //void Update()
    //{
    //    try
    //    {
    //        if (player.GetComponent<PlayerLogicController>().CurrentHp <= 0)
    //        {
    //            LoadGameOverScreen();
    //        }
    //        if (player.GetComponent<PlayerLogicController>().IsFinished)
    //        {
    //            LoadNextStage();
    //        }
    //    }
    //    catch
    //    {
    //        Debug.Log("null player controller");
    //    }
    //}
}