using Game.Controllers;
using Game.Objects;
using Microsoft.Xna.Framework;
using SmallGame;
using System.Collections.Generic;

namespace Game.Engine
{
    public class WorldLevel : GameLevel
    {
        public Vector2 Focus
        {
            get
            {
                return Entities[0].Position;
            }
        }
        protected Dictionary<int, int> entity_controllers;

        public WorldLevel()
        {
            entity_controllers = new Dictionary<int, int>();
            entity_controllers.Add(ControllerSingleton.Instance.Id, 0);
        }
        public TileMapObject TileMap
        {
            get
            {
                List<TileMapObject> tile_maps = Objects.GetObjects<TileMapObject>();
                return tile_maps[0];
            }
        }
        public List<EntityObject> Entities
        {
            get
            {
                return Objects.GetObjects<EntityObject>();
            }
        }

        public void ControllerUpdate(Controller c)
        {
            int index = entity_controllers[c.Id];
            Entities[index].ControllerEvent(c);
        }

        public void Update(GameTime game_time)
        {
            foreach (EntityObject e in Entities)
            {
                e.Update(game_time);
            }
        }

        public bool TestMove(EntityObject entity, Vector2 newPosition)
        {
            if (newPosition.Y < 0 || newPosition.X < 0)
            {
                return false;
            }
            EntityObject e = FindEntity(newPosition);
            if (e == null)
            {
                entity.Position = newPosition;
                return true;
            }
            else if (!e.Collidable)
            {
                e.Interact(EventTrigger.Step, entity);
                entity.Position = newPosition;
                return true;
            }
            return false;
        }

        public EntityObject FindEntity(Vector2 position)
        {
            int x = (int)position.X, y = (int)position.Y;
            EntityObject e = Entities.Find(o => o.Position.X == x && o.Position.Y == y);
            return e;
        }

        public void Interact(EntityObject entity, Vector2 position)
        {
            EntityObject e = FindEntity(position);
            if (e != null)
            {
                e.Interact(EventTrigger.Interact, entity);
            }
        }
    }
}
