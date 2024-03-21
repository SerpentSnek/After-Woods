using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] 
    private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0.0f;
        GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = false;
        AudioListener.pause = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
        GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = true;
        AudioListener.pause = false;
    }
}