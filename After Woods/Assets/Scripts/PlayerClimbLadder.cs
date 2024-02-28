using UnityEngine;
using IInput.Command;

public class PlayerClimbLadder : ScriptableObject, IInputCommand
{
    public void Execute(GameObject gameObject)
    {
        float climbSpeed = 5f;
        var rigidBody = gameObject.GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, climbSpeed);
    }
}