using Game.Controllers;
using Microsoft.Xna.Framework;

namespace Game.Engine
{
    public enum MovingState { Stopped, Left, Right, Up, Down };
    public class Moving
    {
        private MovingState state;

        public Moving()
        {
            state = MovingState.Stopped;
        }

        public MovingState State
        {
            get
            {
                return state;
            }
        }

        public Vector2 Move(float movement_speed, float deltaSeconds, Vector2 Position)
        {
            Vector2 old_position = new Vector2(Position.X, Position.Y);
            switch (state)
            {
                case MovingState.Up:
                    Position.Y += -movement_speed * deltaSeconds;
                    break;
                case MovingState.Left:
                    Position.X += -movement_speed * deltaSeconds;
                    break;
                case MovingState.Down:
                    Position.Y += movement_speed * deltaSeconds;
                    break;
                case MovingState.Right:
                    Position.X += movement_speed * deltaSeconds;
                    break;
            }
            return Position;
        }

        public void Update(GameTime game_time, Controller controller)
        {
            if (controller.Up)
            {
                state = MovingState.Up;
            }

            else if (controller.Left)
            {
                state = MovingState.Left;
            }

            else if (controller.Down)
            {
                state = MovingState.Down;
            }

            else if (controller.Right)
            {
                state = MovingState.Right;
            }
            else
            {
                state = MovingState.Stopped;
            }
        }
    }
}
