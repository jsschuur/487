using Microsoft.Xna.Framework;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class GalleryButton : Button
    {
        public delegate void SwitchDisplay(Prefabrication prefab);

        private Prefabrication galleryButtonIcon;
 
        public SwitchDisplay Switch;

        private float size = 64f;

        public void SetObject(Prefabrication prefab)
        {

            var transform = (Transform)GetComponent<Transform>();
            var renderer = (SpriteRenderer)GetComponent<SpriteRenderer>();

            var center = transform.Position + renderer.Sprite.GetCenter();

            var prefabRenderer = (SpriteRenderer)prefab.GetComponent<SpriteRenderer>();

            galleryButtonIcon = new Prefabrication();
            galleryButtonIcon.AddComponent(prefabRenderer);

            var xOffset = (renderer.Sprite.SpriteRectangle.Width - size) / 2;
            var yOffset = (renderer.Sprite.SpriteRectangle.Height - size) / 2;

            var offset = new Vector2(xOffset, yOffset);

            var buttonIcon = GameManager.Instantiate(galleryButtonIcon, State.Gallery);
            buttonIcon.Tag = "GallerySprite";
            var buttonIconRenderer = (SpriteRenderer)buttonIcon.GetComponent<SpriteRenderer>();

            buttonIconRenderer.Sprite.LayerDepth = 0.2f;
            var scale = (buttonIconRenderer.Sprite.SpriteRectangle.Width > buttonIconRenderer.Sprite.SpriteRectangle.Height) ?
                         size / buttonIconRenderer.Sprite.SpriteRectangle.Width :
                         size / buttonIconRenderer.Sprite.SpriteRectangle.Height;

            if (scale < buttonIconRenderer.Sprite.Scale) buttonIconRenderer.Sprite.Scale = scale;

            var buttonIconTransform = (Transform)buttonIcon.GetComponent<Transform>();

            buttonIconTransform.Position = center - buttonIconRenderer.Sprite.GetCenter();
        }

        public override void OnClick()
        {
            Switch(galleryButtonIcon);
        }
    }
}
