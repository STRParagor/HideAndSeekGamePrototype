using UnityEngine;
using UnityEngine.UI;

namespace ParagorGames.UI.SubViews
{
    public class SliderProgressFillerProxy : BaseProgressFillerProxy
    {
        [SerializeField] private Slider _slider;
        
        public override void SetProgress(float progress)
        {
            progress = (_slider.maxValue - _slider.minValue) * progress;
            _slider.value = progress;
        }
    }
}