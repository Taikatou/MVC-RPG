using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Game.Render;

using SmallGame;
using Game.Objects;
using Game.Engine;
using MonoGame.Extended.ViewportAdapters;
using Game.Common;
using System;
using System.Diagnostics;

namespace Game
{
    public class Game : CoreGame
    {
        public static Game Instance;
        public Renderer renderer;
        private ViewportAdapter viewport_adapter;
        private ManagerManager managers;

        public Game()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            managers = ManagerManager.Instance;
            InputManager input_manager = new InputManager();
        }

        protected override void Initialize()
        {
            base.Initialize();
            DataLoader.RegisterParser(
                StandardGameObjectParser.For<TileMapObject>(),
                StandardGameObjectParser.For<EntityObject>()
            );
            this.viewport_adapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            BoxingViewportAdapter viewport_adapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
            renderer = new Renderer(viewport_adapter, Content);
            PushWorld("level01.json");

            RendererConfig config = new RendererConfig();
            config.AddParam("fps");
            renderer.UpdateConfig(config);
        }

        public void PushWorld(string world_name)
        {
            WorldScene world_level = null;
            DataLoader.LoadAndWatch<WorldScene>(world_name, (level) => world_level = SetLevel(level));
            SceneManager.Instance.PushWorld(world_level);
            renderer.LoadNewScene(world_level);
        }

        public void PushWorld(string world_name, Vector2 end_point)
        {
            PushWorld(world_name);
            SceneManager.CurrentWorld.MoveEntity("player", end_point);
        }

        protected override void Update(GameTime game_time)
        {
            base.Update(game_time);
            managers.Update(game_time);
        }

        protected override void Draw(GameTime game_time)
        {
            try
            {
                GraphicsDevice.Clear(Color.Black);
                base.Draw(game_time);
                renderer.Draw(SpriteBatch);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
