using UnityEngine;

public class EatFoodCommand : ScriptableObject, IInputCommand
{
    [SerializeField] private float hpRestored = 5f;

    public void Execute(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerController>().FoodAmount > 0)
        {
            gameObject.GetComponent<PlayerController>().CurrentHp += HpRestored;
            gameObject.GetComponent<PlayerController>().FoodAmount -= 1;
        }
    }
}
