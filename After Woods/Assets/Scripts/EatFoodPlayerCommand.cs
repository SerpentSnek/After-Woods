using UnityEngine;

public class EatFoodPlayerCommand : MonoBehaviour, IPlayerCommand
{
    [SerializeField] private float hpRestore;

    private void Start()
    {
        hpRestore = 10f;
    }
    public void Execute(GameObject player)
    {
        player.gameObject.GetComponent<PlayerController>().CurrentHp += hpRestore;
    }
}
