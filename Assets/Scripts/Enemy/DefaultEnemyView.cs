using System;
using UnityEngine;

namespace ParagorGames.TestProject.Enemy
{
    public class DefaultEnemyView : BaseEnemyView
    {
        public override Vector3 Position => _rootTransform.position;

        [SerializeField] private Transform _rootTransform;
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _rotateSpeed = 10;
        
        private static readonly int IsMove = Animator.StringToHash("IsMove");

        private Vector3 _moveDirection;
        private Vector3 _currentRotation;
        private float _targetRotation;
        private bool _isMoving;
        

        public override void BeginMove()
        {
            _isMoving = true;
            _animator.SetBool(IsMove, true);
        }

        public override void Move(Vector2 moveDirection)
        {
            _moveDirection.x = moveDirection.x;
            _moveDirection.z = moveDirection.y;
            _characterController.SimpleMove(_moveDirection);
        }

        public override void EndMove()
        {
            _isMoving = false;
            _animator.SetBool(IsMove, false);
        }

        private void Update()
        {
            if (!_isMoving) return;
            
            Rotate();
        }

        private void Rotate()
        {
            if (_moveDirection != Vector3.zero)
            {
                var toRotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
                _targetRotation = toRotation.eulerAngles.y;
            }

            _currentRotation.y = Mathf.LerpAngle(_currentRotation.y, _targetRotation, Time.deltaTime * _rotateSpeed);
            _rootTransform.localEulerAngles = _currentRotation;
        }
    }
}