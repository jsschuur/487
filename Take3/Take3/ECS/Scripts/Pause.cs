using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.Utility;

namespace Take3.ECS.Scripts
{
    class Pause : Script
    {
        private GameObject quitButton;
        private GameObject resumeButton;
        private GameObject mainMenuButton;

        private List<GameObject> activeObjects;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            activeObjects = new List<GameObject>();
            Configs.SetMouseVisible();
            foreach(var obj in GameManager.GetGameObjects().Where(obj => (obj.IsActive)))
            {
                obj.IsActive = false;
                activeObjects.Add(obj);
            }

            quitButton = GameManager.Instantiate(GameManager.GetPrefab("QuitButton"));
            resumeButton = GameManager.Instantiate(GameManager.GetPrefab("ResumeButton"));
            mainMenuButton = GameManager.Instantiate(GameManager.GetPrefab("MainMenuButton"));
            var resumeButtonInstance = (ResumeButton)resumeButton.GetComponent<ResumeButton>();
            resumeButtonInstance.Resume = Resume;
        }

        public void Resume()
        {
            foreach(var obj in activeObjects)
            {
                obj.IsActive = true;
            }

            foreach(var obj in GameManager.GetObjectsByTag("Pause"))
            {
                obj.Die();
            }

            GameManager.UnPause();
        }
    }
}
