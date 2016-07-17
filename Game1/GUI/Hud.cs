using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using MonoGame.Extended.Sprites;
using Microsoft.Xna.Framework.Content;
using System;
using FrameWork.Load;
using FrameWork.GameEngine;

namespace FrameWork.GUI
{
    public class Hud
    {
        private List<HudElement> HudElements;

        public ContentManager hud_content;

        public Hud()
        {
            HudElements = new List<HudElement>();
            hud_content = new ContentManager(Game.Instance.Content.ServiceProvider, Game.Instance.Content.RootDirectory);
        }

        public Hud(WorldScene w) : this()
        {
            AddHudElement("spike_ball", () => Console.WriteLine("Click"),
                          new Vector2(Game.Instance.Width * 0.9f, Game.Instance.Height * 0.15f),
                          new Vector2(1, 1));
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            SpriteBatch.Begin();
            foreach (Sprite s in HudElements)
            {
                s.Draw(SpriteBatch);
            }
            SpriteBatch.End();
        }

        public void Click(Point position)
        {
            foreach (HudElement s in HudElements)
            {
                bool clicked = s.GetBoundingRectangle().Contains(position);
                if(clicked)
                {
                    s.OnClick();
                }
            }
        }

        public void AddHudElement(string file_name, Action action, Vector2 position, Vector2 scale)
        {
            Texture2D texture = AssetLoader.LoadSprite(hud_content, file_name);
            HudElement element = new HudElement(texture, action);
            element.Position = position;
            element.Scale = scale;
            HudElements.Add(element);
        }
    }
}
