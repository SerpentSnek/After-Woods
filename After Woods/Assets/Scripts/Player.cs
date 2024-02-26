[RequireComponent(typeof(BoxCollider2D))]

public class Player
{
    private EnemySpec enemySpec;
    private RadiationSpec radiationSpec;
    [SerializedField] private int totalHp = 100;
    // How much radiation player can take before it starts to damage the player
    [SerializedField] private int totalRadiation = 10;
    // Rate at which radiation depletes health. In this case, lose 5 health every second
    [SerializedField] private float radiationRate = 1.0f;
    private int currentHp;
    private int currentRadiation;
    private int foodAmount;

    public global::System.Int32 TotalHp
    {
        get => totalHp;
        set => totalHp = value;
    }
    public global::System.Int32 TotalRadiation
    {
        get => totalRadiation;
        set => totalRadiation = value;
    }
    public global::System.Single RadiationRate
    {
        get => radiationRate;
        set => radiationRate = value;
    }
    public global::System.Int32 CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public global::System.Int32 CurrentRadiation
    {
        get => currentRadiation;
        set => currentRadiation = value;
    }
    public global::System.Int32 FoodAmount
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
        if (other && other.tag == 'Enemy')
        {
            CurrentHp -= 5;
        }
    }

    void OnRadiationCollide2D(Collider2D other)
    {
        if (other && other.tag == 'Radiation')
        {
            CurrentRadiation += 5;
        }
    }

    void OnFoodCollide2D(Collider2D other)
    {
        if (other && other.tag == 'Food')
        {
            FoodAmount += 1;
        }
    }

    void OnBunkerCollide2D(Collider2D other)
    {
        if (other && other.tag == 'Bunker')
        {

        }
    }
}
