using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Game.Common
{
    public class ManagerManager
    {
        List<BaseManager> manager_list;
        protected static ManagerManager instance = null;
        public static ManagerManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ManagerManager();
                }
                return instance;
            }
        }
        protected ManagerManager()
        {
            manager_list = new List<BaseManager>();
        }

        public void AddChild(BaseManager child)
        {
            manager_list.Add(child);
        }

        public void Update(GameTime game_time)
        {
            for (int i = 0; i < manager_list.Count; i++)
            {
                manager_list[i].Update(game_time);
            }
        }
    }
}
