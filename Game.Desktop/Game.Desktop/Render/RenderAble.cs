using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Render
{
    interface RenderAble
    {
        void Draw(SpriteBatch sprite_batch);

        void Update(GameTime game_time);
    }
}
