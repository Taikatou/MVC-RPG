using Game.Engine;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Maps.Tiled;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Common
{
    public class Current
    {
        protected static TiledMap tiled_map;
        public static TiledMap TiledMap
        {
            get
            {
                if (tiled_map == null)
                {
                    WorldScene world = SceneManager.CurrentWorld;
                    tiled_map = AssetLoader.LoadMap(Game.Instance.renderer.content, world.TileMap.FileName);
                    size = new Vector2(TileWidth, TileHeight); ;
                }
                return tiled_map;
            }
        }

        public static TiledTileLayer TileCollisionLayer
        {
            get
            {
                return TiledMap.GetLayer<TiledTileLayer>("Collision Layer");
            }
        }

        public static bool TileMapCollision(Vector2 position)
        {
            TiledTile t = TileCollisionLayer.GetTile((int)(position.X), (int)(position.Y));
            if(t == null)
            {
                return false;
            }
            return t.Id != 0;
        }

        public static void ResetTileMap()
        {
            tiled_map = null;
        }

        public static float TileHeight
        {
            get
            {
                return TiledMap.TileHeight;
            }
        }
        public static float TileWidth
        {
            get
            {
                return TiledMap.TileWidth;
            }
        }
        public static int TileMapHeight
        {
            get
            {
                return TiledMap.Height;
            }
        }
        public static int TileMapWidth
        {
            get
            {
                return TiledMap.Width;
            }
        }

        private static Vector2 size;

        public static Vector2 Size
        {
            get
            {
                return size;
            }
        }
    }
}
