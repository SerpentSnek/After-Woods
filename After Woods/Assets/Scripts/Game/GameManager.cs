using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IReset
{
    private static GameManager _instance;
    private GameObject player;
    private GameObject savedPlayer;
    //private bool isNewGame;
    private Timer timer;
    private int currentStage;
    private GameObject[] allFoods;
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
            //return;
        }
        else
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
            //this.LoadStartStage();
        }

        //isNewGame = false;
        //player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            player = Instantiate(playerPrefab, new Vector3(0, 0, -1), Quaternion.identity, gameObject.transform);
            //savedPlayer = player;
        }
        //else
        //{
        //    DontDestroyOnLoad(player);
        //}
        //ui = GameObject.Find("UI v2");
        timer = gameObject.GetComponent<Timer>();
        //currentStage = SceneManager.GetActiveScene().buildIndex;
        //timer = GameObject.Find("Timer").GetComponent<Timer>();
        Debug.Log(timer);

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

    // Return to title (reset all player stats i.e. destroy the player object)
    public void LoadMainMenu()
    {
        this.Reset();
        Destroy(this.player);
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        DontDestroyOnLoad(gameObject);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
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
            //DontDestroyOnLoad(player);
            //player.transform.position = new Vector2(0, 0);
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            player.transform.position = new Vector3(0, 0, -1);
            //Destroy(this.player);
            //    Destroy(this.gameObject);
        }
        //DontDestroyOnLoad(gameObject);

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
        // Send the player elsewhere. Do not fully reset player here.
        player.GetComponent<PlayerLogicController>().CurrentHp = player.GetComponent<PlayerLogicController>().TotalHp;
        //player.transform.position = new Vector2(0, 500);
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
        //currentStage = SceneManager.GetSceneByName("GameOver").buildIndex;
        SceneManager.LoadSceneAsync("GameOver");
    }

    public void LoadCurrentStage()
    {
        timer.Reset();
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 0, -1);
        //Destroy(this.player);
        //DontDestroyOnLoad(player);
        //DontDestroyOnLoad(gameObject);
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
        //DontDestroyOnLoad(player);
        //player = savedPlayer;
        DontDestroyOnLoad(gameObject);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 0, -1);

        //currentStage += 1;
        //currentStage = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentStage + 1);
        currentStage++;
        //currentStage = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("next stage: " + currentStage);

    }
}