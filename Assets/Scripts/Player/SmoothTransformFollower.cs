using UnityEngine;

namespace ParagorGames.TestProject.Game
{
    public class SmoothTransformFollower : MonoBehaviour
    {
        [SerializeField] private Transform _currentTransform;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speedFollow = 5f;

        private void Update()
        {
            _currentTransform.position = Vector3.Lerp(_currentTransform.position, _targetTransform.position + _offset,
                Time.deltaTime * _speedFollow);
        }
    }
}