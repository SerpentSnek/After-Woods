using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerLogicController : MonoBehaviour, IReset
{
    [SerializeField] private int foodAmount;
    [SerializeField] private float totalHp;
    private float currentHp;
    private bool isDead;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private float totalRadiation;
    private float currentRadiation;
    [SerializeField] private float radiationDamage;
    private bool isDamagedByRadiation;
    //private bool isFinished;
    [SerializeField] private float baitTime;
    [SerializeField] private float foodHealingValue;
    [SerializeField] private LayerMask radiationLayer;
    [SerializeField] private bool invulnerable;
    private bool dead = false;
    [SerializeField] private float timeBonusWindow;
    [SerializeField] private float bonusTimeMultiplier;
    [SerializeField] private GameObject floatingTextPrefab;
    private float currentTime;
    private int baitPressCount;
    private float firstBaitPressTime;

    private Animator a;
    private PlayerSoundManager sm;

    public bool baitEnabled = true;


    [SerializeField] private InputActionReference bait, eat;

    public int FoodAmount
    {
        get => foodAmount; set => foodAmount = value;
    }
    public float TotalHp
    {
        get => totalHp;
    }
    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = Mathf.Clamp(value, 0.0f, totalHp);
    }
    public float TotalRadiation
    {
        get => totalRadiation;
    }
    // public float CurrentRadiation { get => currentRadiation; }
    // for debugging
    public float CurrentRadiation
    {
        get => currentRadiation; set => currentRadiation = value;
    }
    public bool IsDead
    {
        get => dead;
    }

    // public float RadiationDamage { get; private set; }
    // public bool IsDamagedByRadiation { get; private set; }

    public void Reset()
    {
        currentHp = totalHp;
        currentRadiation = 0;
        foodAmount = 0;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        gameObject.transform.position = new Vector3(0, 0, -1);
        dead = false;
        a.SetBool("Dead", false);
        Time.timeScale = 1;
        GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = true;
    }

    void OnEnable()
    {
        bait.action.performed += OnBaitPress;
        eat.action.performed += OnEatPress;
    }

    void OnDisable()
    {
        bait.action.performed -= OnBaitPress;
        eat.action.performed -= OnEatPress;
    }

    void Start()
    {
        baitPressCount = 0;
        a = gameObject.GetComponent<Animator>();
        sm = gameObject.GetComponent<PlayerSoundManager>();
        Reset();
    }

    void Update()
    {
        if (isDamagedByRadiation)
        // if (Physics2D.OverlapCircle(gameObject.transform.position, 0.5f, radiationLayer))
        {
            currentRadiation += radiationDamage * Time.deltaTime;
            if (currentRadiation >= totalRadiation)
            {
                DieFromRadiation();
            }
        }

        if (currentRadiation > 0)
        {
            switch (GetRadiationStatus())
            {
                case 0:
                    break;
                case 1:
                    currentHp -= (1.5f * Time.deltaTime);
                    break;
                case 2:
                    currentHp -= (3f * Time.deltaTime);
                    break;
                case 3:
                    currentHp -= (6f * Time.deltaTime);
                    break;
                case 4:
                    currentHp -= (50f * Time.deltaTime);
                    break;
            }
        }

        if (currentHp <= 0 && !dead)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            switch (collision.gameObject.tag)
            {
                case "Beast":
                    OnBeastCollideEnter2D();
                    break;
                case "Enemy":
                    OnEnemyCollideEnter2D(collision);
                    break;
            }
        }
        else
        {
            Debug.LogError("null gameObject collision");
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log(collider.gameObject.tag);
        if (collider.gameObject != null)
        {

            switch (collider.gameObject.tag)
            {
                case "Bunker":
                    OnBunkerTriggerEnter2D(collider);
                    break;
                // case "Enemy":
                //     OnEnemyTriggerEnter2D(collider);
                //     break;
                case "Food":
                    OnFoodTriggerEnter2D(collider);
                    break;
                case "Radiation":
                    OnRadiationTriggerEnter2D();
                    break;
            }
        }
        else
        {
            Debug.LogError("null gameObject collider");
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject != null)
        {
            switch (collider.gameObject.tag)
            {
                case "Radiation":
                    OnRadiationTriggerExit2D();
                    break;
            }
        }
        else
        {
            Debug.LogError("null gameObject collider");
        }
    }


    private void OnBeastCollideEnter2D()
    {
        currentHp = 0;
        // Debug.Log("beast hit");
    }

    private void OnEnemyCollideEnter2D(Collision2D collision)
    {
        var enemyController = collision.gameObject.GetComponent<IDamage>();
        if (enemyController != null)
        {
            currentHp -= enemyController.Damage();
            ShowDamage(enemyController.Damage() * -1);
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1, 0, 0);
            StartCoroutine("ReturnColor");
            // Debug.Log(currentHp);
        }
        else
        {
            Debug.LogError("missing enemy controller");
        }
    }

    private void OnBunkerTriggerEnter2D(Collider2D collider)
    {
        GameManager.Instance.LoadNextStage();
        //isFinished = true;
        // Debug.Log("entered bunker");
    }

    // private void OnEnemyTriggerEnter2D(Collider2D collider)
    // {
    //     var enemyController = collider.gameObject.GetComponent<IDamage>();
    //     if (enemyController != null)
    //     {
    //         currentHp -= enemyController.Damage();
    //         // Debug.Log(currentHp);
    //     }
    //     else
    //     {
    //         Debug.LogError("missing enemy controller");
    //     }
    // }

    private void OnFoodTriggerEnter2D(Collider2D collider)
    {
        foodAmount += 1;
        sm.PlaySound("pickup");
        Destroy(collider.gameObject);
    }

    private void OnRadiationTriggerEnter2D()
    {
        isDamagedByRadiation = true;
        // Debug.Log("hi");
    }

    private void OnRadiationTriggerExit2D()
    {
        isDamagedByRadiation = false;
    }

    private void Die()
    {
        if (!invulnerable)
        {
            // Debug.Log("called");
            // Get the game manager to load the game over screen
            dead = true;
            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1, 1, 1);
            GameManager.Instance.Player.GetComponent<PlayerMovementV2>().enabled = false;
            var bgm = GameObject.FindWithTag("StageBGM");
            if (bgm)
            {
                bgm.SetActive(false);
            }
            
            sm.StopAllSounds();
            sm.PlaySound("death");
            Time.timeScale = 0.02f;
            a.SetBool("Dead", true);
            a.Play("Player_Death");
            // GameManager.Instance.LoadGameOverScreen();
        }

        // Debug.Log("died");
    }

    private void DieFromRadiation()
    {
        Die();
        // Debug.Log("died from radiation");
    }

    private void OnBaitPress(InputAction.CallbackContext obj)
    {
        if (foodAmount > 0 && !dead && baitEnabled)
        {
            sm.PlaySound("baiting");
            currentTime = Time.time;
            if (baitPressCount == 0)
            {
                firstBaitPressTime = Time.time;
            }
            baitPressCount++;
            foodAmount -= 1;
            if (currentTime - firstBaitPressTime > timeBonusWindow)
            {
                baitPressCount = 0;
                GameManager.Instance.Timer.AddTime(baitTime);
            }
            else
            {
                GameManager.Instance.Timer.AddTime(Mathf.Pow(baitTime * baitPressCount, bonusTimeMultiplier));
            }
        }
    }

    private void OnEatPress(InputAction.CallbackContext obj)
    {
        if (foodAmount > 0 && currentHp < totalHp && !dead)
        {
            sm.PlaySound("eating");
            ShowDamage(foodHealingValue);
            CurrentHp += foodHealingValue;

            foodAmount -= 1;
        }
    }

    private int GetRadiationStatus()
    {
        if (currentRadiation > 0 && currentRadiation <= 5)
        {
            return 0;
        }
        else if (currentRadiation > 5 && currentRadiation <= 33)
        {
            return 1;
        }
        else if (currentRadiation > 33 && currentRadiation <= 66)
        {
            return 2;
        }
        else if (currentRadiation > 66 && currentRadiation <= 99)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    private void ShowDamage(float damage)
    {
        var floatingText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        floatingText.transform.Translate(new Vector3(0.4f, 0, 0));
        floatingText.transform.SetParent(gameObject.transform);
        var color = new Color(0, 1, 0);
        floatingText.GetComponentInChildren<TextMeshPro>().text = damage.ToString();
        if (damage < 0)
        {
            color = new Color(1, 0, 0);
        }
        floatingText.GetComponentInChildren<TextMeshPro>().color = color;
        Destroy(floatingText, 1f);
        sm.PlaySound("damage");
    }

    IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(0.2f);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.color = new Color(1, 1, 1);
    }
}
