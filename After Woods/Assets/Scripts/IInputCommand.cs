using UnityEngine;

namespace Input.Command
{
    public interface IInputCommand
    {
        void Execute(Rigidbody gameObject);
    }
}
