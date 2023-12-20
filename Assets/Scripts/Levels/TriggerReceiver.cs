using System;
using UnityEngine;

namespace ParagorGames.TestProject.Levels
{
    public class TriggerReceiver : MonoBehaviour
    {
        public Action<Collider> TriggerEnter = delegate {  };
        public Action<Collider> TriggerExit = delegate {  };

        private void OnTriggerEnter(Collider other)
        {
            TriggerEnter.Invoke(other);
        }

        private void OnTriggerExit(Collider other)
        {
            TriggerExit.Invoke(other);
        }
    }
}