using System;
using UnityEngine;

namespace ParagorGames.TestProject.Enemy
{
    [Serializable]
    public class EnemyMoveInfo
    {
        public BaseEnemyView Enemy => _baseEnemy;

        public Waypoints.Waypoints TargetWaypoints => _waypoints;
        
        public int StartPointIndex => _startPointIndex;

        public float MoveSpeed => _moveSpeed;
        
        [SerializeField] private BaseEnemyView _baseEnemy;
        [SerializeField] private Waypoints.Waypoints _waypoints;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _startPointIndex;
    }
}