using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using FrameWork.Load;
using FrameWork.GameEngine.Objects;

namespace FrameWork.Common
{
    public class EventManager: BaseManager
    {
        public Stack<EventObject> event_stack;
        protected EventManager() : base()
        {
            event_stack = new Stack<EventObject>();
        }
        protected static EventManager instance;
        public static EventManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new EventManager();
                }
                return instance;
            }
        }

        public static void Push(EventObject e)
        {
            Instance.event_stack.Push(e);
        }

        public override void Update(GameTime game_time)
        {
            while(event_stack.Count > 0)
            {
                var e = event_stack.Pop();
                Debug.WriteLine("Activate event of type: " + e.event_type + " with value: " + e.Values.ToString());
                switch(e.event_type)
                {
                    case EventType.ChangeScene:
                        Vector2 end_point = Loader.StringToVector2(e.Values["EndPoint"]);
                        Game.Instance.PushWorld(e.Values["WorldName"], end_point);
                        break;
                    case EventType.TextBox:
                        break;
                }
            }
        }
    }
}
