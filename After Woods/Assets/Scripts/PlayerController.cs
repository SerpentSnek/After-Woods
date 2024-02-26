using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Player : MonoBehaviour
{
    private EnemyController enemyController;
    private RadiationSpec radiationSpec;
    [SerializeField] private int totalHp = 100;
    // How much radiation player can take before it starts to damage the player
    [SerializeField] private int totalRadiation = 10;
    // Rate at which radiation depletes health. In this case, lose 5 health every second
    [SerializeField] private float radiationRate = 1.0f;
    private int currentHp;
    private int currentRadiation;
    private int foodAmount;

    public int TotalHp
    {
        get => totalHp;
        set => totalHp = value;
    }
    public int TotalRadiation
    {
        get => totalRadiation;
        set => totalRadiation = value;
    }
    public float RadiationRate
    {
        get => radiationRate;
        set => radiationRate = value;
    }
    public int CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public int CurrentRadiation
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
        CurrentHp = TotalHp;
        FoodAmount = 0;
        CurrentRadiation = 0;
    }

    void OnEnemyCollide2D(Collider2D other)
    {
        if (other && other.tag == "Enemy")
        {
            CurrentHp -= 5;
        }
    }

    void OnRadiationCollide2D(Collider2D other)
    {
        if (other && other.tag == "Radiation")
        {
            CurrentRadiation += 5;
        }
    }

    void OnFoodCollide2D(Collider2D other)
    {
        if (other && other.tag == "Food")
        {
            FoodAmount += 1;
        }
    }

    void OnBunkerCollide2D(Collider2D other)
    {
        if (other && other.tag == "Bunker")
        {

        }
    }
}
