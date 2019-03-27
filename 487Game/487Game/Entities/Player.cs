using _487Game.Components;
using _487Game.Entities;
using _487Game.Input;
using _487Game.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace _487Game
{
    class Player : Entity
    {
        private ProjectileInformation _playerProjectileInformation;
        private float _speedOffset;

        public Player(uint id, string type, KeyboardManager keyboardManager, EntityManager entityManager) : base(id, type)
        {
            keyboardManager.NewInput += NewKeyboardInput;
            _playerProjectileInformation = new ProjectileInformation("BasicProjectile", 10, 300, 8, 16, 0f);
            _speedOffset = 100;
        }

        private void NewKeyboardInput(object sender, NewInputEventArgs e)
        {
            switch (e.Input)
            {
                case Static.Input.Left:
                    ((MovementComponent)GetComponent("MovementComponent")).xDir -= 1; break;
                case Static.Input.Right:
                    ((MovementComponent)GetComponent("MovementComponent")).xDir += 1; break;
                case Static.Input.Up:
                    ((MovementComponent)GetComponent("MovementComponent")).yDir -= 1; break;
                case Static.Input.Down:
                    ((MovementComponent)GetComponent("MovementComponent")).yDir += 1; break;
                case Static.Input.None:
                    ((MovementComponent)GetComponent("MovementComponent")).xDir = 0;
                    ((MovementComponent)GetComponent("MovementComponent")).yDir = 0;
                    break;
                case Static.Input.Shift:
                   ((MovementComponent)GetComponent("MovementComponent")).Speed -= _speedOffset;
                    break;
                case Static.Input.ShiftUp:
                    ((MovementComponent)GetComponent("MovementComponent")).Speed += _speedOffset;
                    break;
                case Static.Input.Space:
                    ((PlayerAttackComponent)GetComponent("PlayerAttackComponent"))?.Fire(e.gameTime); break;
                default:
                    break;
            }
        }
    }
}
