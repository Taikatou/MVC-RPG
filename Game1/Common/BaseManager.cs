﻿using Microsoft.Xna.Framework;

namespace FrameWork.Common
{
    public abstract class BaseManager
    {
        protected BaseManager()
        {
            ManagerManager instance = ManagerManager.Instance;
            instance.AddChild(this);
        }

        abstract public void Update(GameTime game_time);
    }
}
