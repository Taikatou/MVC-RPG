using Game.Common;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Game.Engine
{
    public class SceneManager: BaseManager
    {
        private List<WorldLevel> loaded_Worlds;

        private WorldLevel current_World;

        public static WorldLevel CurrentWorld
        {
            get
            {
                SceneManager instance = SceneManager.Instance;
                return instance.current_World;
            }
        }

        public void PushWorld(WorldLevel child)
        {
            loaded_Worlds.Add(child);
            current_World = child;
        }

        public override void Update(GameTime game_time)
        {
            CurrentWorld.Update(game_time);
        }

        protected SceneManager()
        {
            loaded_Worlds = new List<WorldLevel>();
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
