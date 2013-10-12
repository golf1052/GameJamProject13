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
    class Boss : Enemy
    {
        /// <summary>
        /// Constructor for Boss.
        /// </summary>
        /// <param name="loadedTx"></param>
        public Boss(Texture2D loadedTx) : base(loadedTx)
        {
            /// <summary>
            /// Assigning the inputted texture to this.tex.
            /// </summary>
            this.tex = loadedTx;

            /// <summary>
            /// We are going to set the health & damage for all Bosses as:
            /// </summary>
            this.health = 500;
            this.damage = 20;

            /// <summary>
            /// All vars inherited from Sprite, their values are set here.
            /// </summary>
            this.pos.X = 0;     // ASSIGN ME
            this.pos.Y = FLOOR;
            this.vel.X = 0;     // ASSIGN ME
            this.vel.Y = 0;     // ASSIGN ME
            this.windowCollision = true;
        }

        /// <summary>
        /// Moves the Enemy instance towards the player (at all times). This is FASTER for the Bosses!!
        /// </summary>
        public void move(Sprite player)
        {
            if (player.pos.X < this.pos.X)
            {
                this.pos.X = this.pos.X - 30;
            }
            else if (player.pos.X > this.pos.X)
            {
                this.pos.X = this.pos.X + 30;
            }
        }

        /// <summary>
        /// Override the collide method, as the Boss attacks when close to player.
        /// </summary>
        /// <param name="Player"></param>
        public void collide(Player player)
        {
            if (player.pos.X > (this.rect.Left - 10) || player.pos.X < (this.rect.Left + 10))
            {
                this.attack(player);
            }
            else if (player.rect.Intersects(this.rect))
            {
                // Effects a colision has on a player.
                player.health -= 1;
                player.isColliding = true;
                player.vel.X -= 1;
            }
            player.isColliding = false;
        }

        public void attack(Player player)
        {
            if (player.pos.X == this.pos.X)
            {
                this.pos.Y = FLOOR;
                player.health -= 1;
                player.isColliding = true;
            }
            else
            {
                this.move(player);
                this.pos.Y += 10;
            }
            player.isColliding = false;
        }
    }
}
