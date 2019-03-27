using _487Game.Entities;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Texture;

namespace _487Game.Level
{
    static class WaveLoader
    {
        public static Wave LoadWave(string path, EntityManager entityManager, TextureManager textureManager)
        {
            Wave w = new Wave(entityManager, 0, 20);
            Dictionary<string, EntityBuilder> _loadedEntities = new Dictionary<string, EntityBuilder>();

            StreamReader sr = new StreamReader(path);
            string json = sr.ReadToEnd();

            dynamic data = JsonConvert.DeserializeObject(json);

            foreach (dynamic enemies in data.Enemies)
            {
                EntityBuilder eb = new EntityBuilder();

                foreach (dynamic enemy in enemies)
                {
                    if (enemy.Name == "ID")
                    {
                        _loadedEntities.Add(enemy.Value.Value, eb);
                    }
                    else
                    {
                        eb.AddComponentArgs(enemy.Name, enemy.Value);
                    }
                }
            }

            foreach (dynamic x in data.Spawns)
            {
                Entity e = _loadedEntities[x.Enemy.Value].BuildEntity("enemy", textureManager, entityManager);
                w.AddSpawn(new Spawn(e, new Vector2((float)x.X.Value, (float)x.Y.Value), (float)x.Time));
            }
            return w;
        }
    }
}
