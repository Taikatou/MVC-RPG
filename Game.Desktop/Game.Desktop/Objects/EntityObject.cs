using Game.Controllers;
using Microsoft.Xna.Framework;
using SmallGame.GameObjects;
using Game.Engine;
using System.Diagnostics;
using Game.Common;

namespace Game.Objects
{
    public enum Facing { Right, Left, Up, Down };
    public class EntityObject : GameObject
    {
        public Facing Direction;

        public string FileName;

        public float Rotation;

        public float Scale;

        public Vector2 Position;

        public Controller controller = null;

        private int timer;

        public bool Collidable;

        public EntityObject()
        {
            timer = 0;
        }

        public void Update(GameTime game_time)
        {
            if (controller != null && timer == 0)
            {
                int movement_speed = 400;
                Vector2 NewPosition = new Vector2(Position.X, Position.Y);
                if (controller.A)
                {
                    Interact();
                }
                if (controller.B)
                {
                    movement_speed = 200;
                }
                if (controller.Up)
                {
                    NewPosition.Y -= 1;
                    timer = movement_speed;
                    Direction = Facing.Up;
                }
                else if (controller.Left)
                {
                    NewPosition.X -= 1;
                    timer = movement_speed;
                    Direction = Facing.Left;
                }
                else if (controller.Down)
                {
                    NewPosition.Y += 1;
                    timer = movement_speed;
                    Direction = Facing.Down;
                }
                else if (controller.Right)
                {
                    NewPosition.X += 1;
                    timer = movement_speed;
                    Direction = Facing.Right;
                }
                if(NewPosition != Position)
                {
                    if (RequestMovement(NewPosition))
                    {
                        Position = NewPosition;
                    }
                }
            }
            else
            {
                timer -= game_time.ElapsedGameTime.Milliseconds;
                if (timer < 0)
                {
                    timer = 0;
                }
            }
        }

        public bool RequestMovement(Vector2 newPosition)
        {
            Debug.WriteLine("Move to : " + newPosition);
            WorldLevel world = SceneManager.CurrentWorld;
            world.TestMove(this, newPosition);
            Debug.WriteLine("New position: " + Position);
            return (newPosition == Position);
        }

        public void Interact(EventTrigger event_type, EntityObject e)
        {
            Debug.WriteLine("Interacting with: " + e.Id);
            EventManager.Instance.event_stack.Push(new EventObject(EventType.ChangeScene, "level02.json"));
        }

        public void Interact()
        {
            Vector2 interact_with_pos = Position;
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
            WorldLevel world = SceneManager.CurrentWorld;
            Debug.WriteLine("Interact with: " + interact_with_pos);
            world.Interact(this, interact_with_pos);
        }

        public void ControllerEvent(Controller controller)
        {
            this.controller = controller;
        }
    }
}
