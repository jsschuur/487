using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3
{
    public sealed class TextureManager
    {
        private static readonly TextureManager textureManager = new TextureManager();
        private const string TextureNotFound = "nulltexture";
        private const string FontNotFound = "fontnotfound";

        private static Dictionary<string, Texture2D> loadedTextures;
        private static Dictionary<string, SpriteFont> loadedFonts;

        private static ContentManager contentManager;


        static TextureManager() { }
        private TextureManager()
        {
            loadedTextures = new Dictionary<string, Texture2D>();
            loadedFonts = new Dictionary<string, SpriteFont>();
        }

        public static void Init(ContentManager content)
        {
            contentManager = content;
        }

        public static Texture2D LoadTexture(string path)
        {
            if (loadedTextures.ContainsKey(path))
            {
                return loadedTextures[path];
            }

            try
            {
                var texture = contentManager.Load<Texture2D>(Path.Combine("Textures", path));
                loadedTextures.Add(path, texture);
                return texture;
            }
            catch (Exception) when (path != TextureNotFound)
            {
                return LoadTexture(TextureNotFound);
            }
        }

        public static SpriteFont LoadFont(string path)
        {
            if (loadedFonts.ContainsKey(path))
            {
                return loadedFonts[path];
            }
            try
            {
                var font = contentManager.Load<SpriteFont>(Path.Combine("Fonts", path));
                loadedFonts.Add(path, font);
                return font;
            }
            catch (Exception) when (path != FontNotFound)
            {
                return LoadFont(FontNotFound);
            }
        }
    }
}
