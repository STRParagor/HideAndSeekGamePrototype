using System;
using UnityEngine;

namespace ParagorGames.TestProject.Enemy
{
    public class EnemiesMoveHandler : MonoBehaviour
    {
        [SerializeField] private EnemyMoveInfo[] _enemiesMoveInfo;

        private EnemyWaypointMover[] _enemyWaypointMovers;
        
        private void Start()
        {
            _enemyWaypointMovers = new EnemyWaypointMover[_enemiesMoveInfo.Length];

            for (var i = 0; i < _enemiesMoveInfo.Length; i++)
            {
                var info = _enemiesMoveInfo[i];
                _enemyWaypointMovers[i] = new EnemyWaypointMover(info.Enemy, info.TargetWaypoints, info.MoveSpeed,
                    info.StartPointIndex);
            }
        }

        private void Update()
        {
            foreach (var enemyWaypointMover in _enemyWaypointMovers)
            {
                enemyWaypointMover.UpdateMove();
            }
        }
    }
}