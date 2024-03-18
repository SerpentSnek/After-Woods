using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingEnemyAI: MonoBehaviour
{
    public Transform target;
    public float activateDistance = 50f;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private bool UseOldAI = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = GameManager.Instance.Player.transform;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnpathComplete);
    }

    void OnpathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (TargetInDistance() && Vector2.Distance(rb.position, target.position) > 1.75f)
        {
            pathfollow();
        }
        else if (Vector2.Distance(rb.position, target.position) <= 1.75f)
        {
            Vector2 direction = (target.position - transform.position);
            rb.velocity = direction.normalized * 6f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
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

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < activateDistance;
    }
}
