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
                //new Spawn("PurpleDiamondProjectilePowerUp", new Vector2(340, 200), 0),
 

                new Spawn("RedCircle", new Vector2(450, 175), 0),
                new Spawn("RedCircle", new Vector2(210, 175), 0),
                new Spawn("RedCircle", new Vector2(540, 230), 5000),
                new Spawn("RedCircle", new Vector2(120, 230), 5000),

                //new Spawn("BlackCircle", new Vector2(330, 50), 10000),
                //new Spawn("BlackCircle", new Vector2(530, 150), 10000),
                //new Spawn("BlackCircle", new Vector2(130, 150), 10000),
                //new Spawn("BlackCircle", new Vector2(400, 200), 15000),
                //new Spawn("BlackCircle", new Vector2(260, 200), 15000),

                //new Spawn("GreenSquare", new Vector2(130, 100), 20000),
                //new Spawn("GreenSquare", new Vector2(230, 100), 20000),
                //new Spawn("GreenSquare", new Vector2(430, 100), 20000),
                //new Spawn("GreenSquare", new Vector2(530, 100), 20000),
                //new Spawn("GreenSquare", new Vector2(60, 200), 25000),
                //new Spawn("GreenSquare", new Vector2(60, 300), 25000),
                //new Spawn("GreenSquare", new Vector2(600, 200), 25000),
                //new Spawn("GreenSquare", new Vector2(600, 300), 25000),

                //new Spawn("BlueDiamond", new Vector2(330, 150), 30000),
                //new Spawn("BlueDiamond", new Vector2(230, 120), 30000),
                //new Spawn("BlueDiamond", new Vector2(430, 120), 30000),
                //new Spawn("BlueDiamond", new Vector2(50, 50), 30000),
                //new Spawn("BlueDiamond", new Vector2(610, 50), 30000),

                //new Spawn("YellowHexagon", new Vector2(330, 60), 40000),
                //new Spawn("YellowHexagon", new Vector2(400, 75), 40200),
                //new Spawn("YellowHexagon", new Vector2(260, 75), 40200),
                //new Spawn("YellowHexagon", new Vector2(470, 90), 40400),
                //new Spawn("YellowHexagon", new Vector2(190, 90), 40400),
            };

            foreach (var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }
    }
}
