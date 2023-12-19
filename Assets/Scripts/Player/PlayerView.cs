using UnityEngine;

namespace ParagorGames.TestProject.Player
{
    public sealed class PlayerView : BasePlayerView
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private Animator _characterAnimator;
        [SerializeField] private float _moveSpeed = 6f;
        [SerializeField] private float _rotateSpeed = 10f;

        private PlayerMoveView _moveView;
        private PlayerRotatorView _rotatorView;
        
        private Transform _selfTransform;
        
        private void Awake()
        {
            _moveView = new PlayerMoveView(_characterAnimator, _characterController, _cameraTransform, _moveSpeed);
            _rotatorView = new PlayerRotatorView(_moveView, transform, _rotateSpeed);
        }

        public override void BeginMove()
        {
            _moveView.BeginMove();
        }

        public override void Move(Vector2 moveDirection)
        {
            _moveView.Move(moveDirection);
        }

        public override void EndMove()
        {
            _moveView.EndMove();
        }

        private void Update()
        {
            _rotatorView.Rotate();
        }
    }
}