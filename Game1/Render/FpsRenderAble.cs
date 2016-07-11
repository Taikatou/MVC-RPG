using System;
using Game.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;

namespace Game.Render
{
    public class FpsRenderAble: BaseManager, RenderAble
    {
        private FramesPerSecondCounter fps_counter;
        private BitmapFont bitmapFont;
        public FpsRenderAble(ContentManager content)
        {
            fps_counter = new FramesPerSecondCounter();
            bitmapFont = AssetLoader.LoadFont(content, "montserrat-32");
        }

        public void Draw(SpriteBatch sprite_batch)
        {
            sprite_batch.DrawString(bitmapFont, $"FPS: {fps_counter.AverageFramesPerSecond:0}", Vector2.One, Color.AliceBlue);
        }

        public override void Update(GameTime game_time)
        {
            fps_counter.Update(game_time);
        }
    }
}
