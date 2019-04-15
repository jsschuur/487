using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Take3.GameManagement;

namespace Take3.ECS.Collision
{
    public class CollisionManager
    {
        private List<Tuple<string, string>> _layers;

        public CollisionManager()
        {
            _layers = new List<Tuple<string, string>>();
        }

        public void AddLayer(string tag1, string tag2)
        {
            _layers.Add(new Tuple<string, string>(tag1, tag2));
        }

        public void CheckCollisions()
        {
            foreach(var layer in _layers)
            {
                var layer1 = GameManager.GetObjectsByTag(layer.Item1);
                var layer2 = GameManager.GetObjectsByTag(layer.Item2);

                foreach(var layer1obj in layer1)
                {
                    foreach(var layer2obj in layer2)
                    {
                        var collider1 = (Collider)layer1obj.GetComponent<Collider>();
                        var collider2 = (Collider)layer2obj.GetComponent<Collider>();

                        if(collider1 != null && collider2 != null)
                        {
                            if(CollisionMath.CheckCollision(collider1, collider2))
                            {
                                collider1.Collide(layer2obj);
                                collider2.Collide(layer1obj);
                            }
                        }
                    }
                }
            }
        }
    }
}
