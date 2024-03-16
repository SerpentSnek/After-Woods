using UnityEngine;

public class FlyingEnemyController: MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float damage;
    private GameObject target;
    private Rigidbody2D rb;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        target = GameManager.Instance.Player;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsInRange())
        {
            Chase();
        }
        else
        {
            rb.velocity = new Vector2(0,0);
        }
    }

    private bool IsInRange()
    {
        float distanceToTarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceToTarget <= chaseRange)
        {
            return true;
        }
        return false;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position);
        // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
        rb.velocity = direction.normalized * chaseSpeed;
    }
}