using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IInput.Command;

public class PlayerController : MonoBehaviour
{
    private IInputCommand fire1;
    private IInputCommand jump;
    private IInputCommand right;
    private IInputCommand left;
    private IInputCommand climbLadder;
    private bool isClimbing = false;
    // Start is called before the first frame update
    void Start()
    {
        this.fire1 = ScriptableObject.CreateInstance<PlayerSprint>();
        this.climbLadder = ScriptableObject.CreateInstance<PlayerClimbLadder>();
        this.jump = ScriptableObject.CreateInstance<PlayerJump>();
        this.right = ScriptableObject.CreateInstance<PlayerMoveRight>();
        this.left = ScriptableObject.CreateInstance<PlayerMoveLeft>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetAxis("Horizontal") > 0.01)
        {
            this.right.Execute(this.gameObject);
        }
        if(Input.GetAxis("Horizontal") < -0.01)
        {
            this.left.Execute(this.gameObject);
        }
        if (isClimbing && Input.GetKey(KeyCode.UpArrow))
        {
            this.climbLadder.Execute(this.gameObject);
        }

    
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.jump.Execute(this.gameObject);
        }
        
        if (Input.GetButtonDown("Fire1"))
        {
            this.fire1.Execute(this.gameObject);
        }
            
    }
     void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = true;
            this.GetComponent<Rigidbody2D>().gravityScale = 0;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbing = false;
            // Restore gravity effect after climbing
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
