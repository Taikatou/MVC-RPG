using Game.Engine;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Sprites;
using Game.Objects;
using System.Diagnostics;

namespace Game.Render
{
    class RenderWorld
    {
        public TiledMap tiled_map;
        private WorldLevel base_world;
        private ContentManager world_content;
        private Dictionary<string, RenderEntity> entity_sprites;
        public float TileHeight
        {
            get
            {
                return tiled_map.TileHeight;
            }
        }
        public float TileWidth
        {
            get
            {
                return tiled_map.TileWidth;
            }
        }
        public string Name
        {
            get
            {
                return base_world.Name;
            }
        }
        public RenderWorld(WorldLevel base_world, ContentManager Content)
        {
            this.base_world = base_world;
            world_content = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            tiled_map = AssetLoader.LoadMap(world_content, base_world.TileMap.FileName);
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void UnLoad()
        {
            Debug.WriteLine("Unload world " + base_world.Name);
            world_content.Unload();
            world_content.Dispose();
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.Draw(tiled_map);
            foreach(EntityObject e in base_world.Entities)
            {
                RenderEntity r = GetRenderEntity(e);
                r.Draw(sprite_batch, TileWidth, TileHeight);
            }
        }

        public RenderEntity GetRenderEntity(EntityObject e)
        {
            if (!entity_sprites.ContainsKey(e.Id))
            {
                entity_sprites[e.Id] = new RenderEntity(e, world_content);
            }
            return entity_sprites[e.Id];
        }
    }
}
