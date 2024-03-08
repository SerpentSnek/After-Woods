using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int foodAmount;
    [SerializeField] private float totalHp;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private float totalRadiation;
    [SerializeField] private float radiationDamage;
    private bool isDamagedByRadiation;
    private float currentHp;
    private float currentRadiation;

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
    public float RadiationDamage
    {
        get => radiationDamage;
        set => radiationDamage = value;
    }
    public bool IsDamagedByRadiation
    {
        get => isDamagedByRadiation;
        set => isDamagedByRadiation = value;
    }

    void Awake()
    {
        TotalHp = 100;
        TotalRadiation = 50;
        CurrentHp = TotalHp;
        CurrentRadiation = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("check for collision");
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                OnEnemyCollide2D(collision);
                break;
            case "Radiation":
                IsDamagedByRadiation = true;
                break;
            case "Food":
                OnFoodCollide2D(collision);
                break;
            case "Bunker":
                Debug.Log("landed on bunker");
                OnBunkerCollide2D(collision);
                break;
            case "Beast":
                OnBeastCollide2D(collision);
                break;
            default:
                Debug.LogError("null gameObject collision");
                break;
        }
    }

    void OnEnemyCollide2D(Collision2D other)
    {
        if (other.gameObject != null && other.gameObject.tag == "Enemy")
        {
            CurrentHp -= other.gameObject.GetComponent<EnemyController>().DamageOutput;
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

    void OnRadiationCollide2D()
    {
        //if (other.gameObject != null && other.gameObject.tag == "Radiation")
        //{
        //    CurrentRadiation += RadiationDamage;
        //}
        //else
        //{
        //    Debug.Log("radiation null collider");
        //}
        CurrentRadiation += RadiationDamage;
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

    private void Update()
    {
        if (IsDamagedByRadiation)
        {
            OnRadiationCollide2D();
        }
    }
}
