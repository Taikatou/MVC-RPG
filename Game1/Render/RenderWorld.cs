using Game.Engine;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Maps.Tiled;
using MonoGame.Extended.Sprites;
using Game.Objects;
using System.Diagnostics;
using System;
using Game.Common;
using MonoGame.Extended;
using Microsoft.Xna.Framework;

namespace Game.Render
{
    class RenderWorld: IDisposable
    {
        private WorldScene base_world;
        private ContentManager world_content;
        private Dictionary<string, RenderEntity> entity_sprites;
        public RenderWorld(WorldScene base_world, ContentManager Content)
        {
            this.base_world = base_world;
            world_content = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            entity_sprites = new Dictionary<string, RenderEntity>();
        }

        public void Draw(SpriteBatch sprite_batch, Camera2D camera)
        {
            sprite_batch.Draw(Current.TiledMap);
            Point size = Current.Size.ToPoint();
            foreach (EntityObject e in base_world.Entities)
            {
                Vector2 location = e.Position * Current.Size;
                Rectangle rect = new Rectangle(location.ToPoint(), size);
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
            Current.ResetTileMap();
        }
    }
}
