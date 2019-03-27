using _487Game.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Level
{
    class Wave
    {
        private EntityManager _entityManager;

        private List<Spawn> _spawns;
        private List<Spawn> _toBeRemoved;

        private float _startTime;
        private float _endTime;

        public Wave(EntityManager entityManager, float startTime, float endTime)
        {
            _spawns = new List<Spawn>();
            _toBeRemoved = new List<Spawn>();
            _entityManager = entityManager;
            _startTime = startTime;
            _endTime = endTime;
        }

        public void AddSpawn(Spawn spawn)
        {
            _spawns.Add(spawn);
        }

        public void Update(GameTime gameTime)
        {
            foreach(Spawn s in _spawns)
            {
                if(gameTime.TotalGameTime.TotalSeconds >= s.SpawnTime + _startTime)
                {
                    _entityManager.AddEntity(s.Entity);
                    _toBeRemoved.Add(s);
                }
            }

            foreach(Spawn s in _toBeRemoved)
            {
                _spawns.Remove(s);
            }
        }
    }
}
