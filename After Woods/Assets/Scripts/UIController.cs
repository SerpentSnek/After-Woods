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
        radiationBar.GetComponent<HealthBarController>().SetMaxValue(playerController.TotalHp);
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        this.foodCounter.text = playerController.FoodAmount.ToString();
        healthBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentHp);
        radiationBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentHp);

        // change timer text using beast reference

    }
}