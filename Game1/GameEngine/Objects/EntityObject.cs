using FrameWork.GameEngine.Controllers;
using Microsoft.Xna.Framework;
using SmallGame.GameObjects;
using System.Diagnostics;
using FrameWork.Common;
using System.Collections.Generic;

namespace FrameWork.GameEngine.Objects
{
    public class EntityObject : GameObject
    {
        public Direction Facing;

        public string FileName;

        public float Rotation;

        public Vector2 Position;

        public Controller controller = null;

        public bool Collidable;

        public Dictionary<string, string> InteractEvents;

        public Dictionary<string, string> OnStepEvents;

        public bool RequestMovement = false;

        public bool RequestInteract = false;

        public bool RequestMovementComplete = false;

        public int Timer;

        public int TimerResetValue = 300;

        public int TurnTimer;

        public bool CurrentlyMoving;

        public Vector2 FacingPosition
        {
            get
            {
                return DirectionMapper.GetDirection(Facing);
            }
        }

        public EntityObject()
        {
        }

        public EntityObject(Vector2 Position, string FileName, Direction Facing = Direction.Up, float Rotation=0.0f, bool Collidable=true) : base()
        {
            this.Position = Position;
            this.FileName = FileName;
            this.Collidable = Collidable;
            this.Facing = Facing;
            this.Rotation = Rotation;
            this.Type = "EntityObject";
            this.Id = "player";
        }

        public virtual void Update(GameTime game_time)
        {
            if (Timer <= 0)
            {
                if (controller != null)
                {
                    if (controller.A)
                    {
                        RequestInteract = true;
                    }
                    if (controller.B)
                    {
                        TimerResetValue = 150;
                    }
                    else
                    {
                        TimerResetValue = 300;
                    }
                    Direction facing = controller.Facing;
                    if (facing != Direction.None)
                    {
                        if (Facing == facing)
                        {
                            if(TurnTimer <= 0)
                            {
                                RequestMovement = true;
                            }
                            else
                            {
                                TurnTimer -= game_time.ElapsedGameTime.Milliseconds;
                            }
                        }
                        else
                        {
                            //Change Directions
                            if (!CurrentlyMoving)
                            {
                                TurnTimer = 100;
                                CurrentlyMoving = true;
                            }
                            Facing = facing;
                        }
                    }
                    else
                    {
                        CurrentlyMoving = false;
                    }
                }
            }
            else
            {
                if (Timer > 0)
                {
                    Timer -= game_time.ElapsedGameTime.Milliseconds;
                }
                if(!RequestMovementComplete && Timer <= 0)
                {
                    RequestMovementComplete = true;
                    Debug.WriteLine("Request complete");
                }
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
    }
}
