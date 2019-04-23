using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public class GameObject 
    {
        private IList<Component> components;

        private bool isAlive;
        private bool isActive;


        public string Tag { get; set; }
        
        public bool IsAlive { get { return isAlive; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }

        public double disableTime;

        private GameTime objectTime;

        public GameObject()
        {
            components = new List<Component>();
            AddComponent<Transform>();
            objectTime = new GameTime();
            isAlive = isActive = true;
        }

        public virtual void Die()
        {
            isAlive = false;
        }

        public Component AddComponent<T>() where T : Component, new()
        {
            Component c = (T)Activator.CreateInstance(typeof(T));
            c.Initialize(this);
            components.Add(c);
            return c;
        }

        public void AddComponent(Component c)
        {
            c.Initialize(this);
            components.Add(c);
        }

        public Component GetComponent<T>() where T : Component
        {
            return components.OfType<T>().First(); 
        }

        public bool HasComponent<T>() where T : Component
        {
            return components.OfType<T>().Any();
        }

        public List<Component> GetComponents<T>() where T : Component
        {
            return components.OfType<T>().ToList<Component>();
        }

        public void SetDisableTimer(double milliseconds)
        {
            disableTime = milliseconds;
            isActive = false;
        }

        public void Update(GameTime gameTime)
        {
            if(isActive)
            {
                objectTime.TotalGameTime += gameTime.ElapsedGameTime;
                objectTime.ElapsedGameTime = gameTime.ElapsedGameTime;

                foreach (var behavior in GetComponents<Updatable>())
                {
                    if (behavior.IsActive)
                    {
                        ((Updatable)behavior).Update(objectTime);
                    }
                }
            }
            if(disableTime > 0)
            {
                disableTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            else if(disableTime < 0)
            {
                disableTime = 0;
                isActive = true;
            }
        }
    }
}
