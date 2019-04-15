﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;
using Take3.LevelManagement;

namespace Take3.ECS.Scripts
{
    public class Wave : Script
    {
        public bool IsActive { get; set; }
        private Queue<Spawn> spawns;

        public override void Initialize(GameObject owner)
        {
            base.Initialize(owner);
            spawns = new Queue<Spawn>();
            IsActive = true;
        }

        public void AddSpawn(Spawn spawn)
        {
            spawns.Enqueue(spawn);
        }

        public override void Update(GameTime gameTime)
        {
            if(IsActive)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds >= spawns.Peek().SpawnTime)
                {
                    var currentSpawn = spawns.Dequeue();

                    GameManager.Instantiate(GameManager.GetPrefab(currentSpawn.Name), currentSpawn.Origin);

                    if(spawns.Count == 0)
                    {
                        IsActive = false;
                    }
                }
            }
        }
    }
}