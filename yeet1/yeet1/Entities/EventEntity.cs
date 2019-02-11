using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Utility;

namespace yeet1.Entities
{
    abstract class EventEntity : Entity
    {
        protected event EventHandler<EntityEventArgs> _event;

        public EventEntity(Texture2D texture, Vector2 origin, EntityManager entityManager) : base(texture, origin)
        {
            _event += entityManager.EntityEventHandler;
        }

        public void SendEvent(object sender, EntityEventArgs e)
        {
            _event?.Invoke(sender, e);
        }
    }
}
