using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Transform damageIndicator;
    [SerializeField]
    private UnityEngine.UI.Text foodCounter;
    [SerializeField]
    private UnityEngine.UI.Text timerText;
    [SerializeField] 
    private GameObject healthBar;
    [SerializeField] 
    private GameObject radiationBar;

    private bool coroutineFlag = true;
    private bool firstUpdate = true;

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
        // Timer
        timerText.text = GameManager.Instance.Timer.CurrentTime.ToString();


        var playerController = GameManager.Instance.Player.GetComponent<PlayerLogicController>();

        // Food
        this.foodCounter.text = playerController.FoodAmount.ToString();
        // HP
        healthBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentHp);
        // RP
        radiationBar.GetComponent<HealthBarController>().UpdateValue(playerController.CurrentRadiation);

        if (coroutineFlag)
        {
            StartCoroutine("StartPulsing");
        }

    }

    private IEnumerator StartPulsing()
    {
        coroutineFlag = false;
        
        for(float i = 0.0f; i <= 1.0f; i += 0.1f)
        {
            this.timerText.transform.localScale = new Vector3(
                (Mathf.Lerp(this.timerText.transform.localScale.x, this.timerText.transform.localScale.x + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.y, this.timerText.transform.localScale.y + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.z, this.timerText.transform.localScale.z + 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        for(float i = 0.0f; i <= 1.0f; i += 0.1f)
        {
            this.timerText.transform.localScale = new Vector3(
                (Mathf.Lerp(this.timerText.transform.localScale.x, this.timerText.transform.localScale.x - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.y, this.timerText.transform.localScale.y - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i))),
                (Mathf.Lerp(this.timerText.transform.localScale.z, this.timerText.transform.localScale.z - 0.025f, Mathf.SmoothStep(0.0f, 1.0f, i)))
            );
            yield return new WaitForSeconds(0.015f);
        }

        coroutineFlag = true;
    }
}