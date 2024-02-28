using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using IInput.Command;

namespace IInput.Command
{
    public class PlayerJump : ScriptableObject, ICharacterCommand
    {
        [SerializeField] private float speed = 5.0f;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.y, this.speed);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
