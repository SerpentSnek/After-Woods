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
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        healthBar.GetComponent<HealthBarController>().ChangeValue(playerController.CurrentHp / playerController.TotalHp);
        radiationBar.GetComponent<RadiationBarController>().ChangeValue(playerController.CurrentRadiation / playerController.TotalRadiation);
        this.foodCounter.text = playerController.FoodAmount.ToString();
        
        // change timer text using beast reference

    }
}