using UnityEngine;

namespace ParagorGames.TestProject.Player
{
    public class PlayerRotatorView
    {
        private readonly PlayerMoveView _moveView;
        private readonly Transform _targetTransform;
        private readonly float _rotateSpeed;
        
        private Vector3 _currentRotation;
        private float _targetRotation;

        public PlayerRotatorView(PlayerMoveView moveView, Transform targetTransform, float rotateSpeed)
        {
            _moveView = moveView;
            _targetTransform = targetTransform;
            _rotateSpeed = rotateSpeed;
        }

        public void Rotate()
        {
            var moveDirection = _moveView.MoveDirection;
            if (moveDirection != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                _targetRotation = toRotation.eulerAngles.y;
            }

            _currentRotation.y = Mathf.LerpAngle(_currentRotation.y, _targetRotation, Time.deltaTime * _rotateSpeed);
            _targetTransform.localEulerAngles = _currentRotation;
        }
    }
}