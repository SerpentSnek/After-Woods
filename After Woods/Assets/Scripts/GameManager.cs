using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, Reset
{
    private static GameManager _instance;
    private GameObject _player;
    public Timer TimerObject;
    protected GameObject Player
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("null player");
            }

            return _player;
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
        else if (_instance == null)
        {
            _instance = this;
            _player = GameObject.FindWithTag("Player");
            DontDestroyOnLoad(gameObject);
            this.LoadStartScreen();
        }
        if (TimerObject == null)
        {
            TimerObject = Instance.GetComponent<Timer>();
        }
        if (Player != null)
        {
            this.ResetStats();
        }
    }

    public void ResetStats() // have this with a Reset interface instead (done)
    {
        if (Player != null && TimerObject != null)
        {
            Instance.Player.GetComponent<PlayerController>().CurrentHp = Instance.Player.GetComponent<PlayerController>().TotalHp;
            Instance.Player.GetComponent<PlayerController>().TotalRadiation = 0;
            TimerObject.ResetStats();
        }
        else
        {
            return;
        }
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