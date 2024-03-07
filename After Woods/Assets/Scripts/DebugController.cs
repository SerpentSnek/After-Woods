using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject radiationBar;
    private bool firstUpdate = true;

    void Update()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        if (firstUpdate)
        {
            healthBar.GetComponent<HealthBarController>().SetMaxHealth(playerController.TotalHp);
            radiationBar.GetComponent<RadiationBarController>().SetMaxRadiation(playerController.TotalRadiation);
            firstUpdate = false;
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            healthBar.GetComponent<HealthBarController>().UpdateHealth(playerController.CurrentHp);
        }

        if (Input.GetButtonDown("Fire1"))
        {

            playerController.CurrentHp = playerController.CurrentHp - .1f * playerController.TotalHp;

        }
    }
}