using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text foodCounter;
    [SerializeField]
    private UnityEngine.UI.Text timerText;
    [SerializeField]
    private float timer;
    [SerializeField] 
    private GameObject healthBar;
    [SerializeField] 
    private GameObject radiationBar;

    // TODO add variables for food components

    // Start is called before the first frame update
    void Start()
    {
        this.foodCounter.text = "Food";
        this.timerText.text = "Timer: ";
    }

    // Update is called once per frame
    void Update()
    {
        // TODO change this line to include calculation for total food
        this.foodCounter.text = "x";

        timer -= Time.deltaTime;
        this.timerText.text += timer;

        if (timer <= 0.0f)
        {
            // TODO add what happens when timer ends
        }
    }
}