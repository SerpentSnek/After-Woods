using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
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
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            this.LoadStartScreen();
        }
    }

    public void LoadMainMenu()
    {
        DontDestroyOnLoad(gameObject);
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        SceneManager.LoadSceneAsync("MainMenuPlaceholder");
    }

    public void LoadStartScreen()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadGameOverScreen()
    {
        DontDestroyOnLoad(gameObject);
    }

    // see https://www.youtube.com/watch?v=HBEStd96UzI
    public void LoadNextStage()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}