using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IReset
{
    private static GameManager _instance;
    private GameObject player;
    private Timer timer;
    private int currentStage;
    private PlayerData checkpointInfo;
    private float runtime;
    private bool isRuntimeActive;
    //private GameObject loading;

    class PlayerData
    {
        public float hp, radiation;
        public int food;
    }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject LoadingScreen;

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
    public float Runtime
    {
        get => runtime;
        set => runtime = value;
    }

    public float GetCheckpointHp()
    {
        return checkpointInfo.hp;
    }
    public float GetCheckpointRadiation()
    {
        return checkpointInfo.radiation;
    }
    public float GetCheckpointFood()
    {
        return checkpointInfo.food;
    }

    // https://learn.unity.com/tutorial/implement-data-persistence-between-scenes
    void Awake()
    {
        //LoadingScreen = GameObject.Find("LoadingScreen");
        //if (loading == null)
        //{
        //    loading = Instantiate(LoadingScreen, new Vector2(0, 0), Quaternion.identity, gameObject.transform);
        //}
        //loading.SetActive(false);
        //runtime = 0f;
        //if (currentStage == 0)
        //{
        //    isRuntimeActive = false;
        //}
        //else
        //{
        //    isRuntimeActive = true;
        //}
        isRuntimeActive = true;
        currentStage = SceneManager.GetSceneByName("Stage1v2").buildIndex;
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
        runtime = 0f;
        isRuntimeActive = false;
        this.Reset();
        // for all Load methods just use SceneManager.LoadScene(scene_name)
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void LoadStartStage()
    {
        //DontDestroyOnLoad(gameObject);
        runtime = 0f;
        isRuntimeActive = true;
        if (timer != null)
        {
            timer.Reset();
            timer.IsActive = true;
        }
        //if (player != null)
        //{
        //    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //    player.transform.position = new Vector3(0, 0, -1);
        //}
        //SceneManager.LoadSceneAsync("Stage1v2");
        //https://gamedev.stackexchange.com/questions/153707/how-to-get-current-scene-name
        currentStage = SceneManager.GetSceneByName("Stage1v2").buildIndex;
        LoadLevel(currentStage);
        Debug.Log("stage at start: " + currentStage);
    }

    public void LoadGameOverScreen()
    {
        // Don't destroy the player just yet in case they want to start from a checkpoint.
        // player.GetComponent<PlayerLogicController>().CurrentHp = player.GetComponent<PlayerLogicController>().TotalHp;
        isRuntimeActive = false;
        //DontDestroyOnLoad(player);
        DontDestroyOnLoad(gameObject);
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync("GameOver");
    }

    public void LoadCurrentStage()
    {
        isRuntimeActive = true;
        player.GetComponent<PlayerLogicController>().Reset();
        player.GetComponent<PlayerLogicController>().CurrentHp = checkpointInfo.hp;
        player.GetComponent<PlayerLogicController>().CurrentRadiation = checkpointInfo.radiation;
        player.GetComponent<PlayerLogicController>().FoodAmount = checkpointInfo.food;
        timer.Reset();
        LoadLevel(currentStage);
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

        checkpointInfo.hp = player.GetComponent<PlayerLogicController>().CurrentHp;
        checkpointInfo.radiation = player.GetComponent<PlayerLogicController>().CurrentRadiation;
        checkpointInfo.food = player.GetComponent<PlayerLogicController>().FoodAmount;
        LoadLevel(currentStage + 1);

        currentStage++;
        Debug.Log("next stage: " + currentStage);
    }
    private void LoadLevel(int sceneInd)
    {
        StartCoroutine(LoadLevelAsync(sceneInd));
    }
    IEnumerator LoadLevelAsync(int sceneId)
    {
        AsyncOperation loadingDone = SceneManager.LoadSceneAsync(sceneId);
        // whatever loading scene is called in either scene or gameObject form
        //loading.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
        //loading.SetActive(true);
        Debug.Log("loadingdone: " + loadingDone);
        if (loadingDone == null)
        {
            currentStage = 2;
            SceneManager.LoadScene(2);
        }
        else
        {
            while (!loadingDone.isDone)
            {
                yield return null;
            }
        }
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.transform.position = new Vector3(0, 0, -1);
        isRuntimeActive = true;
        //loading.SetActive(false);
    }
    void Update()
    {
        if (isRuntimeActive == true)
        {
            runtime += Time.deltaTime;
            Debug.Log("runtime active: " + runtime);
        }
        else
        {
            Debug.Log("inactive runtime:" + runtime);
        }
    }
}