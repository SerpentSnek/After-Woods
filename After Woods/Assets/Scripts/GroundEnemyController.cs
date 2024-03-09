using UnityEngine;

public class GroundEnemyController: MonoBehaviour
{
    public GameObject target;
    private float chaseRange= 5f; 
    private float chaseSpeed = 2f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Check_Chase();
    }

    void Check_Chase()
    {
        float distanceTotarget = Vector2.Distance(this.gameObject.transform.position, target.transform.position);
        if (distanceTotarget <= chaseRange)
        {
            Chasetarget();
        }
        else
        {
            rb.velocity  = new Vector2(0, rb.velocity.y);
        }
    }

    void Chasetarget()
    {
        Vector2 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }
}