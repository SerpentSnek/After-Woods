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
    private GameObject healthBar;
    [SerializeField] 
    private GameObject radiationBar;

    // Start is called before the first frame update
    void Awake()
    {
        this.foodCounter.text = "Food";
        this.timerText.text = "Timer: ";
    }

    void Start()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        healthBar.GetComponent<HealthBarController>().SetMaxValue(playerController.TotalHp);
        radiationBar.GetComponent<HealthBarController>().SetMaxValue(playerController.TotalRadiation);
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        timerText.text = GameManager.Instance.Timer.CurrentTime.ToString();

        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();

        // Food
        this.foodCounter.text = playerController.FoodAmount.ToString();
        // HP
        healthBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentHp);
        // RP
        radiationBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentRadiation);
        // Debug.LogWarning("radiation bar is broken");

    }
}