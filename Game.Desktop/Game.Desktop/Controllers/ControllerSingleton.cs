namespace Game.Controllers
{
    public class ControllerSingleton: Controller
    {

        protected static Controller instance = null;
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Controller();
                }
                return instance;
            }
        }

        protected ControllerSingleton()
        {

        }
    }
}
