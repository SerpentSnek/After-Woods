using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;
    [SerializeField]
    private GameObject radiationBar;
    [SerializeField]
    private bool active;

    void Update()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerController>();

        if (Input.GetButtonDown("Fire1") && active)
        {
            playerController.CurrentHp = playerController.CurrentHp - .1f * playerController.TotalHp;
            playerController.CurrentRadiation = playerController.CurrentRadiation + .1f * playerController.TotalRadiation;
        }

        if (Input.GetButtonDown("Fire2") && active)
        {
            playerController.CurrentHp = playerController.CurrentHp + .1f * playerController.TotalHp;
            playerController.CurrentRadiation = playerController.CurrentRadiation - .1f * playerController.TotalRadiation;
        }

        // if (Input.GetButtonDown("Fire1"))
        // {
        //     playerController.CurrentRadiation = playerController.CurrentRadiation - .1f * playerController.TotalRadiation;
        //     Debug.Log(playerController.CurrentRadiation);
        // }

        // if (Input.GetButtonDown("Fire2"))
        // {
        //     playerController.CurrentRadiation = playerController.CurrentRadiation + .1f * playerController.TotalRadiation;
        //     Debug.Log(playerController.TotalRadiation);
        //     Debug.Log(playerController.CurrentRadiation);
        // }
    }
}