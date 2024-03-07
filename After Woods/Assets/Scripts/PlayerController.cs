using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IInput.Command;

public class PlayerController : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private IInputCommand fire1;
    private IInputCommand jump;
    private IInputCommand right;
    private IInputCommand left;
    private IInputCommand climbup;
    private IInputCommand climbdown;
    public LayerMask groundLayer;     
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
        this.fire1 = ScriptableObject.CreateInstance<PlayerSprint>();
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
            rb.gravityScale = 0;
            this.climbup.Execute(this.gameObject);
        }
       else if(isClimbdown)
        {
            this.climbdown.Execute(this.gameObject);
        }
        else
        {
            rb.velocity = new Vector(0,0);
        }

        if(isMovingRight)
        {
            this.right.Execute(this.gameObject);
        }

        if(isMovingLeft)
        {
            this.left.Execute(this.gameObject);
        }
        if(isJump)
        {
            this.jump.Execute(this.gameObject);
        }
    }    
    void Update()
    {
        isOnGroundCheck();
        Debug.Log("debug");
        isSprintingCheck();
        if(isLadder && Input.GetAxis("Vertical") > 0.01)
        {
            isClimbup = true;
        }
        else if(isLadder && Input.GetAxis("Vertical") < -0.01)
        {
            isClimbdown = true;
        }
        else
        {
            isClimbup = false;
            isClimbdown = false;
        }

        if(rb.velocity.x < 5.0f && Input.GetAxis("Horizontal") > 0.01)
        {
            isMovingRight = true;
        }
        else
        {
            isMovingRight = false;
        }

        if(rb.velocity.x > -5.0f && Input.GetAxis("Horizontal") < -0.01)
        {
            isMovingLeft = true;
        }
        else
        {
            isMovingLeft = false;
        }

        if (isOnGround && Input.GetButtonDown("Jump"))
        {
            isJump = true;
            Debug.Log("jumping"+ rb.velocity.y);
        }
        else
        {
            isJump = false;
        }
        
        if (isSprinting && Input.GetButtonDown("Fire1"))
        {
            this.fire1.Execute(this.gameObject);
        }
            
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
    void isSprintingCheck()
    {
        if(Mathf.Abs(rb.velocity.x) < 5.1f)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting =false;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isLadder = false;
            rb.gravityScale = 1f;
        }
    }
}
