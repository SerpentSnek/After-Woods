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
            healthBar.GetComponent<HealthBarController>().ChangeValue(ratio);
        }
    }
}