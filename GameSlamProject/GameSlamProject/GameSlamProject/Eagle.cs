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
    /// <summary> Eagle
    /// Eagle is a class representing skill #4.
    /// </summary>
    public class Eagle : Sprite
    {
        public int damage;
        public GraphicsDeviceManager graphics;
        public bool hitBottom;

        #region CONSTANTS
        public Vector2 descendingRightSpeed = new Vector2(7, 25);
        public Vector2 ascendingRightSpeed = new Vector2(7, -25);

        public Vector2 descendingLeftSpeed = new Vector2(-7, 25);
        public Vector2 ascendingLeftSpeed = new Vector2(-7, -25);

        public const int EAGLE_WIDTH = 40;
        public const int EAGLE_HEIGHT = 25;
        #endregion

        public Eagle(Texture2D loadedTex, int damage, GraphicsDeviceManager graphics)
            : base(loadedTex)
        {
            this.damage = 10;
            this.pos.X = graphics.GraphicsDevice.Viewport.Width / 2;
            this.pos.Y = 0;
            this.graphics = graphics;
            this.hitBottom = false;
        }

        // Determines which direction to call the eagle.
        public void moveEagle(Player p)
        {
            if (p.canUseStrike)
            {
                if (p.facing == Facing.Right)
                {
                    this.moveEagleLeft();
                }
                else this.moveEagleRight();
            }
        }

        // Moves the Eagle when it's called to the right.
        public void moveEagleLeft()
        {
            
            if (!this.hitBottom)
            {
                if (this.pos.Y >= graphics.GraphicsDevice.Viewport.Height - 25)
                {
                    this.hitBottom = true;
                }
                else
                {
                    this.pos += descendingRightSpeed;
                }
            }
            else 
            this.pos += ascendingRightSpeed;
        }
        // Moves the Eagle when it's called to the left.
        public void moveEagleRight()
        {
            
            if (!this.hitBottom)
            {
                if (this.pos.Y >= graphics.GraphicsDevice.Viewport.Height - 25)
                {
                    this.hitBottom = true;
                }
                else
                {
                    this.pos += descendingLeftSpeed;
                }
            }
            else
                this.pos += ascendingLeftSpeed;
        }

        // Checks to see if the Eagle is nearby the enemy. If it is, the enemy
        // takes damage.
        public void damageEnemy(Enemy e)
        {
            Rectangle eagleBox = new Rectangle((int)this.pos.X, (int)this.pos.Y, EAGLE_WIDTH, EAGLE_HEIGHT);
            if (eagleBox.Intersects(e.rect))
            {
                e.health -= this.damage;
            }
        }

        //Checks to see if the eagle hits any enemies on this arraylist of Enemies.
        public void damageEnemies(List<Enemy> LoE)
        {
            foreach (Enemy e in LoE)
            {
                this.damageEnemy(e);
            }
        }

        // checks to see if the eagle is out of bounds now, so it can reset.
        public void eagleOut(Player p)
        {
            if (this.pos.Y <= -1)
            {
                p.canUseStrike = true;
                this.pos.X = graphics.GraphicsDevice.Viewport.Width / 2;
                this.pos.Y = 0;
                this.visible = false;
            }
        }
    }
}
