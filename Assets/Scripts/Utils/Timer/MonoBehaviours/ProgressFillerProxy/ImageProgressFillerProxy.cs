using UnityEngine;
using UnityEngine.UI;

namespace ParagorGames.UI.SubViews
{
    public class ImageProgressFillerProxy : BaseProgressFillerProxy
    {
        [SerializeField] private Image _image;
        
        public override void SetProgress(float progress)
        {
            _image.fillAmount = progress;
        }
    }
}