using FrameWork.Load;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;

namespace FrameWork.Common
{
    public class Current
    {
        public TiledMap TiledMap;

        public Current(string fileName)
        {
            TiledMap = AssetLoader.LoadMap(Game.Instance.renderer.content, fileName);
            size = new Vector2(TileWidth, TileHeight);
        }

        public TiledTileLayer TileCollisionLayer
        {
            get
            {
                return TiledMap.GetLayer<TiledTileLayer>("Collision Layer");
            }
        }

        public bool TileMapCollision(int x, int y)
        {
            TiledTile t = TileCollisionLayer.GetTile(x, y);
            if(t == null)
            {
                return false;
            }
            return t.Id != 0;
        }

        public bool TileMapCollision(Vector2 position)
        {
            return TileMapCollision((int)(position.X), (int)(position.Y));
        }

        public int TileHeight
        {
            get
            {
                return TiledMap.TileHeight;
            }
        }
        public int TileWidth
        {
            get
            {
                return TiledMap.TileWidth;
            }
        }
        public int TileMapHeight
        {
            get
            {
                return TiledMap.Height;
            }
        }
        public int TileMapWidth
        {
            get
            {
                return TiledMap.Width;
            }
        }

        private Vector2 size;

        public Vector2 Size
        {
            get
            {
                return size;
            }
        }
    }
}
