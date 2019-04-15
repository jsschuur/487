using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.ECS;

namespace Take3.LevelManagement
{
    public class Spawn
    {
        public string Name { get; set; }
        public Vector2 Origin { get; set; }
        public float SpawnTime { get; set; }

        public Spawn(string name, Vector2 origin, float spawnTime)
        {
            Name = name;
            Origin = origin;
            SpawnTime = spawnTime;
        }
    }
}
