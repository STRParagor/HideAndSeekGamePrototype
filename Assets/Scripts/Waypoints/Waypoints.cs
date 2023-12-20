using UnityEngine;

namespace ParagorGames.TestProject.Waypoints
{
    public class Waypoints : MonoBehaviour
    {
        public WaypointInfo[] PointsInfo => _waypoints;
        public bool IsLoopPath => _loopPath;
        
        [SerializeField] private WaypointInfo[] _waypoints;
        [SerializeField] private bool _loopPath;

#if UNITY_EDITOR
        
        private readonly Color _unselectColor = new Color(0.25f, 0.5f, 0.25f, 0.25f);
        
        private void OnDrawGizmos()
        {
            DrawGizmosPath(_unselectColor);
        }

        private void OnDrawGizmosSelected()
        {
            DrawGizmosPath(Color.green);
        }

        private void DrawGizmosPath(Color color)
        {
            if (_waypoints == null || _waypoints.Length == 0)
            {
                return;
            }

            int pointsCount = _waypoints.Length;
            for (var i = 0; i < pointsCount; i++)
            {
                int nextIndex = i + 1;
                if (nextIndex >= pointsCount)
                {
                    if (_loopPath)
                    {
                        nextIndex = 0;
                    }
                    else
                    {
                        return;
                    }
                }

                var firstTransform = _waypoints[i].PointTransform;
                var secondTransform = _waypoints[nextIndex].PointTransform;

                if (firstTransform == null || secondTransform == null)
                {
                    continue;
                }

                Gizmos.color = color;
                Gizmos.DrawLine(firstTransform.position, secondTransform.position);
            }
        }
        
#endif
        
    }
}