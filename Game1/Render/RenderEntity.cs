using Game.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game.Render
{
    class RenderEntity
    {
        private Texture2D[] entity_texture;

        private EntityObject base_entity;

        public RenderEntity(EntityObject base_entity, ContentManager content)
        {
            this.base_entity = base_entity;
            entity_texture = AssetLoader.LoadEntitySprites(content, base_entity.FileName);
        }

        public void Draw(SpriteBatch sprite_batch, Rectangle rect)
        {
            Texture2D texture = entity_texture[(int)base_entity.Direction];
            sprite_batch.Draw(texture, rect, null, Color.White, base_entity.Rotation, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
