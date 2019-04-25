using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;
using Take3.LevelManagement;

namespace Take3.ECS.Scripts 
{
    class Wave2 : Wave
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            endTimeDelay = 2000;

            nextWave = GameManager.GetPrefab("Wave3");

            var spawnArray = new[]
            {
                new Spawn("Midboss", new Vector2(296, -128), 0),
            };
        
            foreach(var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}
