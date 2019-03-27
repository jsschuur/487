using _487Game.Entities;
using _487Game.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _487Game.Components
{
    class ProjectileComponent : Component
    {
        private event EventHandler<ProjectileEventArgs> _fire;

        public ProjectileComponent(Entity owner, EntityManager entityManager) : base(owner)
        {
            _componentType = "ProjectileComponent";
            _fire += entityManager.ProjectileEventHandler;
        }

        public void Fire(ProjectileInformation projectileInformation, Vector2 direction)
        {
            _fire?.Invoke(this, new ProjectileEventArgs(projectileInformation, direction));
        }

        public override void Update(GameTime gameTime) { }
    }
}
