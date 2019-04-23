using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Take3.GameManagement;

namespace Take3.ECS.Scripts
{
    class Enemy : Script
    {
        protected int health { get; set; }

        protected int initialScore;
        protected int score;
        
        public int Score { get { return score; } }

        public override void OnCollision(GameObject collider)
        {
            initialScore = 100;
            if(collider.Tag == "PlayerProjectile")
            {
                health--;
            }
        }

        public override void Update(GameTime gameTime)
        {
            score = initialScore - ((int)gameTime.TotalGameTime.TotalSeconds * 6);
            if(health <= 0)
            {
                ((Score)GameManager.GetObjectByTag("Score").GetComponent<Score>()).AddScore(score);
                Die();
            }
        }
    }
}
