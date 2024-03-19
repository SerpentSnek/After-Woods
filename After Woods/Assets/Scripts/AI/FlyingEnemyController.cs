using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyController: MonoBehaviour, IDamage
{
    [SerializeField] private float chaseRange;
    [SerializeField] private float speed;
    [SerializeField] private float nextWaypointDistance;
    [SerializeField] private float damage;
    [SerializeField] private float basicChaseThreshold;
    [SerializeField] private float basicChaseSpeed;
    [SerializeField] private float patrolRadius;
    [SerializeField] private float patrolSpeed;
    private GameObject target;
    private Vector3 patrolTarget;
    private Vector3 initialPatrolPosition;
    private bool isUpdatedInitialPatrolPosition;
    private float patrolTimer = 2f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Animator a;

    private Seeker seeker;
    private Rigidbody2D rb;

    public float Damage()
    {
        return damage;
    }

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        a = gameObject.GetComponent<Animator>();
        target = GameManager.Instance.Player;
        patrolTarget = GenerateRandomPointInRadius();
        initialPatrolPosition = transform.position;
        isUpdatedInitialPatrolPosition = true;
        InvokeRepeating("UpdatePath", 0f, 0.25f);
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.transform.position, OnpathComplete);
    }

    void OnpathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void Update()
    {
        // 3 cases
        // 1: player out of range: patrol
        // 2: player in range, outside of basic chase threshold
        // 3: in range, in basic chase threshold
        if (IsInRange())
        {
            if (UseBasicChase())
            {
                var direction = target.transform.position - transform.position;
                rb.velocity = direction.normalized * basicChaseSpeed;
            }
            else
            {
                pathfollow();
            }
            isUpdatedInitialPatrolPosition = false;
            a.SetBool("Aggro", true);
        }
        else
        {
            a.SetBool("Aggro", false);
            Patrol();
        }

        // Vector2 direction = (target.position - transform.position);
        // if (IsInRange() && Vector2.Distance(rb.position, target.position) > 1.75f)
        // {
        //     pathfollow();
        // }
        // else if (Vector2.Distance(rb.position, target.position) <= 1.75f)
        // {
        //     direction = (target.position - transform.position);
        //     rb.velocity = direction.normalized * 6f;
        // }
        // else
        // {
        //     rb.velocity = Vector2.zero;
        // }

        if (rb.velocity.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (rb.velocity.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        // a.SetBool("Aggro", rb.velocity != Vector2.zero);
    }

    private void pathfollow()
    {
        if(path == null)
        {
            // Debug.Log("null path");
            return;
        }

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }else{
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint]-rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;    
        
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, target.transform.position) < chaseRange;
    }

    private bool UseBasicChase()
    {
        return Vector2.Distance(transform.position, target.transform.position) < basicChaseThreshold;
    }

    private void Patrol()
    {
        if (!isUpdatedInitialPatrolPosition)
        {
            initialPatrolPosition = transform.position;
            isUpdatedInitialPatrolPosition = true;
            rb.velocity = Vector2.zero;
        }
        patrolTimer += Time.deltaTime;
        if (Vector2.Distance(transform.position, patrolTarget) < 0.01f || patrolTimer > 2f)
        {
            patrolTarget = GenerateRandomPointInRadius();
            patrolTimer = 0f;
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolTarget, patrolSpeed * Time.deltaTime);
    }

    private Vector3 GenerateRandomPointInRadius()
    {
        var result = transform.position;
        var offset = Random.Range(-patrolRadius, patrolRadius);
        result.x = initialPatrolPosition.x + offset;
        offset = Random.Range(-patrolRadius, patrolRadius);
        result.y = initialPatrolPosition.y + offset;
        return result;
    }
}
