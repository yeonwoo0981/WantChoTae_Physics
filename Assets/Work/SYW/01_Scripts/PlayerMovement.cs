using UnityEngine;

namespace Work.SYW._01_Scripts
{
   public class PlayerMovement : MonoBehaviour
   {
      [SerializeField] private float _speed = 5f;
      private float _xMove;
      public Rigidbody2D Rb { get; private set; }
   
      private void Awake() => Rb = GetComponent<Rigidbody2D>();
      public void SetXMove(float xMove) => _xMove = xMove;

      private void FixedUpdate()
      {
         HorizontalMove();
      }

      private void HorizontalMove()
      {
         Rb.linearVelocity = new Vector2(_xMove * _speed, Rb.linearVelocity.y);
      }
   }
}
