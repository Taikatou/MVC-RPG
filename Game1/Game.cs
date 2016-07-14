using Microsoft.Xna.Framework;
using FrameWork.Render;

using SmallGame;
using FrameWork.GameEngine.Objects;
using MonoGame.Extended.ViewportAdapters;
using FrameWork.Common;
using System;
using System.Diagnostics;
using FrameWork.GameEngine;
using FrameWork.GUI;

namespace FrameWork
{
    public class Game : CoreGame
    {
        public static Game Instance;
        public Renderer renderer;
        private ManagerManager managers;
        public Hud hud;

        public int Width = 800, Height = 480;

        public Game()
        {
            IsMouseVisible = true;
            Window.AllowUserResizing = true;
            managers = ManagerManager.Instance;
            InputManager input_manager = new InputManager();
            // IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            base.Initialize();
            DataLoader.RegisterParser(
                StandardGameObjectParser.For<TileMapObject>(),
                StandardGameObjectParser.For<EntityObject>()
            );
            BoxingViewportAdapter viewport_adapter = new BoxingViewportAdapter(Window, GraphicsDevice, Width, Height);
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
            hud = new Hud(world_level);
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
                base.Draw(game_time);
                renderer.Draw(SpriteBatch);
                hud.Draw(SpriteBatch);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
