using UnityEngine;

public class EatFoodPlayerCommand : MonoBehaviour, IInputCommand
{
    [SerializeField] private float hpRestore;

    private void Start()
    {
        hpRestore = 10f;
    }
    public void Execute(GameObject player)
    {
        if (player.gameObject.GetComponent<PlayerController>().FoodAmount > 0)
        {
            player.gameObject.GetComponent<PlayerController>().CurrentHp += hpRestore;
            player.gameObject.GetComponent<PlayerController>().FoodAmount -= 1;
        }


    }
}
