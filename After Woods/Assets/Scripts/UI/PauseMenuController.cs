using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] 
    private GameObject pauseMenu;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0.0f;
        GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = false;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = true;
        AudioListener.pause = false;
    }
}