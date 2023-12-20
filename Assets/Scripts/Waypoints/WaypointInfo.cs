using System;
using UnityEngine;

namespace ParagorGames.TestProject.Waypoints
{
    [Serializable]
    public class WaypointInfo
    {
        public Transform PointTransform => _pointTransform;
        public float WalkerWaitingSeconds => _walkerWaitingSeconds;
        
        [SerializeField] private Transform _pointTransform;
        [SerializeField, Min(0)] private float _walkerWaitingSeconds;
    }
}