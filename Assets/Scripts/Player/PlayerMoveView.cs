using UnityEngine;

namespace ParagorGames.TestProject.Player
{
    public class PlayerMoveView
    {
        public Vector3 MoveDirection => _moveDirection;
        
        private readonly Animator _characterAnimator;
        private readonly CharacterController _characterController;
        private readonly Transform _cameraTransform;
        private readonly float _moveSpeed;
        
        private bool _isMoving;
        private Vector3 _moveDirection;
        
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public PlayerMoveView(Animator characterAnimator, CharacterController characterController, Transform cameraTransform, float moveSpeed)
        {
            _characterAnimator = characterAnimator;
            _characterController = characterController;
            _cameraTransform = cameraTransform;
            _moveSpeed = moveSpeed;
        }


        public void BeginMove()
        {
            SetMoveState(true);
        }

        public void Move(Vector2 moveDirection)
        {
            var right = _cameraTransform.right;
            right.y = 0;
            right.Normalize();
            
            var forward = _cameraTransform.forward;
            forward.y = 0;
            forward.Normalize();
            
            _moveDirection.x = moveDirection.x * right.x;
            _moveDirection.z = moveDirection.y * forward.z;
            _characterController.SimpleMove(_moveDirection * _moveSpeed);
        }

        public void EndMove()
        {
            _moveDirection = Vector3.zero;
            SetMoveState(false);
        }

        private void SetMoveState(bool isMoving)
        {
            _isMoving = isMoving;
            _characterAnimator.SetBool(IsWalk, _isMoving);
        }
    }
}