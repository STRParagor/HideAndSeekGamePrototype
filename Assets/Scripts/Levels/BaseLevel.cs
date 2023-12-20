using System;
using UnityEngine;

namespace ParagorGames.TestProject.Levels
{
    public abstract class BaseLevel : MonoBehaviour
    {
        public Action LevelFinished = delegate { };
        
        [SerializeField] private TriggerReceiver _finishTrigger;

        private void OnEnable()
        {
            _finishTrigger.TriggerEnter += OnTriggerEnter;
        }

        private void OnDisable()
        {
            _finishTrigger.TriggerEnter -= OnTriggerEnter; 
        }

        private void OnTriggerEnter(Collider colliderEnter)
        {
            if (colliderEnter.CompareTag("Player"))
            {
                LevelFinished.Invoke();
            }
        }
    }
}