using _487Game.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Level
{
    class Spawn
    {
        private Entity _entity;
        private float _spawnTime;
        
        public Entity Entity { get { return _entity; } }
        public float SpawnTime { get { return _spawnTime; } }

        public Spawn() { }

        public Spawn(Entity entity, Vector2 origin, float spawnTime)
        {
            _entity = entity;
            ((PositionComponent)_entity.GetComponent("PositionComponent")).SetPosition(origin);
            _spawnTime = spawnTime;
        }

        public Spawn(Entity entity, float spawnTime)
        {
            _entity = entity;
            _spawnTime = spawnTime;
        }
    }
}
