﻿using Pathfinding;
using System.Collections;
using UnityEngine;

public class BeastAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f, jumpForce = 100f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 1f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true, isJumping, isInAir;
    public bool directionLookEnabled = true;

    [SerializeField] Vector3 startOffset;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] private Transform groundCheck;
    [SerializeField] public bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    Seeker seeker;
    Rigidbody2D rb;
    private bool isOnCoolDown;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false; 

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void Update()
    {
        Debug.Log(isGrounded);
    }
    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset, transform.position.z);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 2.64f, groundLayer);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        float heightDifference = Mathf.Log(target.position.y - rb.position.y+1)+1;

    // Adjust jump force based on height difference
        float modifiedJumpForce = jumpForce + heightDifference * jumpModifier;

        // Jump
        if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                if (isInAir) return; 
                isJumping = true;
                rb.AddForce(Vector2.up * modifiedJumpForce);
                StartCoroutine(JumpCoolDown());

            }
        }
        if (isGrounded)
        {
            isJumping = false;
            isInAir = false; 
        }
        else
        {
            isInAir = true;
        }

        // Movement
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.01f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator JumpCoolDown()
    {
        isOnCoolDown = true; 
        yield return new WaitForSeconds(1f);
        isOnCoolDown = false;
    }
}