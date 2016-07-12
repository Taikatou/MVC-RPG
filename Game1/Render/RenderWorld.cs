using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Sprites;
using FrameWork.GameEngine.Objects;
using System.Diagnostics;
using System;
using MonoGame.Extended;
using Microsoft.Xna.Framework;
using FrameWork.GameEngine;

namespace FrameWork.Render
{
    class RenderWorld: IDisposable
    {
        public WorldScene base_world;
        private ContentManager world_content;
        private Dictionary<string, RenderEntity> entity_sprites;
        public Vector2 Offset;
        public RenderWorld(WorldScene base_world, ContentManager Content)
        {
            this.base_world = base_world;
            world_content = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            entity_sprites = new Dictionary<string, RenderEntity>();
            float height = base_world.Current.TileHeight / 2;
            float width = base_world.Current.TileWidth / 2;
            Offset =  new Vector2(height, width);
        }

        public Vector2 Focus
        {
            get
            {
                Vector2 size = base_world.Current.Size;
                Vector2 focus = SceneManager.Focus * size;
                return focus + Offset;
            }
        }

        public void Draw(SpriteBatch sprite_batch, Camera2D camera)
        {
            sprite_batch.Draw(base_world.Current.TiledMap);
            Vector2 size = base_world.Current.Size;
            foreach (EntityObject e in base_world.Entities)
            {
                Vector2 location = e.Position * base_world.Current.Size;
                Rectangle rect = new Rectangle(location.ToPoint(), size.ToPoint());
                ContainmentType contains = camera.Contains(rect);
                if (contains != ContainmentType.Disjoint)
                {
                    RenderEntity r = GetRenderEntity(e);
                    r.Draw(sprite_batch, rect);
                }
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

        public void Dispose()
        {
            Debug.WriteLine("Unload render world " + base_world.Name);
            world_content.Unload();
            world_content.Dispose();
        }
    }
}
