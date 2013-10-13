using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameSlamProject
{
    public class Enemy : Sprite
    {
        #region CONSTANTS
        const int MOVE_DISTANCE = 5; // How far we want the unit to move on a tick.
        #endregion

        // FIELDS -----------------------------------------------------------------------------------------------------

        /// <summary>
        /// int representing the enemy's health.
        /// </summary>
        public int health;

        /// <summary>
        /// int representing how much damage the player can do.
        /// </summary>
        public int damage;
        /// <summary>
        /// Constructor for an Enemy.
        /// </summary>
        /// <param name="loadedTx"> Texture represents the image file used for that enemy. </param>
        public Enemy(Texture2D loadedTx) : base(loadedTx)
        {
            // Empty constructor, since all subclasses have particular fields.
        }

        /// <summary>
        /// Moves the Enemy instance towards the player (at all times).
        /// </summary>
        public void move(Player p)
        {
            // if they enemies are afraid, they move away from the player.
            if (p.fearOn)
            {
                if (p.pos.X < this.pos.X)
                {
                    this.pos.X = this.pos.X + MOVE_DISTANCE;
                }
                else if (p.pos.X > this.pos.X)
                {
                    this.pos.X = this.pos.X - MOVE_DISTANCE;
                }
            }
                // otherwise, they move towards the player.
            else 
            if (p.pos.X + 5 < this.pos.X)
            {
                this.pos.X = this.pos.X - MOVE_DISTANCE;
            }
            else if (p.pos.X > this.pos.X)
            {
                this.pos.X = this.pos.X + MOVE_DISTANCE;
            }
        }

        /// <summary>
        /// We check to see if this enemy has collided w/ given player, and if so, we hurt player
        /// </summary>
        /// <param name="player"></param>
        public void collide(Player player)
        {
            if (player.rect.Intersects(this.rect))
            {
                // Effects a colision has on a player.
                player.health -= 1;
                player.isColliding = true;
                player.vel.X -= 1;
            }
            player.isColliding = false;
        }

        /// <summary>
        /// Depending on where the player is on the map && how many enemies are on screen, spawn
        /// either a Democrat || a Republican
        /// </summary>
        public void spawnEnemy(Player p, World w, MapSector ms)
        {
            if (w.enemyList.Count < 10)
                if (p.rect.Intersects(ms.start))
                {
                    w.enemyList.Add(new Enemy(****Democrat or Republican****)); // set location to (w.graphics.GraphicsDevice.Viewport.Width, w.GROUND_HEIGHT)
                }
                if (p.rect.Intersects(ms.republican))
                {
                    w.enemyList.Add(new Enemy(****Republican****)); // set location to (w.graphics.GraphicsDevice.Viewport.Width, w.GROUND_HEIGHT)
                }
                if (p.rect.Intersects(ms.democrat))
                {
                    w.enemyList.Add(new Enemy(****Democrat****)); // set location to (w.graphics.GraphicsDevice.Viewport.Width, w.GROUND_HEIGHT)
                }
                if (p.rect.Intersects(ms.boss))
                {
                    // Add one boss to the enemy list after clearing all enemies off list.
                }
        }

        public void killEnemy(World w)
        {
            if(this.health <= 0 || ****Enemy is not on the window****)
            {
                w.enemyList.Remove(this);
            }
        }
    }
}
