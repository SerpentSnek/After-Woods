using UnityEngine;

// currently just a copy of flying enemy but with timer logic
public class BeastController : MonoBehaviour
{
    [SerializeField] private float chaseSpeed;
    private GameObject target;
    private Rigidbody2D rb;

    void Start()
    {
        target = GameManager.Instance.Player;
        rb = gameObject.GetComponent<Rigidbody2D>();
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
        // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
        rb.velocity = direction.normalized * chaseSpeed;
    }
}
