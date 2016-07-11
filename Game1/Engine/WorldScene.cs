using Game.Common;
using Game.Controllers;
using Game.Objects;
using Microsoft.Xna.Framework;
using SmallGame;
using System.Collections.Generic;
using System.Diagnostics;

namespace Game.Engine
{
    public class WorldScene : GameLevel
    {
        protected Dictionary<int, string> entity_controllers;

        public WorldScene()
        {
            entity_controllers = new Dictionary<int, string>();
            entity_controllers.Add(ControllerSingleton.Instance.Id, "player");
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

        public void AddEntity(EntityObject e)
        {
            Objects.Add(e);
        }

        public void ControllerUpdate(Controller c)
        {
            string id = entity_controllers[c.Id];
            EntityObject e = Entities.Find(x => x.Id.Equals(id));
            if (e != null)
            {
                e.ControllerEvent(c);
            }
        }

        public void Update(EntityObject e)
        {
            if (e.request_movement)
            {
                bool in_map = e.NewPosition.X >= 0 && e.NewPosition.Y >= 0 &&
                              e.NewPosition.X < Current.TileMapWidth &&e.NewPosition.Y < Current.TileMapHeight;
                if (in_map && !Current.TileMapCollision(e.NewPosition))
                {
                    //Request Movement
                    EntityObject collide_entity = FindEntity(e.NewPosition);
                    if (collide_entity == null)
                    {
                        e.Position = e.NewPosition;
                    }
                    else if (!collide_entity.Collidable)
                    {
                        e.Position = e.NewPosition;
                        collide_entity.Interact(EventTrigger.Step, e);
                    }
                }
                e.request_movement = false;
            }
            if (e.request_interact)
            {
                EntityObject entity = FindEntity(e.interact_with_pos);
                if (entity != null)
                {
                    entity.Interact(EventTrigger.Interact, e);
                }
                e.request_interact = false;
            }
        }

        public void Update(GameTime game_time)
        {
            foreach (EntityObject e in Entities)
            {
                e.Update(game_time);
                Update(e);
            }
        }

        public EntityObject FindEntity(Vector2 position)
        {
            int x = (int)position.X, y = (int)position.Y;
            EntityObject e = Entities.Find(o => o.Position.Y == y && o.Position.X == x);
            return e;
        }

        public void MoveEntity(string id, Vector2 position)
        {
            EntityObject e = Entities.Find(x => x.Id.Equals(id));
            if (e != null)
            {
                Debug.WriteLine("Move entity with id: " + id + " to: " + position.ToString());
                e.Position = position;
            }
        }
    }
}
