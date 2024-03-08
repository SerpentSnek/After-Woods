using UnityEngine;

public class EatFoodPlayerCommand : MonoBehaviour, IInputCommand
{
    [SerializeField] private float hpRestore;

    public float HpRestore
    {
        get => hpRestore;
        set => hpRestore = value;
    }

    private void Start()
    {
        HpRestore = 10f;
    }
    public void Execute(GameObject player)
    {
        if (player.gameObject.GetComponent<PlayerController>().FoodAmount > 0)
        {
            player.gameObject.GetComponent<PlayerController>().CurrentHp += HpRestore;
            player.gameObject.GetComponent<PlayerController>().FoodAmount -= 1;
        }
    }
}
