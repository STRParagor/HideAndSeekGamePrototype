namespace ParagorGames.TestProject.Player
{
    public class JoystickPlayerInputHandler : IPlayerInputHandler
    {
        private readonly Joystick _joystick;
        private readonly BasePlayerView _playerView;

        public JoystickPlayerInputHandler(Joystick joystick, BasePlayerView playerView)
        {
            _joystick = joystick;
            _playerView = playerView;

            _joystick.BeginMove += _playerView.BeginMove;
            _joystick.EndMove += _playerView.EndMove;
        }
        
        public void UpdateInput()
        {
            if (_joystick.IsActive == false)
            {
                return;
            }
            
            _playerView.Move(_joystick.Direction);
        }
    }
}