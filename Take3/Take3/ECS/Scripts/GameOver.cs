using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.Utility;

namespace Take3.ECS.Scripts
{
    class GameOver : Script
    {
        private GameObject quitButton;
        private GameObject mainMenuButton;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            Configs.SetMouseVisible();
            foreach (var obj in GameManager.GetGameObjects().Where(obj => (obj.IsActive)))
            {
                obj.IsActive = false;
            }

            foreach (var obj in GameManager.GetObjectsByTag("ScreenComponent")) obj.IsActive = true;

            quitButton = GameManager.Instantiate(GameManager.GetPrefab("QuitButton"));
            mainMenuButton = GameManager.Instantiate(GameManager.GetPrefab("MainMenuButton"));
   
        }
    }
}
