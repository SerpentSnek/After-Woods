using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadCheckpoint()
    {
        Debug.Log("Load");
    }
}