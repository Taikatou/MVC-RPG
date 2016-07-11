using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Game.Objects;
using System.Diagnostics;

namespace Game.Common
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
        public override void Update(GameTime game_time)
        {
            while(event_stack.Count > 0)
            {
                var e = event_stack.Pop();
                Debug.WriteLine("Activate event of type: " + e.EventType + " with value: " + e.Value);
                switch(e.EventType)
                {
                    case EventType.ChangeScene:
                        Game.Instance.PushWorld(e.Value);
                        break;
                    case EventType.TextBox:
                        break;
                }
            }
        }
    }
}
