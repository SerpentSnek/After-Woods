using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IInput.Command;

namespace IInput.Command
{
    public class PlayerMoveLeft : ScriptableObject, IInputCommand
    {
        private float speed = 2.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(-this.speed, rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}