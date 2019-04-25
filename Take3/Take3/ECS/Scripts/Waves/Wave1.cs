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
    class Wave1 : Wave
    {
        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            endTimeDelay = 2000;
            nextWave = GameManager.GetPrefab("Wave2");

            var spawnArray = new[]
            {

                new Spawn("RedCircle", new Vector2(450, 175), 0),
                new Spawn("RedCircle", new Vector2(210, 175), 0),
                new Spawn("RedCircle", new Vector2(330, 230), 5000),

                new Spawn("BlackCircle", new Vector2(530, 150), 10000),
                new Spawn("BlackCircle", new Vector2(130, 150), 10000),
                new Spawn("BlackCircle", new Vector2(330, 200), 15000),

                new Spawn("GreenSquare", new Vector2(230, 100), 22000),
                new Spawn("GreenSquare", new Vector2(330, 100), 22000),
                new Spawn("GreenSquare", new Vector2(430, 100), 22000),

                new Spawn("BlueDiamond", new Vector2(330, 150), 30000),
                new Spawn("BlueDiamond", new Vector2(230, 120), 30000),
                new Spawn("BlueDiamond", new Vector2(430, 120), 30000),

                new Spawn("YellowHexagon", new Vector2(330, 60), 40000),
                new Spawn("YellowHexagon", new Vector2(400, 75), 40200),
                new Spawn("YellowHexagon", new Vector2(260, 75), 40200),

            };

            foreach (var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}
