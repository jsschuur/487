using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.LevelManagement;

namespace Take3.ECS.Scripts
{
    class Wave1 : Wave
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            var spawnArray = new[]
            {
                new Spawn("BlackCircle", new Vector2(500, 100), 0),
                new Spawn("BlackCircle", new Vector2(300, 100), 0),
                new Spawn("BlackCircle", new Vector2(400, 100), 0),
                new Spawn("FlipGameScreen", new Vector2(400, 100), 1000),
            };

            foreach(var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}
