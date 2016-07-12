using System;
using FrameWork.Common;
using FrameWork.GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrameWork.GUI
{
    public class Hud : BaseManager
    {
        protected static Hud instance;

        public static Hud Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Hud();
                }
                return instance;
            }
        }

        public void Draw(SpriteBatch SpriteBatch)
        {

        }

        public void Click(Point position)
        {

        }

        public override void Update(GameTime game_time)
        {
        }
    }
}
