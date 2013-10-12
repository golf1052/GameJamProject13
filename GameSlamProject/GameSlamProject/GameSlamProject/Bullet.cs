using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameSlamProject
{
    /// <summary>
    /// Bullet is the projectile filed during the players #2 skill.
    /// </summary>
    public class Bullet : Sprite
    {
        #region CONSTANTS
        public const int BULLET_SPEED = 15;
        public const int BULLET_WIDTH = 10;
        public const int BULLET_HEIGHT = 5;
        #endregion
        public int bulletSpeed;
        public Bullet(Texture2D loadedTex)
            : base(loadedTex)
        {
            this.bulletSpeed = BULLET_SPEED;
        }

        // Moves the fired bullet depending on the direction of the player.
        public void moveBullet(Player p)
        {
            if (p.facing == Facing.Left)
            {
                this.pos.X -= (this.bulletSpeed * p.rateOfFire);
            }
            else this.pos.X += (this.bulletSpeed * p.rateOfFire);
        }

        // Determines if the bullet hit the enemy. If it did, reduce the enemy
        // health.
        public void hitByBullet(Enemy e)
        {
            Rectangle bulletBox = new Rectangle((int)this.pos.X, (int)this.pos.Y, BULLET_WIDTH, BULLET_HEIGHT);
            if (bulletBox.Intersects(e.rect)) 
            {
                e.health -= 1;


            }
        }
    }
}
