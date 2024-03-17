using UnityEngine;

public class GroundEnemyController : MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float damage;
    private GameObject target;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        target = GameManager.Instance.Player;
    }

    void Update()
    {
        if (IsInRange())
        {
            Chase();
        }
        else
        {
            var rb = gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            else
            {
                Debug.LogError("null rigidbody on enemy");
            }

        }
    }

    private bool IsInRange()
    {
        float distanceToTarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceToTarget <= chaseRange)
        {
            this.gameObject.GetComponent<Animator>().Play("GroundAggro");
            return true;
        }
        this.gameObject.GetComponent<Animator>().Play("GroundIdle");
        return false;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
            if (direction.x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (direction.x < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
        else
        {
            Debug.LogError("null rigidbody on enemy");
        }
    }
}