using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class GroundEnemyController : MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private float patrolEdgeDistance;
    [SerializeField] private float damage;
    private GameObject target;
    private Animator a;
    private Rigidbody2D rb;
    private MobSoundManager sm;

    private int dir;
    private float initialXPosition;
    private bool isUpdatedInitialXPosition;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        target = GameManager.Instance.Player;
        a = gameObject.GetComponent<Animator>();
        sm = gameObject.GetComponent<MobSoundManager>();

        var beast = GameObject.FindWithTag("Beast");
        Physics2D.IgnoreCollision(beast.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        initialXPosition = gameObject.transform.position.x;
        isUpdatedInitialXPosition = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        dir = UnityEngine.Random.Range(0, 2);

        if (dir == 0)
        {
            dir = -1;
        }
    }

    void Update()
    {
        if (IsInRange())
        {
            a.SetBool("Aggro", true);
            sm.PlayAggroSound();
            isUpdatedInitialXPosition = false;
            Chase();
        }
        else
        {
            a.SetBool("Aggro", false);
            sm.StopAggroSound();
            Patrol();
        }
        if (rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y); 
    }

    private void Patrol()
    {
        if (!isUpdatedInitialXPosition)
        {
            initialXPosition = gameObject.transform.position.x;
            isUpdatedInitialXPosition = true;
        }

        if (Math.Abs(initialXPosition - transform.position.x) > patrolEdgeDistance)
        {
            var newPosition = transform.position;
            newPosition.x = initialXPosition + (patrolEdgeDistance - 0.01f) * dir;
            transform.position = newPosition;

            dir *= -1;
        } else if (rb.velocity.x == 0)
        {
            dir *= -1;
        }

        rb.velocity = new Vector2(patrolSpeed * dir, rb.velocity.y);
    }
}