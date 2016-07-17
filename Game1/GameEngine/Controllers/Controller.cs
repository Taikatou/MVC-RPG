using FrameWork.Common;

namespace FrameWork.GameEngine.Controllers
{
    public class Controller
    {
        public bool Right = false;
        public bool Left = false;
        public bool Up = false;
        public bool Down = false;
        public bool A = false;
        public bool B = false;
        public int Id;

        public Direction Facing
        {
            get
            {
                if(Right)
                {
                    return Direction.Right;
                }
                else if (Left)
                {
                    return Direction.Left;
                }
                else if (Up)
                {
                    return Direction.Up;
                }
                else if (Down)
                {
                    return Direction.Down;
                }
                else
                {
                    return Direction.None;
                }
            }
        }

        protected static int ControllerCount = 0;

        public Controller()
        {
            Id = ControllerCount;
            ControllerCount++;
        }
    }
}
