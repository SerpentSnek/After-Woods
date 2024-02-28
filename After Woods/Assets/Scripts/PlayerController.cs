using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    private EnemyController enemyController;
    // The radiation spec provides info on damage dealt
    private RadiationSpec radiationSpec;
    [SerializeField] private float totalHp = 100;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private float totalRadiation = 50;
    private float currentHp;
    private float currentRadiation;
    private int foodAmount;

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

    void OnAwake()
    {
        //totalHp = 100;
        //totalRadiation = 10;
        enemyController = FindAnyObjectByType<EnemyController>();
        radiationSpec = new RadiationSpec();
        CurrentHp = TotalHp;
        FoodAmount = 0;
        CurrentRadiation = 0;
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
                Debug.Log("null gameObject collision");
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
            // call GameManager to load new scene
            GameManager.Instance.LoadMainMenu();
        }
        else
        {
            Debug.Log("bunker null collider");
        }
    }
}
