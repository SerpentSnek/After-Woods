using UnityEngine;


namespace IInput.Command
{
    public class PlayerClimbDown : ScriptableObject, IInputCommand
    {
        float climbSpeed = 5f;
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(0, -climbSpeed);
        }
    }
}