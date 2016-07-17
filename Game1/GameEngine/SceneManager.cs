using FrameWork.Common;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using FrameWork.GameEngine.Objects;

namespace FrameWork.GameEngine
{
    public class SceneManager: BaseManager
    {
        private Stack<WorldScene> loaded_scenes;

        private WorldScene current_World;

        private EntityObject player;

        public static EntityObject Focus
        {
            get
            {
                return Instance.player;
            }
        }

        public static WorldScene CurrentWorld
        {
            get
            {
                return Instance.current_World;
            }
        }

        public void PushScene(WorldScene child)
        {
            loaded_scenes.Push(child);
            current_World = child;
        }

        public void PopScene()
        {
            if (loaded_scenes.Count > 1)
            {
                loaded_scenes.Pop();
                current_World = loaded_scenes.Peek();
            }
        }

        public void PushWorld(WorldScene child)
        {
            loaded_scenes.Clear();
            PushScene(child);
            player = new EntityObject(player.Position, player.FileName, player.Facing, player.Rotation, player.Collidable);
            Debug.WriteLine("PushWorld : " + child.Name);
            child.Objects.Add(player);
        }

        public override void Update(GameTime game_time)
        {
            CurrentWorld.Update(game_time);
        }

        protected SceneManager()
        {
            loaded_scenes = new Stack<WorldScene>();
            player = new EntityObject(new Vector2(2, 2), "axe");
        }

        protected static SceneManager instance;

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneManager();
                }
                return instance;
            }
        }
    }
}
