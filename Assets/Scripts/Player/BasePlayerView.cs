using UnityEngine;

namespace ParagorGames.TestProject.Player
{
    public abstract class BasePlayerView : MonoBehaviour, IMovable
    {
        public abstract void BeginMove();
        public abstract void Move(Vector2 moveDirection);
        public abstract void EndMove();
    }
}