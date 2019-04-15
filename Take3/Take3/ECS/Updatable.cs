using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Take3.ECS
{
    public abstract class Updatable : Component
    {
        public virtual void Update(GameTime gameTime) { }
    }
}
