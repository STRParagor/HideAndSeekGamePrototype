using UnityEngine;

namespace ParagorGames.UI.SubViews
{
    public abstract class BaseProgressFillerProxy : MonoBehaviour
    {
        public abstract void SetProgress(float progress);
    }
}