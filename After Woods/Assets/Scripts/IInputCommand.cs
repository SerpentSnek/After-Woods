using UnityEngine;

namespace IInput.Command
{
    public interface IInputCommand
    {
        void Execute(Rigidbody gameObject);
    }
}
