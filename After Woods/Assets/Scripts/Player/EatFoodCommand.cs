using UnityEngine;

public class EatFoodCommand : ScriptableObject, IInputCommand
{
    [SerializeField] private float hpRestored = 5f;

    public void Execute(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerLogicController>().FoodAmount > 0)
        {
            gameObject.GetComponent<PlayerLogicController>().CurrentHp += hpRestored;
            gameObject.GetComponent<PlayerLogicController>().FoodAmount -= 1;
        }
    }
}
