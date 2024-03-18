using UnityEngine;

public class FlyingEnemyController : MonoBehaviour, IDamage
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
        var beast = GameObject.FindWithTag("Beast");
        Physics2D.IgnoreCollision(beast.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Update()
    {
        if (IsInRange())
        {
            Chase();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private bool IsInRange()
    {
        float distanceToTarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceToTarget <= chaseRange)
        {
            this.gameObject.GetComponent<Animator>().Play("Flying_Aggro");
            return true;
        }
        this.gameObject.GetComponent<Animator>().Play("Flying_Idle");
        return false;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position);
        // transform.position = Vector2.MoveTowards(transform.position, target.transform.position, chaseSpeed * Time.deltaTime);
        rb.velocity = direction.normalized * chaseSpeed;
        if (direction.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}