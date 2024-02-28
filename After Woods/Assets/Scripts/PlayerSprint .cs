using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IInput.Command;

namespace IInput.Command
{
    public class PlayerSprint : ScriptableObject, IInputCommand
    {
        [SerializeField] private float speedfactor = 1.2f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x*speedfactor, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
