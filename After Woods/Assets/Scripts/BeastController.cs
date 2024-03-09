using UnityEngine;

// currently just a copy of flying enemy but with timer logic
public class BeastController : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    [SerializeField] private GameObject target;
    
    void Start()
    {
        // target = GameManager.Instance.Player;
    }

    void Update()
    {
        var timer = GameManager.Instance.Timer;
        if (timer.IsTimeUp)
        {
            Chase();
        }
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position);
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
    }
}
