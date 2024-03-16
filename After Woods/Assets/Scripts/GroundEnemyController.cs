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
            this.gameObject.GetComponent<Animator>().Play("Ground_Aggro");
            return true;
        }
        this.gameObject.GetComponent<Animator>().Play("Ground_Idle");
        return false;
    }

    private void Chase()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        var rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
        }
        else
        {
            Debug.LogError("null rigidbody on enemy");
        }
    }
}