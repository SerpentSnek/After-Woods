using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text foodCounter;

    // Start is called before the first frame update
    void Start()
    {
        this.foodCounter.text = "Food";
    }

    // Update is called once per frame
    void Update()
    {
        this.foodCounter.text = "x";
    }
}