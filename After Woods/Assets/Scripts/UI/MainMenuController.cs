using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // TODO load the start scene
        GameManager.Instance.LoadStartStage();
    }

    public void StartTutorial()
    {
        GameManager.Instance.LoadTutorialStage();
    }
}