using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using System;

namespace FrameWork.GUI
{
    class HudElement : Sprite
    {
        Action action;
        public HudElement(Texture2D texture, Action action): base(texture)
        {
            this.action = action;
        }

        public void OnClick()
        {
            action.Invoke();
        }
    }
}
