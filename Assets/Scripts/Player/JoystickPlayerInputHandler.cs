namespace ParagorGames.TestProject.Player
{
    public class JoystickPlayerInputHandler : IPlayerInputHandler
    {
        private readonly Joystick _joystick;
        private readonly IMovable _movable;

        public JoystickPlayerInputHandler(Joystick joystick, IMovable movable)
        {
            _joystick = joystick;
            _movable = movable;

            _joystick.BeginMove += _movable.BeginMove;
            _joystick.EndMove += _movable.EndMove;
        }
        
        public void UpdateInput()
        {
            if (_joystick.IsActive == false)
            {
                return;
            }
            
            _movable.Move(_joystick.Direction);
        }
    }
}