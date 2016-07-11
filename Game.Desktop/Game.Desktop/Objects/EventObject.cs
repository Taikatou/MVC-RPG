using SmallGame.GameObjects;

namespace Game.Objects
{
    public enum EventType { ChangeScene, TextBox };
    public enum EventTrigger { Interact, Step };
    public class EventObject : GameObject
    {
        public string Value;

        public EventType EventType;

        public EventObject() : base()
        {

        }

        public EventObject(EventType EventType, string Value) : base()
        {
            this.EventType = EventType;
            this.Value = Value;
        }
    }
}
