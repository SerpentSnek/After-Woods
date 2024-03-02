using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PlayerController playerController;
    //private bool isNewGame;
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
        //isNewGame = false;
        playerController = FindObjectOfType<PlayerController>();
        this.ResetPlayerStats();
    }

    public void ResetPlayerStats()
    {
        Instance.playerController.CurrentHp = Instance.playerController.TotalHp;
        Instance.playerController.TotalRadiation = 0;
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
        // SceneManager.LoadSceneAsync("whatever game over scene is named");
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        //this.isNewGame = false;
        //playerController = FindObjectOfType<PlayerController>();
        this.ResetPlayerStats();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(playerController.gameObject);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}