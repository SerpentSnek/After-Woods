using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IReset
{
    private static GameManager _instance;
    private GameObject player;
    //private bool isNewGame;
    private Timer timer;

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

    public GameObject Player { get; private set; }

    public Timer Timer { get; private set; }

    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            // why is this here?
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            this.LoadStartScreen();
        }

        //isNewGame = false;
        player = GameObject.FindWithTag("Player"); ;

        if (timer == null)
        {
            gameObject.AddComponent<Timer>();
        }

        this.Reset();
    }

    public void Reset() // have this with a Reset interface instead (done)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.Reset();
        timer.Reset();
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

        var playerController = player.GetComponent<PlayerController>();
        playerController.FoodAmount = 0;

        this.Reset();

        // SceneManager.LoadSceneAsync("whatever game over scene is named");
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        //this.isNewGame = false;
        //playerController = FindObjectOfType<PlayerController>();
        var playerController = player.GetComponent<PlayerController>();
        this.Reset();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}