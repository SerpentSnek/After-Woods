using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character.Command;

public class PlayerController : MonoBehaviour
{
    private ICharacterCommand fire1;
    private ICharacterCommand jump;
    private ICharacterCommand right;
    private ICharacterCommand left;
    // Start is called before the first frame update
    void Start()
    {
        this.fire1 = ScriptableObject.CreateInstance<PlayerSprint>();
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
        if (Input.GetButtonDown("Space"))
        {
            this.jump.Execute(this.gameObject);
        }
    }
    void Update(){
        if (Input.GetButtonDown("Fire1"))
        {
            this.fire1.Execute(this.gameObject);
        }
    }
}
