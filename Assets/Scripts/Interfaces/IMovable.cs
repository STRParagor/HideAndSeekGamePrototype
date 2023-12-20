using UnityEngine;

namespace ParagorGames.TestProject
{
    public interface IMovable
    {
        void BeginMove();
        void Move(Vector2 moveDirection);
        void EndMove();
    }
}