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
        public Vector2 BULLET_SPEED_L = new Vector2(15, 0);
        public Vector2 BULLET_SPEED_R = new Vector2(-15, 0);
        public const int BULLET_WIDTH = 10;
        public const int BULLET_HEIGHT = 5;
        #endregion
        public Bullet(Texture2D loadedTex) 
            : base(loadedTex)
        {
        }

        // Moves the fired bullet depending on the direction of the player.
        public void moveBullet(Player p)
        {
            if (this.visible)
            {
            if (p.facing == Facing.Left)
            {
                this.pos = this.pos - BULLET_SPEED_L;
            }
            else this.pos = this.pos - BULLET_SPEED_R;
            }
        }


        // Determines if the bullet hit the enemy. If it did, reduce the enemy
        // health.
        public void hitByBullet(Enemy e, Player p)
        {
            Rectangle bulletBox = new Rectangle((int)this.pos.X, (int)this.pos.Y, BULLET_WIDTH, BULLET_HEIGHT);
            if (bulletBox.Intersects(e.rect)) 
            {
                e.health -= 1;
                p.myBullet.pos.X = 0;
                p.myBullet.pos.Y = 0;
                p.canFire = true;
                p.myBullet.visible = false;
                // Bullet gets moved back to the Player elsewhere
            }
        }

        // Determines if the bullet hit any of these enemies.
        public void hitEnemies(List<Enemy> LoE, Player p)
        {
            foreach (Enemy e in LoE)
            {
                this.hitByBullet(e, p);
            }
        }

        // checks if a bullet is off screen
        public void offScreen(Player p, World w)
        {
            if ((this.pos.X <= 0) || (this.pos.X >= w.graphics.GraphicsDevice.Viewport.Width))
            {
                this.visible = false;
                this.pos = new Vector2(0, 0);
                p.canFire = true;
            }
        }
    }
}
