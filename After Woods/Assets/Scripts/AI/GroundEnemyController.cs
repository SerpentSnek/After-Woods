using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class GroundEnemyController : MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float damage;
    private GameObject target;
    private Animator a;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        target = GameManager.Instance.Player;
        a = gameObject.GetComponent<Animator>();
        var beast = GameObject.FindWithTag("Beast");
        Physics2D.IgnoreCollision(beast.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Update()
    {
        var rb = gameObject.GetComponent<Rigidbody2D>();
        a.SetBool("Aggro", rb.velocity.y != 0f);
        if (IsInRange())
        {
            Chase();
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private bool IsInRange()
    {
        float distanceToTarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        return distanceToTarget <= chaseRange;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        if (direction.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        var rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); 
    }
}