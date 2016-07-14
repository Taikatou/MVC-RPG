using FrameWork.Common;
using FrameWork.GameEngine.Controllers;
using FrameWork.GameEngine.Objects;
using Microsoft.Xna.Framework;
using SmallGame;
using System.Collections.Generic;
using System.Diagnostics;

namespace FrameWork.GameEngine
{
    public class WorldScene : GameLevel
    {
        protected Dictionary<int, string> entity_controllers;
        public Dictionary<Vector2, EntityObject> entity_spatial_hash;
        public Dictionary<Vector2, EntityObject> EntitySpatialHash
        {
            get
            {
                if(entity_spatial_hash == null)
                {
                    entity_spatial_hash = new Dictionary<Vector2, EntityObject>();
                    ReLoadSpatialHash();
                }
                return entity_spatial_hash;
            }
        }
        private Current current;
        public Current Current
        {
            get
            {
                if(current == null)
                {
                    current = new Current(TileMap.FileName);
                }
                return current;
            }
        }

        public void ReLoadSpatialHash()
        {
            EntitySpatialHash.Clear();
            foreach (EntityObject e in Entities)
            {
                EntitySpatialHash[e.Position] = e;
            }
        }

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

        public EntityObject TryGetEntityHash(Vector2 position)
        {
            if (EntitySpatialHash.ContainsKey(position))
            {
                return EntitySpatialHash[position];
            }
            else
            {
                return null;
            }
        }

        public bool EntityCollision(Vector2 position)
        {
            EntityObject e = TryGetEntityHash(position);
            bool collision = !(e == null || !e.Collidable);
            return collision;
        }

        public bool CollisionGridTest(Vector2 position, bool Moving = false)
        {
            int x = (int)(position.X), y = (int)(position.Y);
            bool collision = Current.TileMapCollision(x, y) || EntityCollision(position);
            return collision;
        }

        public void CheckStepOn(EntityObject e, Vector2 position)
        {
            EntityObject collide_entity = TryGetEntityHash(position);
            if (collide_entity != null && !collide_entity.Collidable)
            {
                collide_entity.Interact(EventTrigger.Step, e);
            }
        }

        public void Update(EntityObject e)
        {
            List<Vector2> keys_to_remove = new List<Vector2>();
            if (e.request_movement)
            {
                bool in_map = e.NewPosition.X >= 0 && e.NewPosition.Y >= 0 &&
                              e.NewPosition.X < Current.TileMapWidth &&e.NewPosition.Y < Current.TileMapHeight;
                if (in_map && !CollisionGridTest(e.NewPosition))
                {
                    //Request Movement
                    CheckStepOn(e, e.NewPosition);
                    keys_to_remove.Add(e.Position);
                    e.Position = e.NewPosition;
                    EntitySpatialHash[e.Position] = e;
                }
                e.request_movement = false;
            }
            if (e.request_interact)
            {
                EntityObject entity = TryGetEntityHash(e.interact_with_pos);
                if (entity != null)
                {
                    entity.Interact(EventTrigger.Interact, e);
                }
                e.request_interact = false;
            }
            //Remove all keys all movment for this tick is over
            foreach(Vector2 v in keys_to_remove)
            {
                EntitySpatialHash.Remove(v);
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
