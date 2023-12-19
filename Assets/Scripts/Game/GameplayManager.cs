using UnityEngine;
using ParagorGames.TestProject.Player;

namespace ParagorGames.TestProject.Game
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private BasePlayerView _playerView;
        [SerializeField] private Joystick _joystick;

        private IPlayerInputHandler _playerInputHandler;

        private void Awake()
        {
            _playerInputHandler = new JoystickPlayerInputHandler(_joystick, _playerView);
        }

        private void Update()
        {
            _playerInputHandler.UpdateInput();
        }
    }
}