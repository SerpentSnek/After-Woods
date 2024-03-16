using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Transform damageIndicator;
    [SerializeField] private TextMeshProUGUI foodCounter;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] 
    private GameObject healthBar;
    [SerializeField] 
    private GameObject radiationBar;
    private bool isTimerPulsing = false;

    public bool IsTimerPulsing { get => isTimerPulsing; set => isTimerPulsing = value; }

    // TODO add variables for food components

    // Start is called before the first frame update
    void Awake()
    {
        this.foodCounter.text = "Food";
        this.timerText.text = "Timer: ";
    }

    void Start()
    {
        var playerController = GameManager.Instance.Player.GetComponent<PlayerLogicController>();
        healthBar.GetComponent<HealthBarController>().SetMaxValue(playerController.TotalHp);
        radiationBar.GetComponent<HealthBarController>().SetMaxValue(playerController.TotalRadiation);
    }

    // Update is called once per frame
    void Update()
    {
        var timeLeft = GameManager.Instance.Timer.CurrentTime;
        // Timer
        timerText.text = Math.Round(timeLeft, 2).ToString();
        if (timeLeft < 5f && timeLeft > 0f && !isTimerPulsing)
        {
            isTimerPulsing = true;
            StartCoroutine("Pulse");
        }

        if (timeLeft == 0f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }

        var playerController = GameManager.Instance.Player.GetComponent<PlayerLogicController>();

        // Food
        this.foodCounter.text = playerController.FoodAmount.ToString();
        // HP
        healthBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentHp);
        // RP
        radiationBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentRadiation);
    }

    private IEnumerator Pulse()
    {
        for (float i = 0.0f; i <= 1.25f; i += 0.1f)
        {
            this.timerText.transform.localScale = new Vector3(
                (Mathf.Lerp(this.timerText.transform.localScale.x, this.timerText.transform.localScale.x + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.y, this.timerText.transform.localScale.y + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.z, this.timerText.transform.localScale.z + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i)))
            );
            yield return new WaitForSeconds(0.04f);
        }

        for (float i = 0.0f; i <= 1.25f; i += 0.1f)
        {
            this.timerText.transform.localScale = new Vector3(
                (Mathf.Lerp(this.timerText.transform.localScale.x, this.timerText.transform.localScale.x - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.y, this.timerText.transform.localScale.y - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.z, this.timerText.transform.localScale.z - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i)))
            );
            yield return new WaitForSeconds(0.04f);
        }
        isTimerPulsing = false;
    }
}