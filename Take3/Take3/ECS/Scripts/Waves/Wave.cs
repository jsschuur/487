using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;
using Take3.LevelManagement;

namespace Take3.ECS.Scripts
{
    public class Wave : Script
    {
        protected Queue<Spawn> spawns = new Queue<Spawn>();

        private double endTime;

        protected float endTimeDelay;
        protected Prefabrication nextWave;

        private bool isCurrentWave;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            isCurrentWave = true;
        }

        public void AddSpawn(Spawn spawn)
        {
            spawns.Enqueue(spawn);
        }

        public override void Update(GameTime gameTime)
        {
            if(isCurrentWave)
            {
                if (spawns.Count == 0)
                {
                    if(!GameManager.GetObjectsByTag("Enemy").Any())
                    {
                        isCurrentWave = false;
                        endTime = gameTime.TotalGameTime.TotalMilliseconds;
                    }
                }
                else if (gameTime.TotalGameTime.TotalMilliseconds >= spawns.Peek()?.SpawnTime)
                {
                    var currentSpawn = spawns.Dequeue();
                    var obj = GameManager.Instantiate(GameManager.GetPrefab(currentSpawn.Name), currentSpawn.Origin);
                }
            }
            else
            {
                if(gameTime.TotalGameTime.TotalMilliseconds >= endTime + endTimeDelay)
                {
                    Die();
                    if(nextWave != null)
                    {
                        GameManager.Instantiate(nextWave);
                    }
                    else
                    {
                        //end the game
                    }
                }
            }
        }
    }
}
