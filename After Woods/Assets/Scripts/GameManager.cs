using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, Reset
{
    private static GameManager _instance;
    private GameObject player;
    //private bool isNewGame;
    public Timer TimerObject;


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
        get
        {
            return player;
        }
    }

    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    private void Awake()
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

        //isNewGame = false;
        player = GameObject.FindWithTag("Player"); ;

        if (TimerObject == null)
        {
            TimerObject = new Timer(20f);
        }

        this.ResetStats();

    }

    public void ResetStats(GameObject obj = null) // have this with a Reset interface instead (done)
    {
        var playerController = player.GetComponent<PlayerController>();
        playerController.CurrentHp = playerController.TotalHp;
        playerController.TotalRadiation = 0;
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

        var playerController = player.GetComponent<PlayerController>();
        playerController.FoodAmount = 0;

        this.ResetStats();

        // SceneManager.LoadSceneAsync("whatever game over scene is named");
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        //this.isNewGame = false;
        //playerController = FindObjectOfType<PlayerController>();
        var playerController = player.GetComponent<PlayerController>();
        this.ResetStats();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(player);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}