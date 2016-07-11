using MonoGame.Extended.Maps.Tiled;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace Game
{
    class AssetLoader
    {
        public static TiledMap LoadMap(ContentManager content, string file_name)
        {
            file_name = string.Format("TileMaps/{0}", file_name);
            TiledMap tiled_map = content.Load<TiledMap>(file_name);
            return tiled_map;
        }

        public static Texture2D LoadSprite(ContentManager content, string file_name)
        {
            file_name = string.Format("Images/{0}", file_name);
            Texture2D texture = content.Load<Texture2D>(file_name);
            return texture;
        }

        public static Texture2D[] LoadEntitySprites(ContentManager content, string file_name)
        {
            Texture2D [] to_return = new Texture2D[4];
            string path_name = string.Format("Images/{0}", file_name);
            string[] files = { "Right", "Left", "Up", "Down" };
            for (int i = 0; i < to_return.Length; i++)
            {
                string file_path_name = path_name + "/" + files[i];
                Texture2D texture = content.Load<Texture2D>(file_path_name);
                to_return[i] = texture;
            }
            return to_return;
        }

        public static BitmapFont LoadFont(ContentManager content, string file_name)
        {
            file_name = string.Format("Fonts/{0}", file_name);
            BitmapFont font = content.Load<BitmapFont>(file_name);
            return font;
        }
    }
}
