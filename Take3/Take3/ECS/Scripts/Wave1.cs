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
                new Spawn("Player", new Vector2(300, 300), 0),
                 new Spawn("PurpleDiamondProjectilePowerUp", new Vector2(200, 200), 200),
                new Spawn("BlueDiamond", new Vector2(200, 200), 1000),

            };

            foreach(var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}
