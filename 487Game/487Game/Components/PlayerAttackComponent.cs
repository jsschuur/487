using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _487Game.Utility;
using Microsoft.Xna.Framework;

namespace _487Game.Components
{
    class PlayerAttackComponent : Component
    {
        private ProjectileInformation _playerProjectileInformation;

        private Vector2 _direction;

        private double _cooldown;
        private double _lastTimeFired;

        private bool _offCooldown;

        public PlayerAttackComponent(Entity owner, float cooldown, ProjectileInformation projectileInformation) : base(owner)
        {
            _componentType = "PlayerAttackComponent";
            _playerProjectileInformation = projectileInformation;
            _cooldown = cooldown;
            _offCooldown = true;
            _direction = new Vector2(0, -1);
        }


        public void Fire(GameTime gameTime)
        {
            if(_offCooldown)
            {
                ((ProjectileComponent)_owner.GetComponent("ProjectileComponent")).Fire(_playerProjectileInformation, _direction);
                _lastTimeFired = gameTime.TotalGameTime.TotalMilliseconds;
                _offCooldown = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalMilliseconds > _cooldown + _lastTimeFired)
            {
                _offCooldown = true;
            }
        }
    }
}
