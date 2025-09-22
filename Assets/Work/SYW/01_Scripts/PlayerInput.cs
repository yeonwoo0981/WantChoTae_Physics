using UnityEngine;
using UnityEngine.InputSystem;

namespace Work.SYW._01_Scripts
{
    public class PlayerInput : MonoBehaviour
    {
        public Vector2 MoveDir { get; private set; }
        public void OnMove(InputValue value) => MoveDir = value.Get<Vector2>();
    }
}
