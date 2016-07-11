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

        public Vector2 Position
        {
            get
            {
                return base_entity.Position;
            }
        }

        private ContentManager content;

        public RenderEntity(EntityObject base_entity, ContentManager content)
        {
            this.base_entity = base_entity;
            this.content = content;
            entity_texture = AssetLoader.LoadEntitySprites(content, base_entity.FileName);
        }

        public void Draw(SpriteBatch sprite_batch, float TileWidth, float TileHeight)
        {
            Vector2 Position = base_entity.Position * new Vector2(TileWidth, TileHeight);
            Rectangle rect = new Rectangle((int)Position.X, (int)Position.Y, (int)TileWidth, (int)TileHeight);
            Texture2D texture = entity_texture[(int)base_entity.Direction];
            sprite_batch.Draw(texture, rect, null, Color.White, base_entity.Rotation, new Vector2(0, 0), SpriteEffects.None, 0);
        }
    }
}
