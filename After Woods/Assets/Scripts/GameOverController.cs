using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadCheckpoint()
    {
        Debug.Log("Load");
    }
}