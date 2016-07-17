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
        public Dictionary<string, Vector2> MovingEntities;
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
            MovingEntities = new Dictionary<string, Vector2>();
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
                List<EntityObject> to_return = Objects.GetObjects<EntityObject>();
                to_return.AddRange(Objects.GetObjects<NPCObject>());
                return to_return;
            }
        }

        public void ControllerUpdate(Controller c)
        {
            string id = entity_controllers[c.Id];
            EntityObject e = Entities.Find(x => x.Id.Equals(id));
            if (e != null)
            {
                e.controller = c;
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
            bool collision = Current.TileMapCollision(position) || EntityCollision(position);
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

        public void CheckEntityChange(EntityObject e)
        {
            Vector2 new_position = e.Position + e.FacingPosition;
            if (e.RequestMovement)
            {
                bool in_map = new_position.X >= 0 && new_position.Y >= 0 &&
                              new_position.X < Current.TileMapWidth && new_position.Y < Current.TileMapHeight;
                if (in_map && !CollisionGridTest(new_position))
                {
                    //Request Movement
                    CheckStepOn(e, new_position);
                    MovingEntities[e.Id] = e.Position;
                    e.Position = new_position;
                    e.Timer = e.TimerResetValue;
                    EntitySpatialHash[e.Position] = e;
                }
                e.RequestMovement = false;
            }
            if(e.RequestMovementComplete)
            {
                e.RequestMovementComplete = false;
                Debug.WriteLine("Remove entity");
                EntitySpatialHash.Remove(MovingEntities[e.Id]);
                MovingEntities.Remove(e.Id);
            }
            if (e.RequestInteract)
            {
                EntityObject entity = TryGetEntityHash(new_position);
                if (entity != null)
                {
                    entity.Interact(EventTrigger.Interact, e);
                }
                e.RequestInteract = false;
            }
        }

        public void Update(GameTime game_time)
        {
            foreach (EntityObject e in Entities)
            {
                e.Update(game_time);
                CheckEntityChange(e);
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
