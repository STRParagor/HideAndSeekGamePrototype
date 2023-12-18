using UnityEngine;
using UnityEngine.Assertions;
using DG.Tweening;

namespace ParagorGames.Utils.UI.Visualization
{
    public class ScaleVisualizer : MonoBehaviour
    {
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private ScaleVisualizationData _scaleData;
        
        private Vector3 _initialScale;
        private Sequence _scaleSequence;

        private void Awake()
        {
            _initialScale = _targetTransform.localScale;
        }

        public void BeginVisualization()
        {
            BeginScaleVisualization();
        }
        
        public Sequence BeginScaleVisualization()
        {
            if(_targetTransform == null) return null;
            EndScaleVisualization(false);
            
            var scaleSequence = _scaleData.ScaleSequence;
            Assert.IsTrue(scaleSequence.Length > 0, "No scale animation data");

            if (!_scaleData.IsInitialScaleFromSelf)
            {
                _targetTransform.localScale = scaleSequence[0];
            }

            _scaleSequence = DOTween.Sequence()
                .SetEase(_scaleData.EaseAnimation);
            float commonDuration = _scaleData.Duration / scaleSequence.Length;

            foreach (var scale in scaleSequence)
            {
                _scaleSequence.Append(_targetTransform.DOScale(scale, commonDuration));
            }
            
            return _scaleSequence;
        }

        public void EndScaleVisualization(bool completeAnimation)
        {
            if (_scaleSequence != null && _scaleSequence.IsActive() && _scaleSequence.IsPlaying())
            {
                _scaleSequence?.Kill(completeAnimation);
                _scaleSequence = null;
            }
        }

        public void ResetScale()
        {
            if (_targetTransform == null) return;
            _targetTransform.localScale = _initialScale;
        }
    }
}