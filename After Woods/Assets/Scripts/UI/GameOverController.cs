using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text runTime;
    [SerializeField]
    private UnityEngine.UI.Text hpLeft;
    [SerializeField]
    private UnityEngine.UI.Text rpPercentage;
    [SerializeField]
    private UnityEngine.UI.Text foodLeft;
    [SerializeField]
    private UnityEngine.UI.Text distanceToHome;

    void Awake()
    {
        this.runTime.text = "Runtime: ";
        this.hpLeft.text = "Remaining Health: ";
        this.rpPercentage.text = "Radiation Percentage Full: ";
        this.foodLeft.text = "Food Remaining: ";
        this.distanceToHome.text = "Distance to Home; ";
    }

    void Update()
    {

    }

    // Go back to main menu and restart the whole game from there.
    public void RestartGame()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void LoadCheckpoint()
    {
        //GameObject manager = GameObject.Find("GameManager");
        //manager.GetComponent<GameManager>().LoadCurrentStage();
        GameManager.Instance.LoadCurrentStage();
        Debug.Log("Load");
    }
}