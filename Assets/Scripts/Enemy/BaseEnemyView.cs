using UnityEngine;

namespace ParagorGames.TestProject.Enemy
{
    public abstract class BaseEnemyView : MonoBehaviour, IMovable
    {
        public abstract Vector3 Position { get;  }
        public abstract void BeginMove();

        public abstract void Move(Vector2 moveDirection);

        public abstract void EndMove();
    }
}