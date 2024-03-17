using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerLogicController : MonoBehaviour, IReset
{
    [SerializeField] private int foodAmount;
    [SerializeField] private float totalHp;
    private float currentHp;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private float totalRadiation;
    private float currentRadiation;
    [SerializeField] private float radiationDamage;
    private bool isDamagedByRadiation;
    //private bool isFinished;
    [SerializeField] private float baitTime;
    [SerializeField] private float foodHealingValue;
    [SerializeField] private LayerMask radiationLayer;


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
    //public bool IsFinished
    //{
    //    get => isFinished;
    //    set => isFinished = value;
    //}

    // public float RadiationDamage { get; private set; }
    // public bool IsDamagedByRadiation { get; private set; }

    public void Reset()
    {
        currentHp = totalHp;
        currentRadiation = 0;
        foodAmount = 0;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        gameObject.transform.position = Vector2.zero;
        //isFinished = false;
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

        if (currentHp <= 0)
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
        Debug.Log(collider.gameObject.tag);
        if (collider.gameObject != null)
        {

            switch (collider.gameObject.tag)
            {
                case "Bunker":
                    OnBunkerTriggerEnter2D(collider);
                    break;
                case "Enemy":
                    OnEnemyTriggerEnter2D(collider);
                    break;
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
            Debug.Log(currentHp);
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
        Debug.Log("entered bunker");
    }

    private void OnEnemyTriggerEnter2D(Collider2D collider)
    {
        var enemyController = collider.gameObject.GetComponent<IDamage>();
        if (enemyController != null)
        {
            currentHp -= enemyController.Damage();
            Debug.Log(currentHp);
        }
        else
        {
            Debug.LogError("missing enemy controller");
        }
    }

    private void OnFoodTriggerEnter2D(Collider2D collider)
    {
        foodAmount += 1;
        Destroy(collider.gameObject);
    }

    private void OnRadiationTriggerEnter2D()
    {
        isDamagedByRadiation = true;
        Debug.Log("hi");
    }

    private void OnRadiationTriggerExit2D()
    {
        isDamagedByRadiation = false;
    }

    private void Die()
    {
        // Get the game manager to load the game over screen
        GameManager.Instance.LoadGameOverScreen();
        Debug.Log("died");
    }

    private void DieFromRadiation()
    {
        Die();
        Debug.Log("died from radiation");
    }

    private void OnBaitPress(InputAction.CallbackContext obj)
    {
        if (foodAmount > 0)
        {
            foodAmount -= 1;
            GameManager.Instance.Timer.AddTime(baitTime);
        }
    }

    private void OnEatPress(InputAction.CallbackContext obj)
    {
        if (foodAmount > 0 && currentHp < totalHp)
        {
            CurrentHp += foodHealingValue;
            foodAmount -= 1;
        }
    }
}
