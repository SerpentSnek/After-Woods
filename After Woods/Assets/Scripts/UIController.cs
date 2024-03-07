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
    private bool firstUpdate = true;

    // TODO add variables for food components

    // Start is called before the first frame update
    void Awake()
    {
        this.foodCounter.text = "Food";
        this.timerText.text = "Timer: ";
    }

    // Update is called once per frame
    void Update()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        if (firstUpdate)
        {
            healthBar.GetComponent<HealthBarController>().SetMaxHealth(playerController.TotalHp);
            radiationBar.GetComponent<RadiationBarController>().SetMaxRadiation(playerController.TotalRadiation);
            firstUpdate = false;
        }
        this.foodCounter.text = playerController.FoodAmount.ToString();
        
        // change timer text using beast reference

    }
}