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

        protected static int ControllerCount = 0;

        public Controller()
        {
            Id = ControllerCount;
            ControllerCount++;
        }
    }
}
