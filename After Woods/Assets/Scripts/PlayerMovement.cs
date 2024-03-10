using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IInput.Command;

public class PlayerMovement : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private IInputCommand fire3;
    private IInputCommand jump;
    private IInputCommand right;
    private IInputCommand left;
    private IInputCommand climbup;
    private IInputCommand climbdown;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    private bool isOnGround;
    private bool isClimbup;
    private bool isClimbdown;
    private bool isLadder;
    private bool isSprint;
    private bool isSprinting;
    private bool isMovingRight;
    private bool isMovingLeft;
    private bool isJump;

    void Start()
    {
        this.fire3 = ScriptableObject.CreateInstance<PlayerSprint>();
        this.climbup = ScriptableObject.CreateInstance<PlayerClimbUp>();
        this.climbdown = ScriptableObject.CreateInstance<PlayerClimbDown>();
        this.jump = ScriptableObject.CreateInstance<PlayerJump>();
        this.right = ScriptableObject.CreateInstance<PlayerMoveRight>();
        this.left = ScriptableObject.CreateInstance<PlayerMoveLeft>();
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (isClimbup)
        {
            this.climbup.Execute(this.gameObject);
        }
        else if (isClimbdown)
        {
            this.climbdown.Execute(this.gameObject);
        }
        else if (coll.IsTouchingLayers(ladderLayer) && Input.GetAxis("Horizontal") == 0)
        {
            rb.velocity = new Vector2(0, 0);
        }

        if (isMovingRight)
        {
            this.right.Execute(this.gameObject);
        }

        if (isMovingLeft)
        {
            this.left.Execute(this.gameObject);
        }

    }
    void Update()
    {
        isOnGroundCheck();
        if (isLadder && Input.GetAxis("Vertical") > 0.01)
        {
            isClimbup = true;
        }
        else if (isLadder && Input.GetAxis("Vertical") < -0.01)
        {
            isClimbdown = true;
        }
        else
        {
            isClimbup = false;
            isClimbdown = false;
        }

        if (Input.GetAxis("Horizontal") > 0.01)
        {
            isMovingRight = true;
        }
        else
        {
            isMovingRight = false;
        }

        if (Input.GetAxis("Horizontal") < -0.01)
        {
            isMovingLeft = true;
        }
        else
        {
            isMovingLeft = false;
        }

        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            this.jump.Execute(this.gameObject);
            // Debug.Log(rb.velocity.y);
        }

        if (isOnGround && Input.GetButton("Jump"))
        {
            this.jump.Execute(this.gameObject);
            // Debug.Log(rb.velocity.y);
        }

        if (Input.GetButtonDown("Fire3"))
        {
            this.fire3.Execute(this.gameObject);
            Debug.Log(rb.velocity.x);
        }

        if (Input.GetButton("Fire3"))
        {
            this.fire3.Execute(this.gameObject);
        }

        // Debug.Log(rb.velocity.x);
    }
    void isOnGroundCheck()
    {
        if (coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
            rb.gravityScale = 0;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            rb.gravityScale = 1;
        }
    }
}