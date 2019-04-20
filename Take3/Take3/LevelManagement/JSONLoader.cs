using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.ECS;
using Take3.ECS.Scripts;
using Take3.GameManagement;

namespace Take3.LevelManagement
{
    public sealed class JSONLoader
    {
        private static readonly JSONLoader loader = new JSONLoader();

        static JSONLoader() { }
        private JSONLoader()
        {

        }

        public static void LoadJSONObjects(string path)
        {
            var json = new StreamReader(path).ReadToEnd();

            dynamic data = JsonConvert.DeserializeObject(json);

            foreach(dynamic obj in data)
            {
                Prefabrication prefab = new Prefabrication();
                GameManager.AddPrefabrication(obj.Name, prefab);

                foreach(dynamic args in obj.Value)
                {
                    AddJSONArgs(prefab, args);
                }
            }
        }

        public static GameObject LoadJSONWave(string path)
        {
            GameObject obj = new GameObject();
            var wave = (Wave)obj.AddComponent<Wave>();

            var json = new StreamReader(path).ReadToEnd();

            dynamic data = JsonConvert.DeserializeObject(json);

            foreach (dynamic spawn in data.Spawns)
            {
                wave.AddSpawn(new Spawn((string)(spawn.Name),
                                        new Vector2((float)spawn.X, (float)spawn.Y), 
                                        (float)spawn.Time));
            }

            return obj;
        }

        private static void AddJSONArgs(Prefabrication prefab, dynamic args)
        {
            switch(args.Name)
            {
                case "Tag":
                    prefab.Tag = (string)args.Value; break;
                case "Scale":
                    SetScale(prefab, args.Value); break;
                case "Sprite":
                    AddSpriteArgs(prefab, args.Value); break;
                case "Velocity":
                    AddVelocityArgs(prefab, args.Value); break;
                case "Animations":
                    AddAnimationArgs(prefab, args.Value); break;
                case "Scripts":
                    AddScripts(prefab, args.Value); break;
                case "Position":
                    ChangePosition(prefab, args.Value); break;
                case "Collision":
                    AddCollisionArgs(prefab, args.Value); break;
                case "Spawns":
                    AddWave(prefab, args.Value); break;
            }
        }

        private static void AddVelocityArgs(Prefabrication prefab, dynamic args)
        {
            var newVelocity = (Velocity)prefab.AddComponent<Velocity>();
            newVelocity.Speed = args.Speed;
            newVelocity.Acceleration = args.Acceleration;
        }

        private static void AddWave(Prefabrication prefab, dynamic args)
        {
            var wave = (Wave)prefab.AddComponent<Wave>();
          
            foreach(var spawn in args)
            {
                Spawn newSpawn = new Spawn((string)spawn.Name, new Vector2((float)spawn.X, (float)spawn.Y), (float)spawn.Time);
                wave.AddSpawn(newSpawn);
            }
        }

        private static void SetScale(Prefabrication prefab, dynamic args)
        {
            var transform = (Transform)prefab.GetComponent<Transform>();
            transform.Scale = (float)args.Value;
        }

        private static void ChangePosition(Prefabrication prefab, dynamic args)
        {
            var transform = (Transform)prefab.GetComponent<Transform>();
            transform.Position = new Vector2((float)args.X, (float)args.Y);
        }

        private static void AddSpriteArgs(Prefabrication prefab, dynamic args)
        {
            var newRenderer = (Renderer)prefab.AddComponent<Renderer>();
            newRenderer.Sprite = new Sprite(TextureManager.LoadTexture((string)args.Path), 
                                            new Rectangle(0, 0, (int)args.Width, (int)args.Height), 
                                            (float)args.Scale, (bool)args.Rotatable, (float)args.Depth);
        }

        private static void AddAnimationArgs(Prefabrication prefab, dynamic args)
        {
            var animator = (Animator)prefab.AddComponent<Animator>();
            foreach(dynamic animation in args)
            {
                animator.AddAnimation((string)animation.Name, 
                                      new Animation((int)animation.NumFrames, (float)animation.Delay, (int)animation.YOffset));   
            }
        }

        private static void AddScripts(Prefabrication prefab, dynamic args)
        {
            foreach(dynamic script in args)
            {
                Type newScript = Type.GetType("Take3.ECS.Scripts." + (string)script);
                if(newScript != null)
                {
                    prefab.AddComponent((Component)Activator.CreateInstance(newScript));
                }
            }
        }

        public static void AddCollisionArgs(Prefabrication prefab, dynamic args)
        {
            if((string)args.Shape == "Circle")
            {
                var collider = (Collider)prefab.AddComponent<CircleCollider>();
                collider.Buffer = (float)args.Buffer;
            }
            else
            {
                var collider = (Collider)prefab.AddComponent<BoxCollider>();
                collider.Buffer = (float)args.Buffer;
            }
        }
    }
}
