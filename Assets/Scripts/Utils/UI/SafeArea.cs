using UnityEngine;

namespace ParagorGames.Utils.UI
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] bool _conformX = true;
        [SerializeField] bool _conformY = true;

        private RectTransform _lastRectTransform;
        private Rect _lastSafeArea = new (0, 0, 0, 0);
        private Vector2Int _lastScreenSize = new(0, 0);
        private ScreenOrientation _lastOrientation = ScreenOrientation.AutoRotation;
        
        private bool _bannerStatusChanged;

        private void Awake ()
        {
            _lastRectTransform = GetComponent<RectTransform> ();

            if (_lastRectTransform == null)
            {
                Debug.LogError ("Cannot apply safe area - no RectTransform found on " + name);
                Destroy (gameObject);
            }

            Refresh ();
        }

        private void Refresh ()
        {
            var safeArea = Screen.safeArea;

            if (safeArea != _lastSafeArea
                || Screen.width != _lastScreenSize.x
                || Screen.height != _lastScreenSize.y
                || Screen.orientation != _lastOrientation
                || _bannerStatusChanged)
            {
                _lastScreenSize.x = Screen.width;
                _lastScreenSize.y = Screen.height;
                _lastOrientation = Screen.orientation;
                _bannerStatusChanged = false;

                ApplySafeArea (safeArea);
            }
        }
        
        private void ApplySafeArea (Rect r)
        {
            _lastSafeArea = r;
            
            if (!_conformX)
            {
                r.x = 0;
                r.width = Screen.width;
            }
            
            if (!_conformY)
            {
                r.y = 0;
                r.height = Screen.height;
            }
            
            if (Screen.width > 0 && Screen.height > 0)
            {
                var anchorMin = r.position;
                var anchorMax = r.position + r.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;

                if (anchorMin.x >= 0 && anchorMin.y >= 0 && anchorMax.x >= 0 && anchorMax.y >= 0)
                {
                    _lastRectTransform.anchorMin = anchorMin;
                    _lastRectTransform.anchorMax = anchorMax;
                }
            }
        }
    }
}
