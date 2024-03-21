using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovementV2 : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float gravityScale;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbingSpeed;
    [SerializeField] private float sprintSpeedFactor;
    private bool isClimbing;
    private bool isSprinting;

    private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;

    [SerializeField] private InputActionReference move, jump, sprint;

    private Animator a;
    private PlayerSoundManager sm;

    void OnEnable()
    {
        jump.action.performed += OnJumpPress;
        jump.action.canceled += OnJumpRelease;
        sprint.action.performed += OnSprintPress;
        sprint.action.canceled += OnSprintRelease;
    }

    void OnDisable()
    {
        jump.action.performed -= OnJumpPress;
        jump.action.canceled -= OnJumpRelease;
        sprint.action.performed -= OnSprintPress;
        sprint.action.canceled -= OnSprintRelease;
    }

    void Start()
    {
        a = gameObject.GetComponent<Animator>();
        sm = gameObject.GetComponent<PlayerSoundManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        gravityScale = rb.gravityScale;
    }

    void Update()
    {
        var movement = move.action.ReadValue<Vector2>();
        horizontal = movement.x;
        vertical = movement.y;

        if (horizontal > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (horizontal < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        a.SetFloat("xSpeed", Math.Abs(rb.velocity.x));
        a.SetFloat("ySpeed", Math.Abs(rb.velocity.y));
        a.SetFloat("yVelocity", rb.velocity.y);
        a.SetBool("Grounded", IsGrounded());
        a.SetBool("Climbing", isClimbing);

        if (isClimbing)
        {
            sm.PlaySound("climbing");
        }
        else
        {
            sm.StopSound("climbing");
        }

        if (IsGrounded() && horizontal != 0)
        {
            sm.PlaySound("walking");
        }
        else
        {
            sm.StopSound("walking");
        }
    }

    void FixedUpdate()
    {
        if (isSprinting)
        {
            rb.velocity = new Vector2(horizontal * speed * sprintSpeedFactor, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        if (isClimbing && rb.velocity.y < climbingSpeed)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * climbingSpeed);
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            isClimbing = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 1.5f);
        }
    }

    // private bool IsClimbing()
    // {
    //     return Physics2D.OverlapCircle(gameObject.transform.position, 0.4f, ladderLayer);
    // }

    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, new Vector2(0.75f, 0.5f), 0, groundLayer);
    }

    private void OnJumpPress(InputAction.CallbackContext obj)
    {
        if (IsGrounded() || isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            sm.PlaySound("jumping");
        }
        
    }

    private void OnJumpRelease(InputAction.CallbackContext obj)
    {
        if (rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void OnSprintPress(InputAction.CallbackContext obj)
    {
        isSprinting = true;
    }

    private void OnSprintRelease(InputAction.CallbackContext obj)
    {
        isSprinting = false;
    }
}