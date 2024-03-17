using UnityEngine;

public class BaitFoodCommand : ScriptableObject, IInputCommand
{
    public void Execute(GameObject gameObject)
    {
        if (gameObject.GetComponent<PlayerLogicController>().FoodAmount > 0)
        {
            gameObject.GetComponent<PlayerLogicController>().FoodAmount -= 1;
            GameManager.Instance.Timer.AddTime(1);
        }
    }
}
