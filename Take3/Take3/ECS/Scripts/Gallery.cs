using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class Gallery : Script
    {
        private const int scale = 64;
        private const int buttonPadding = 10;
        private Vector2 buttonOrigin = new Vector2(32f, 32f);


        private GameObject currentObject;

        public override void Initialize(GameObject owner)
        {
            var index = 0;

            foreach(var obj in GameManager.GameObjects)
            {
                var newObject = new GameObject();

                var newObjectTransform = (Transform)newObject.GetComponent<Transform>();
                var newGalleryButton = (GallaryButton)newObject.AddComponent<GallaryButton>();
                var newObjectRenderer = (Renderer)newObject.AddComponent<Renderer>();

                var newPositionX = (index % 6);
                var newPositionY = (index / 6);

                newObjectTransform.Position = new Vector2(newPositionX * buttonPadding + newPositionX * )

                var objSprite = ((Renderer)obj.GetComponent<Renderer>()).Sprite;
                var newScale = (objSprite.SpriteRectangle.Width > objSprite.SpriteRectangle.Height) ? scale / objSprite.SpriteRectangle.Width : scale / objSprite.SpriteRectangle.Height;
                var newSprite = new Sprite(objSprite.Texture, objSprite.SpriteRectangle, newScale, false, 0.4f);

                index++;
            }
        }
    }
}
