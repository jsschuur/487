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
    class Wave4 : Wave
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            endTimeDelay = 2000;
            nextWave = GameManager.GetPrefab("GameOver");

            var spawnArray = new[]
            {
                new Spawn("FinalBoss", new Vector2(232, 0), 0),
            };

            foreach (var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}

