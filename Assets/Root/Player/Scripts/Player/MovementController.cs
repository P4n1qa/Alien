using Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Root.Player.Scripts.Player
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private FixedJoystick _fixedJoystick;
            
        private Vector3 _moveDirection;
        
        private void Update()
        {
            CalculateMoveDirection();
            Move();
            Rotate();
        }

        private void CalculateMoveDirection()
        {
            _moveDirection = MoveDirection(_fixedJoystick);
        }
        
        private Vector3 MoveDirection(FixedJoystick joystick)
        {
            float moveHorizontal = joystick.Horizontal;
            float moveVertical = joystick.Vertical;
            
            Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);

            return moveDirection;
        }
        
        private void Move()
        {
            if (_moveDirection.magnitude > 1)
            {
                _moveDirection = _moveDirection.normalized;
            }
            
            transform.position += _moveDirection * _moveSpeed * Time.deltaTime;
        }

        private void Rotate()
        {
            if (_moveDirection == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
