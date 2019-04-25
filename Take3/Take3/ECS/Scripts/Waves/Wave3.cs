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
    class Wave3 : Wave
    {

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);

            endTimeDelay = 2000;
            nextWave = GameManager.GetPrefab("Wave4");

            var spawnArray = new[]
            {

        

                new Spawn("RedCircle", new Vector2(330, 70), 0),

                new Spawn("YellowHexagon", new Vector2(240, 150), 2000),
                new Spawn("YellowHexagon", new Vector2(420, 150), 2000),


                new Spawn("BlackCircle", new Vector2(60, 100), 16500),
                new Spawn("BlackCircle", new Vector2(600, 100), 16500),

                new Spawn("GreenSquare", new Vector2(160, 100), 17000),
                new Spawn("GreenSquare", new Vector2(520, 100), 17000),


                new Spawn("BlueDiamond", new Vector2(580, 220), 35400),
                new Spawn("BlueDiamond", new Vector2(80, 220), 35400),

                new Spawn("GreenSquare", new Vector2(280, 80), 35600),
                new Spawn("GreenSquare", new Vector2(380, 80), 35600),


            };

            foreach (var spawn in spawnArray)
            {
                spawns.Enqueue(spawn);
            }
        }

    }       
}