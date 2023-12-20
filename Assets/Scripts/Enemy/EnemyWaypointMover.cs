using System;
using UnityEngine;
using ParagorGames.TestProject.Waypoints;

namespace ParagorGames.TestProject.Enemy
{
    public class EnemyWaypointMover
    {
        private readonly BaseEnemyView _enemy;
        private readonly float _moveSpeed;
        private readonly WaypointInfo[] _waypoints;
        private readonly bool _isLoopPath;
        private const float ThresholdCheckDistance = 0.25f;

        private int _currentPointIndex;
        private WaypointInfo _currentWaypoint;
        private bool _isMoveBackward;
        private Vector2 _moveDirection;
        private float _waitingTime;

        private Action CurrentState;

        public EnemyWaypointMover(BaseEnemyView enemy, Waypoints.Waypoints waypoints, float moveSpeed, int startPointIndex = -1)
        {
            _enemy = enemy;
            _moveSpeed = moveSpeed;
            _waypoints = waypoints.PointsInfo;
            _isLoopPath = waypoints.IsLoopPath;
            _currentPointIndex = startPointIndex;
            SelectWaypoint();
            CurrentState = MoveToPointState;
        }

        public void UpdateMove()
        {
            CurrentState();
        }

        private void SelectWaypoint()
        {
            _currentPointIndex = Mathf.Clamp(_currentPointIndex, 0, _waypoints.Length - 1);

            _currentWaypoint = _waypoints[_currentPointIndex];
            
            _enemy.BeginMove();
        }

        private void FindNextWaypoint()
        {
            if (_isLoopPath)
            {
                _currentPointIndex += 1;
                
                if (_currentPointIndex >= _waypoints.Length)
                {
                    _currentPointIndex = 0;
                }
            }
            else
            {
                _currentPointIndex += _isMoveBackward ? -1 : 1;
                
                if (_currentPointIndex >= _waypoints.Length || _currentPointIndex < 0)
                {
                    _isMoveBackward = !_isMoveBackward;
                    _currentPointIndex += _isMoveBackward ? -2 : 2;;
                }
            }

            SelectWaypoint();
        }

        private void MoveToPointState()
        {
            var delta = (_currentWaypoint.PointTransform.position - _enemy.Position);
            delta.y = 0;
            var moveDirection = delta.normalized;
            _moveDirection.x = moveDirection.x;
            _moveDirection.y = moveDirection.z;
            _enemy.Move(_moveDirection * _moveSpeed);
            
            float distanceToPoint = delta.sqrMagnitude;

            if (distanceToPoint < ThresholdCheckDistance * ThresholdCheckDistance)
            {
                _waitingTime = 0;
                if (_currentWaypoint.WalkerWaitingSeconds > 0)
                {
                    _enemy.EndMove();
                }
                CurrentState = WaitingOnPointState;
            }
        }

        private void WaitingOnPointState()
        {
            _waitingTime += Time.deltaTime;
            
            if (_waitingTime > _currentWaypoint.WalkerWaitingSeconds)
            {
                FindNextWaypoint();
                CurrentState = MoveToPointState;
            }
        }
    }
}