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
        public Point Offset;
        public RenderWorld(WorldScene base_world, ContentManager Content)
        {
            this.base_world = base_world;
            world_content = new ContentManager(Content.ServiceProvider, Content.RootDirectory);
            entity_sprites = new Dictionary<string, RenderEntity>();
            int height = base_world.Current.TileHeight / 2;
            int width = base_world.Current.TileWidth / 2;
            Offset = new Point(height, width);
        }

        public Vector2 Focus
        {
            get
            {
                Point focus = GetPosition(SceneManager.Focus);
                focus += Offset;
                return focus.ToVector2();
            }
        }

        public Point GetPosition(EntityObject e)
        {
            Vector2 location = e.Position * base_world.Current.Size;
            if (e.Timer > 0)
            {
                float progress = (float)e.Timer / e.TimerResetValue;
                Vector2 velocity = (e.FacingPosition * progress) * base_world.Current.Size;
                location -= velocity;
            }
            return location.ToPoint();
        }

        public void Draw(SpriteBatch sprite_batch, Camera2D camera)
        {
            camera.LookAt(Focus);
            sprite_batch.Begin(transformMatrix: camera.GetViewMatrix());
            sprite_batch.Draw(base_world.Current.TiledMap);
            Vector2 size = base_world.Current.Size;
            foreach (EntityObject e in base_world.Entities)
            {
                Point location = GetPosition(e);
                Rectangle rect = new Rectangle(location, size.ToPoint());
                ContainmentType contains = camera.Contains(rect);
                if (contains != ContainmentType.Disjoint)
                {
                    RenderEntity r = GetRenderEntity(e);
                    r.Draw(sprite_batch, rect);
                }
            }
            sprite_batch.End();
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
