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
        this.runTime.text = "Runtime: " + Mathf.Round(Time.time) + " seconds";
        this.hpLeft.text = "Health at checkpoint: "
            + Mathf.Round(GameManager.Instance.GetCheckpointHp());
        this.rpPercentage.text = "Radiation: "
            + Mathf.Round(GameManager.Instance.GetCheckpointRadiation()) + "%";
        this.foodLeft.text = "Food Remaining: "
            + GameManager.Instance.GetCheckpointFood();
        //this.distanceToHome.text = "Distance completed: "
        //    + ((GameManager.Instance.CurrentStage - 1) / 3) + "%";
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