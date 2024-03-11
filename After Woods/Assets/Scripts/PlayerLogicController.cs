using UnityEngine;

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

    private IInputCommand fire1;
    private IInputCommand fire2;

    public int FoodAmount { get => foodAmount; set => foodAmount = value; }
    public float TotalHp { get => totalHp; }
    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = Mathf.Clamp(value, 0.0f, totalHp);
    }
    public float TotalRadiation { get => totalRadiation; }
    // public float CurrentRadiation { get => currentRadiation; }
    // for debugging
    public float CurrentRadiation { get => currentRadiation; set => currentRadiation = value; }
    // public float RadiationDamage { get; private set; }
    // public bool IsDamagedByRadiation { get; private set; }

    public void Reset()
    {
        currentHp = totalHp;
        currentRadiation = 0;
        foodAmount = 0;
    }

    void Start()
    {
        Reset();
        this.fire1 = ScriptableObject.CreateInstance<BaitFoodCommand>();
        this.fire2 = ScriptableObject.CreateInstance<EatFoodCommand>();
    }

    void Update()
    {
        if (isDamagedByRadiation)
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

        if (Input.GetButtonDown("Fire1"))
        {
            this.fire1.Execute(gameObject);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            this.fire2.Execute(gameObject);
        }

        // if (Input.GetAxis("Horizontal") > 0.01)
        // {
        //     var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        //     if (rigidBody != null)
        //     {
        //         rigidBody.velocity = new Vector2(5f, rigidBody.velocity.y);
        //         gameObject.GetComponent<SpriteRenderer>().flipX = false;
        //     }
        // }
        // if (Input.GetAxis("Horizontal") < -0.01)
        // {
        //     var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        //     if (rigidBody != null)
        //     {
        //         rigidBody.velocity = new Vector2(-5f, rigidBody.velocity.y);
        //         gameObject.GetComponent<SpriteRenderer>().flipX = true;
        //     }
        // }
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
        if (collider.gameObject != null)
        {
            switch (collider.gameObject.tag)
            {
                case "Bunker":
                    OnBunkerTriggerEnter2D(collider);
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
        // GameManager.Instance.LoadNextStage();
        Debug.Log("entered bunker");
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
        // GameManager.Instance.LoadGameOverScreen();
        // Debug.Log("died");
    }

    private void DieFromRadiation()
    {
        Die();
        Debug.Log("died from radiation");
    }
}
