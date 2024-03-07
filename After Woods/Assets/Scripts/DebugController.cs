using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject radiationBar;
    [SerializeField] private float ratio;

    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            healthBar.GetComponent<HealthBarController>().SetHealth(ratio);
        }

        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();
        if (Input.GetButtonDown("Fire1"))
        {

            playerController.CurrentHp = playerController.CurrentHp - .1f * playerController.TotalHp;

        }
    }
}