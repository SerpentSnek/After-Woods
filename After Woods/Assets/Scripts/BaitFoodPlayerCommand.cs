using UnityEngine;

public class BaitFoodPlayerCommand : MonoBehaviour, IInputCommand
{
    private Timer timer;
    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
    }
    public void Execute(GameObject player)
    {
        if (player.gameObject.GetComponent<PlayerController>().FoodAmount > 0)
        {
            player.gameObject.GetComponent<PlayerController>().FoodAmount -= 1;
            timer.AddTime();
        }
    }
}
