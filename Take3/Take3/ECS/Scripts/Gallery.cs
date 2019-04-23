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
        private GameObject currentOnDisplay;

        private Vector2 displayWindow;
        private float size;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            displayWindow = new Vector2(942, 232);
            size = 256f;

            var galleryButtonPrefab = GameManager.GetPrefab("GalleryButton");
            var galleryButtonRenderer = (SpriteRenderer)galleryButtonPrefab.GetComponent<SpriteRenderer>();

            var width = galleryButtonRenderer.Sprite.SpriteRectangle.Width;
            var height = galleryButtonRenderer.Sprite.SpriteRectangle.Height;

            var padding = width / 4f;

            var index = 0;

            var galleryList = new List<Prefabrication>();


            galleryList.Add(GameManager.GetPrefab("Player"));
            foreach(var prefab in GameManager.GetPrefabsByTag("Enemy"))
            {
                galleryList.Add(prefab);
            }
            foreach (var prefab in GameManager.GetPrefabsByTag("Projectile"))
            {
                galleryList.Add(prefab);
            }

            foreach (var prefab in galleryList)
            {
                var xPos = index % 4 + 1;
                var yPos = index / 4 + 1;

                var xOffset = xPos * (padding + width);
                var yOffset = yPos * (padding + height);

                var newGalleryButton = GameManager.Instantiate(galleryButtonPrefab, new Vector2(xOffset, yOffset), State.Gallery);
                var galleryButtonInstance = (GalleryButton)newGalleryButton.GetComponent<GalleryButton>();
                galleryButtonInstance.SetObject(prefab);
                galleryButtonInstance.Switch = SwitchCurrentDisplay;

                index++;
            }
        }

        public void SwitchCurrentDisplay(Prefabrication prefab)
        {

            if (currentOnDisplay != null) currentOnDisplay.Die();
            currentOnDisplay = GameManager.Instantiate(prefab, displayWindow);
            var currentOnDisplayRenderer = (SpriteRenderer)currentOnDisplay.GetComponent<SpriteRenderer>();

            currentOnDisplayRenderer.Sprite.LayerDepth = 0.2f;

            var scale = (currentOnDisplayRenderer.Sprite.SpriteRectangle.Width > currentOnDisplayRenderer.Sprite.SpriteRectangle.Height) ? 
                         size / currentOnDisplayRenderer.Sprite.SpriteRectangle.Width : 
                         size / currentOnDisplayRenderer.Sprite.SpriteRectangle.Height;

            var windowCenter = new Vector2(displayWindow.X + size / 2, displayWindow.Y + size / 2);
            var currentOnDisplayTransform = (Transform)currentOnDisplay.GetComponent<Transform>();

            currentOnDisplayTransform.Position = windowCenter - currentOnDisplayRenderer.Sprite.GetCenter();
        }
    }
}
