using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FrameWork.Render
{
    interface RenderAble
    {
        void Draw(SpriteBatch sprite_batch);

        void Update(GameTime game_time);
    }
}
