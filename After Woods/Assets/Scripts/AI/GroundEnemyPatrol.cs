using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyPatrol : MonoBehaviour
{
    public Transform target;
    public float activateDistance;
    public float speed;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentpoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint = pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TargetInDistance())
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if(currentpoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

        if(Vector2.Distance(transform.position, currentpoint.position)<1f && currentpoint == pointB.transform)
        {
            changedirection();
            currentpoint = pointA.transform;
        }
        if(Vector2.Distance(transform.position, currentpoint.position)<1f && currentpoint == pointA.transform)
        {
            changedirection();
            currentpoint = pointB.transform;
        }
    }
    private void changedirection()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }
}
