using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    private string musicTrack = "MainMenuOST";

    public void StartGame()
    {
        // TODO load the start scene
        GameManager.Instance.LoadStartStage();
    }

    public void StartTutorial()
    {
        // TODO load the tutorial scene
    }

    void Update()
    {
        FindObjectOfType<SoundManager>().PlayMusicTrack(this.musicTrack);
    }
}