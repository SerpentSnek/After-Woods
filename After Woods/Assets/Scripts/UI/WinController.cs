using UnityEngine;
using TMPro;
using System;

public class WinController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI runTime;

    void Awake()
    {
        this.runTime.text = "Runtime: " + Math.Round(Time.time, 2) + " seconds";
    }

    public void StartGame()
    {
        // TODO load the start scene
        GameManager.Instance.LoadStartStage();
    }

}