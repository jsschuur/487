using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yeet1.Texture
{
    class TextureManager
    {
        private const string TextureNotFound = "texturenotfound";
        private const string FontNotFound = "fontnotfound";

        private Dictionary<string, Texture2D> _loadedTextures;
        private Dictionary<string, SpriteFont> _loadedFonts;

        private ContentManager _contentManager;

        public TextureManager(ContentManager contentManager)
        {
            this._contentManager = contentManager;
            this._loadedTextures = new Dictionary<string, Texture2D>();
            this._loadedFonts = new Dictionary<string, SpriteFont>();
        }

        public Texture2D LoadTexture(string path)
        {
            if (_loadedTextures.ContainsKey(path))
            {
                return _loadedTextures[path];
            }

            try
            {
                var texture = _contentManager.Load<Texture2D>(Path.Combine("Textures", path));
                _loadedTextures.Add(path, texture);
                return texture;
            }
            catch (Exception) when (path != TextureNotFound)
            {
                return LoadTexture(TextureNotFound);
            }
        }

        public SpriteFont LoadFont(string path)
        {
            if (_loadedFonts.ContainsKey(path))
            {
                return _loadedFonts[path];
            }

            try
            {
                var font = _contentManager.Load<SpriteFont>(Path.Combine("Fonts", path));
                _loadedFonts.Add(path, font);
                return font;
            }
            catch (Exception) when (path != FontNotFound)
            {
                return LoadFont(FontNotFound);
            }
        }
    }
}
