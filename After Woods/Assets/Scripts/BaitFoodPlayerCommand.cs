using UnityEngine;

public class BaitFoodPlayerCommand : MonoBehaviour, IPlayerCommand
{
    private Timer timer;
    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
    }
    public void Execute(GameObject player)
    {
        player.gameObject.GetComponent<PlayerController>().FoodAmount -= 1;
        timer.AddTime();
    }
}
