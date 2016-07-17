using MonoGame.Extended.ViewportAdapters;
using MonoGame.Extended;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

using System.Collections.Generic;
using FrameWork.GameEngine;

namespace FrameWork.Render
{
    /// <summary>
    /// Renderer for game, TODO add configuration changes for different options
    /// </summary>
    public class Renderer
    {
        private Camera2D camera;
        private RenderWorld cached_scene;
        public ContentManager content;
        private List<RenderAble> render_list;
        
        public Renderer(BoxingViewportAdapter viewport_adapter, ContentManager content)
        {
            this.content = content;
            camera = new Camera2D(viewport_adapter);
            render_list = new List<RenderAble>();
        }

        public void LoadNewScene(WorldScene first_world)
        {
            if(cached_scene != null)
            {
                cached_scene.Dispose();
            }
            cached_scene = new RenderWorld(first_world, content);
        }
        public void UpdateConfig(RendererConfig config)
        {
            render_list = new List<RenderAble>();
            if(config.GetKey("fps"))
            {
                render_list.Add(new FpsRenderAble(content));
            }
        }
        public void Draw(SpriteBatch sprite_batch)
        {
            cached_scene.Draw(sprite_batch, camera);
            sprite_batch.Begin();
            foreach (RenderAble r in render_list)
            {
                r.Draw(sprite_batch);
            }
            sprite_batch.End();
        }
    }
}
