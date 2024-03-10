using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
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

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask ladderLayer;

    [SerializeField] private InputActionReference move, jump, sprint; 

    void OnEnable()
    {
        jump.action.performed += OnJumpPress;
        jump.action.canceled += OnJumpRelease;
        sprint.action.performed += OnSprintPress;
        sprint.action.canceled += OnSprintRelease;
        gravityScale = rb.gravityScale;
    }

    void OnDisable()
    {
        jump.action.performed -= OnJumpPress;
        jump.action.canceled -= OnJumpRelease;
        sprint.action.performed -= OnSprintPress;
        sprint.action.canceled -= OnSprintRelease;
    }

    void Update()
    {
        var movement = move.action.ReadValue<Vector2>();
        horizontal = movement.x;
        vertical = movement.y;
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
            // rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    // private bool IsClimbing()
    // {
    //     return Physics2D.OverlapCircle(gameObject.transform.position, 0.4f, ladderLayer);
    // }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void OnJumpPress(InputAction.CallbackContext obj)
    {
        if (IsGrounded() || isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
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