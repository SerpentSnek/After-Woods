using Pathfinding;
using System;
using System.Collections;
using UnityEngine;

public class BeastAI : MonoBehaviour
{
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance;
    public float pathUpdateSeconds;

    [Header("Physics")]
    public float speed, jumpForce;
    public float nextWaypointDistance;
    public float jumpNodeHeightRequirement;
    public float jumpModifier;
    public float jumpCheckOffset;

    [Header("Custom Behavior")]
    public bool followEnabled;
    public bool jumpEnabled, isJumping, isInAir;
    public bool directionLookEnabled;
    [SerializeField] private float attackRange;

    [SerializeField] Vector3 startOffset;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] private Transform groundCheck;
    [SerializeField] public bool isGrounded;
    [SerializeField] private LayerMask groundLayer;
    Seeker seeker;
    Rigidbody2D rb;
    private bool isOnCoolDown;

    private Vector2 lastPos;
    private float pollTime = 0f;
    private Animator a;
    private BeastSoundManager sm;
    private bool cachedActivated;
    private float jumpTimer;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        a = gameObject.GetComponent<Animator>();
        sm = gameObject.GetComponent<BeastSoundManager>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false; 
        target = GameManager.Instance.Player.transform;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    void Update()
    {
        if (!GameManager.Instance.Player.GetComponent<PlayerLogicController>().IsDead)
        {
            if (TargetInDistance() && followEnabled)
            {
                PathFollow();
            }

            pollTime += Time.deltaTime;
            if (pollTime > 1f && followEnabled && isGrounded && !isInAir && !isOnCoolDown)
            {
                pollTime = 0f;
                if (Vector2.Distance(rb.position, lastPos) < 1f)
                {
                    rb.AddForce(new Vector2(UnityEngine.Random.Range(200, 500), UnityEngine.Random.Range(700, 1200)));
                }
                lastPos = rb.position;
            }
            var timer = GameManager.Instance.Timer;
            followEnabled = timer.IsTimeUp;

            a.SetFloat("xSpeed", Math.Abs(rb.velocity.x));
            a.SetBool("Activated", cachedActivated == false && followEnabled);

            if (followEnabled)
            {
                sm.StartChaseBGM();
                if (cachedActivated == false)
                {
                    sm.PlaySound("roar");
                }

                if (!isJumping)
                {
                    sm.PlaySound("stomp");
                }
                else
                {
                    sm.StopSound("stomp");
                }
            }
            else
            {
                sm.StopSound("roar");
            }

            cachedActivated = followEnabled;
            a.SetBool("Attack", Vector2.Distance(this.gameObject.transform.position, target.position) < attackRange && followEnabled);
            if (Vector2.Distance(this.gameObject.transform.position, target.position) < attackRange)
            {
                sm.PlaySound("roar"); ;
            }

            // Debug.Log(isGrounded);
        }
        else
        {
            sm.StopSound("stomp");
            sm.StopChaseBGM();
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
        // isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1f, groundLayer);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(4, 1), 0, groundLayer);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        float heightDifference = Mathf.Log(target.position.y - rb.position.y+1)+1;

    // Adjust jump force based on height difference
        float modifiedJumpForce = jumpForce + heightDifference * jumpModifier;
        jumpTimer += Time.deltaTime;
        isOnCoolDown = jumpTimer < 1f;

        // Jump
        if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                if (isInAir) return; 
                isJumping = true;
                if (modifiedJumpForce > 1f)
                {
                    rb.AddForce(Vector2.up * modifiedJumpForce);
                }
                
                jumpTimer = 0f;
                // Debug.Log("jumped");
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
        rb.AddForce(new Vector2(force.x, 0f));

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
}