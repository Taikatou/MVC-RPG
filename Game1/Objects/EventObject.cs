using SmallGame.GameObjects;
using System.Collections.Generic;

namespace Game.Objects
{
    public enum EventType { ChangeScene, TextBox };
    public enum EventTrigger { Interact, Step };
    public class EventObject : GameObject
    {
        public Dictionary<string, string> Values;

        public EventType event_type;

        public EventObject() : base()
        {
            Values = new Dictionary<string, string>();
        }

        public EventObject(Dictionary<string, string> Values) : base()
        {
            switch(Values["Value"])
            {
                case "ChangeScene":
                    event_type = EventType.ChangeScene;
                    break;
                case "Text":
                    event_type = EventType.TextBox;
                    break;
            }
            this.Values = Values;
        }
    }
}
