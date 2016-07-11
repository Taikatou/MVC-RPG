using Game.Controllers;
using Microsoft.Xna.Framework;
using SmallGame.GameObjects;
using Game.Engine;
using System.Diagnostics;
using Game.Common;
using System.Collections.Generic;

namespace Game.Objects
{
    public enum Facing { Right, Left, Up, Down };
    public class EntityObject : GameObject
    {
        public Facing Direction;

        public string FileName;

        public float Rotation;

        public Vector2 Position;

        public Vector2 NewPosition;

        public Controller controller = null;

        public bool Collidable;

        public Dictionary<string, string> InteractEvents;

        public Dictionary<string, string> OnStepEvents;

        private int timer;

        public bool request_movement = false;

        public bool request_interact = false;

        public Vector2 interact_with_pos;

        public EntityObject()
        {
            timer = 0;
        }

        public EntityObject(Vector2 Position, string FileName, Facing Direction=Facing.Up, float Rotation=0.0f, bool Collidable=true) : base()
        {
            this.Position = Position;
            this.FileName = FileName;
            this.Collidable = Collidable;
            this.Direction = Direction;
            this.Rotation = Rotation;
            this.Type = "EntityObject";
            this.Id = "player";
        }

        public void Update(GameTime game_time)
        {
            if (controller != null && timer <= 0)
            {
                NewPosition = new Vector2(Position.X, Position.Y);
                if (controller.A)
                {
                    Interact();
                }
                if (controller.B)
                {
                }
                if (controller.Up)
                {
                    NewPosition += new Vector2(0, -1);
                    Direction = Facing.Up;
                }
                else if (controller.Left)
                {
                    NewPosition += new Vector2(-1, 0);
                    Direction = Facing.Left;
                }
                else if (controller.Down)
                {
                    NewPosition += new Vector2(0, 1);
                    Direction = Facing.Down;
                }
                else if (controller.Right)
                {
                    NewPosition += new Vector2(1, 0);
                    Direction = Facing.Right;
                }
                if(NewPosition != Position)
                {
                    timer = 400;
                    request_movement = true;
                }
            }
            else if (timer > 0)
            {
                timer -= game_time.ElapsedGameTime.Milliseconds;
            }
        }

        public void Interact(EventTrigger event_type, EntityObject e)
        {
            Debug.WriteLine("Interacting with: " + e.Id);
            switch(event_type)
            {
                case EventTrigger.Step:
                    if(OnStepEvents != null)
                    {
                        EventManager.Push(new EventObject(OnStepEvents));
                    }
                    break;
                case EventTrigger.Interact:
                    if (InteractEvents != null)
                    {
                        EventManager.Push(new EventObject(InteractEvents));
                    }
                    break;
                default:
                    Debug.WriteLine("Event type not recognised value: " + event_type.ToString());
                    break;
            }
        }

        public void Interact()
        {
            interact_with_pos = Position;
            switch(Direction)
            {
                case Facing.Right:
                    interact_with_pos.X++;
                    break;
                case Facing.Left:
                    interact_with_pos.X--;
                    break;
                case Facing.Up:
                    interact_with_pos.Y--;
                    break;
                case Facing.Down:
                    interact_with_pos.Y++;
                    break;
            }
            request_interact = true;
        }

        public void ControllerEvent(Controller controller)
        {
            this.controller = controller;
        }
    }
}
