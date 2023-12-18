using UnityEngine;
using DG.Tweening;

namespace ParagorGames.Utils.UI.Visualization
{
    [CreateAssetMenu(fileName = nameof(ScaleVisualizationData), menuName = "ParagorGames/UI/Data/Scale Visualization Data")]
    public class ScaleVisualizationData : ScriptableObject
    {
        public Vector2[] ScaleSequence => _scaleSequence;
        public float Duration => _duration;
        public Ease EaseAnimation => _easeAnimation;
        public bool IsInitialScaleFromSelf => _isInitialScaleFromSelf;
        
        [SerializeField] private Vector2[] _scaleSequence;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _easeAnimation;
        [SerializeField] private bool _isInitialScaleFromSelf;
    }
}