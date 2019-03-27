using _487Game.Collision;
using _487Game.Components.EnemyBehaviorComponents;
using _487Game.EnemyBehavior;
using _487Game.Entities;
using _487Game.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yeet1.Texture;

namespace _487Game.Components
{
    class ComponentFactory
    {
        private EntityManager _entityManager;
        private TextureManager _textureManager;

        public ComponentFactory(EntityManager entityManager, TextureManager textureManager)
        {
            _entityManager = entityManager;
            _textureManager = textureManager;
        }

        
    }
}
