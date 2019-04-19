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
                GameObject newObject = new GameObject();
                GameManager.AddPrefabrication(obj.Name, newObject);

                foreach(dynamic args in obj.Value)
                {
                    AddJSONArgs(newObject, args);
                }
            }
        }

        private static void AddJSONArgs(GameObject newObject, dynamic args)
        {
            switch(args.Name)
            {
                case "Tag":
                    newObject.Tag = (string)args.Value; break;
                case "Scale":
                    SetScale(newObject, args.Value); break;
                case "Sprite":
                    AddSpriteArgs(newObject, args.Value); break;
                case "Velocity":
                    AddVelocityArgs(newObject, args.Value); break;
                case "Animations":
                    AddAnimationArgs(newObject, args.Value); break;
                case "Scripts":
                    AddScripts(newObject, args.Value); break;
                case "Position":
                    ChangePosition(newObject, args.Value); break;
                case "Collision":
                    AddCollisionArgs(newObject, args.Value); break;
                case "Spawns":
                    AddWave(newObject, args.Value); break;
            }
        }

        private static void AddVelocityArgs(GameObject newObject, dynamic args)
        {
            var newVelocity = (Velocity)newObject.AddComponent<Velocity>();
            newVelocity.Speed = args.Speed;
            newVelocity.Acceleration = args.Acceleration;
        }

        private static void AddWave(GameObject newObject, dynamic args)
        {
            var wave = (Wave)newObject.AddComponent<Wave>();
          
            foreach(var spawn in args)
            {
                Spawn newSpawn = new Spawn((string)spawn.Name, new Vector2((float)spawn.X, (float)spawn.Y), (float)spawn.Time);
                wave.AddSpawn(newSpawn);
            }
        }

        private static void SetScale(GameObject newObject, dynamic args)
        {
            var transform = (Transform)newObject.GetComponent<Transform>();
            transform.Scale = (float)args.Value;
        }

        private static void ChangePosition(GameObject newObject, dynamic args)
        {
            var transform = (Transform)newObject.GetComponent<Transform>();
            transform.Position = new Vector2((float)args.X, (float)args.Y);
        }

        private static void AddSpriteArgs(GameObject newObject, dynamic args)
        {
            var newRenderer = (Renderer)newObject.AddComponent<Renderer>();
            newRenderer.Sprite = new Sprite(TextureManager.LoadTexture((string)args.Path), 
                                            new Rectangle(0, 0, (int)args.Width, (int)args.Height), 
                                            (float)args.Scale, (bool)args.Rotatable, (float)args.Depth);
        }

        private static void AddAnimationArgs(GameObject newObject, dynamic args)
        {
            var animator = (Animator)newObject.AddComponent<Animator>();
            foreach(dynamic animation in args)
            {
                animator.AddAnimation((string)animation.Name, 
                                      new Animation((int)animation.NumFrames, (float)animation.Delay, (int)animation.YOffset));   
            }
        }

        private static void AddScripts(GameObject newObject, dynamic args)
        {
            foreach(dynamic script in args)
            {
                Type newScript = Type.GetType("Take3.ECS.Scripts." + (string)script);
                if(newScript != null)
                {
                    newObject.AddComponent((Component)Activator.CreateInstance(newScript));
                }
            }
        }

        public static void AddCollisionArgs(GameObject newObject, dynamic args)
        {
            if((string)args.Shape == "Circle")
            {
                var collider = (Collider)newObject.AddComponent<CircleCollider>();
                collider.Buffer = (float)args.Buffer;
            }
            else
            {
                var collider = (Collider)newObject.AddComponent<BoxCollider>();
                collider.Buffer = (float)args.Buffer;
            }
        }
    }
}
