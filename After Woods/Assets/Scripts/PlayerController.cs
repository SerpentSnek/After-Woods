using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    /* Use IPlayerCommand to bind inputs for eating food and baiting food */
    // private IPlayerCommand ...
    private float speed = 5.0f;
    private EnemyController enemyController;
    // The radiation spec provides info on damage dealt
    private RadiationSpec radiationSpec;
    [SerializeField] private float totalHp;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private float totalRadiation;
    private float currentHp;
    private float currentRadiation;
    [SerializeField] private int foodAmount;

    public float TotalHp
    {
        get => totalHp;
        set => totalHp = value;
    }
    public float TotalRadiation
    {
        get => totalRadiation;
        set => totalRadiation = value;
    }
    public float CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public float CurrentRadiation
    {
        get => currentRadiation;
        set => currentRadiation = value;
    }
    public int FoodAmount
    {
        get => foodAmount;
        set => foodAmount = value;
    }

    void Awake()
    {
        // totalHp = 100.0f;
        // totalRadiation = 50.0f;
        enemyController = FindAnyObjectByType<EnemyController>();
        radiationSpec = FindObjectOfType<RadiationSpec>();
        CurrentHp = TotalHp;
        CurrentRadiation = 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                OnEnemyCollide2D(collision);
                break;
            case "Radiation":
                OnRadiationCollide2D(collision);
                break;
            case "Food":
                OnFoodCollide2D(collision);
                break;
            case "Bunker":
                OnBunkerCollide2D(collision);
                break;
            case "Beast":
                OnBeastCollide2D(collision);
                break;
            default:
                // Debug.LogError("null gameObject collision");
                break;
        }
    }

    void OnEnemyCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Enemy")
        {
            CurrentHp -= enemyController.DamageOutput;
        }
        else
        {
            Debug.Log("enemy null collider");
        }
    }

    void OnBeastCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Beast")
        {
            // Instakill
            CurrentHp = 0;
            // Get the game manager to load the game over screen
            GameManager.Instance.LoadGameOverScreen();
        }
    }

    void OnRadiationCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Radiation")
        {
            CurrentRadiation += radiationSpec.RadiationDamage;
        }
        else
        {
            Debug.Log("radiation null collider");
        }
    }

    void OnFoodCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Food")
        {
            this.FoodAmount += 1;
            Destroy(other.gameObject);
        }
        else
        {
            Debug.Log("food null collider");
        }
    }

    void OnBunkerCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Bunker")
        {
            // call GameManager to load new scene (mock main menu used for testing)
            GameManager.Instance.LoadNextStage();
        }
        else
        {
            Debug.Log("bunker null collider");
        }
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.01)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        if (Input.GetAxis("Horizontal") < -0.01)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(-this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }
}
