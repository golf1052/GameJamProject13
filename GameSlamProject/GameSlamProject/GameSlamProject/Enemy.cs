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
        const int MOVE_DISTANCE = 10; // How far we want the unit to move on a tick.
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

        public bool outOfBounds = true;

        public enum Animations
        {
            Running,
            Idle,
            None
        }

        public Animations animationState;

        /// <summary>
        /// Base constructor for an enemy
        /// </summary>
        /// <param name="loadedTex">The texture to be loaded</param>
        public Enemy(Texture2D loadedTex)
            : base(loadedTex)
        {
        }

        /// <summary>
        /// Constructor for an Enemy.
        /// </summary>
        /// <param name="loadedTx"> Texture represents the image file used for that enemy. </param>
        public Enemy(Texture2D loadedTex, int spawnRange, float minVelocity, float maxVelocity, World world) : base(loadedTex)
        {
            // MapSector sector should be around here somewhere soon in teh future
            tex = loadedTex;
            isAnimatable = false;
            SpawnEnemy(spawnRange, minVelocity, maxVelocity, world);
        }

        public Enemy(List<SpriteSheet> loadedSheets, int spawnRange, float minVelocity, float maxVelocity, World world)
            : base(loadedSheets)
        {
            SpawnEnemy(spawnRange, minVelocity, maxVelocity, world);
            isAnimatable = true;
            animationState = Animations.Running;
        }

        /// <summary>
        /// Moves the Enemy instance towards the player (at all times).
        /// </summary>
        public void Update(GameTime gameTime, Player p, World world, int sheet)
        {
            pos += vel;
            drawRect.X = (int)pos.X;
            drawRect.Y = (int)pos.Y;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteTransform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(pos, 0.0f));
            rect = CalculateBoundingRectangle(new Rectangle(0, 0, tex.Width, tex.Height), spriteTransform);

            if (health <= 0)
            {
                alive = false;
            }

            // if they enemies are afraid, they move away from the player.
            if (p.fearOn)
            {
                if (p.pos.X < this.pos.X)
                {
                    this.pos.X = this.pos.X + MOVE_DISTANCE;
                    facing = Facing.Right;
                    animationState = Animations.Running;
                }
                else if (p.pos.X > this.pos.X)
                {
                    this.pos.X = this.pos.X - MOVE_DISTANCE;
                    facing = Facing.Left;
                    animationState = Animations.Running;
                }
            }

            if (outOfBounds == false)
            {
                if (pos.X < 0)
                {
                    pos.X = 0;
                    vel *= -1;
                    facing = Facing.Right;
                    animationState = Animations.Running;
                }

                if (pos.X > world.graphics.GraphicsDevice.Viewport.Width)
                {
                    pos.X = world.graphics.GraphicsDevice.Viewport.Width;
                    vel *= -1;
                    facing = Facing.Left;
                    animationState = Animations.Running;
                }
            }
            else
            {
                if (pos.X < 0)
                {
                    pos.X = 0;
                    vel *= -1;
                    facing = Facing.Right;
                    animationState = Animations.Running;
                }

                if (pos.X > world.graphics.GraphicsDevice.Viewport.Width)
                {
                    pos.X = world.graphics.GraphicsDevice.Viewport.Width;
                    vel *= -1;
                    facing = Facing.Left;
                    animationState = Animations.Running;
                }
            }

            if (isAnimatable == true)
            {
                spriteSheets[sheet].UpdateAnimation(gameTime);

                foreach (SpriteSheet currentSheet in spriteSheets)
                {
                    currentSheet.pos = pos;
                    currentSheet.vel = vel;
                    currentSheet.visible = visible;
                    currentSheet.alive = alive;
                    currentSheet.facing = facing;
                    currentSheet.isAnimatable = isAnimatable;
                    currentSheet.origin = origin;
                    currentSheet.rotation = rotation;
                    currentSheet.scale = scale;
                }
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
                //player.isColliding = true;
                //player.vel.X -= 1;
            }
            player.isColliding = false;
        }

        /// <summary>
        /// Depending on where the player is on the map && how many enemies are on screen, spawn
        /// either a Democrat || a Republican
        /// </summary>
        public void SpawnEnemy(int spawnRange, float minVelocity, float maxVelocity, World world)
        {
            int enemiesAlive = 0;

            foreach (Enemy e in world.enemyList)
            {
                if (e.alive == true)
                {
                    enemiesAlive++;
                }
            }

            if (enemiesAlive < world.MAX_ENEMIES)
            {
                alive = true;
                health = 2;

                if (random.NextDouble() < 0.5)
                {
                    pos = new Vector2(random.Next(-spawnRange, 0), world.GROUND_HEIGHT - tex.Height / 2);
                }
                else
                {
                    pos = new Vector2(random.Next(world.graphics.GraphicsDevice.Viewport.Width + 1, world.graphics.GraphicsDevice.Viewport.Width + spawnRange), world.GROUND_HEIGHT - tex.Height / 2);
                }
                if (pos.X < 0)
                {
                    vel = new Vector2(world.RandomBetween(minVelocity, maxVelocity), 0.0f);
                }
                else
                {
                    vel = new Vector2(-world.RandomBetween(minVelocity, maxVelocity), 0.0f);
                }

                //if (random.NextDouble() < 0.5)
                //{
                //    pos = new Vector2(random.Next(-spawnRange, 0), world.GROUND_HEIGHT - spriteSheets[0].tex.Height / 2);
                //}
                //else
                //{
                //    pos = new Vector2(random.Next(world.graphics.GraphicsDevice.Viewport.Width + 1, world.graphics.GraphicsDevice.Viewport.Width + spawnRange), world.GROUND_HEIGHT - spriteSheets[0].tex.Height / 2);
                //}
                //if (pos.X < 0)
                //{
                //    vel = new Vector2(world.RandomBetween(minVelocity, maxVelocity), 0.0f);
                //}
                //else
                //{
                //    vel = new Vector2(-world.RandomBetween(minVelocity, maxVelocity), 0.0f);
                //}
            }
        }

        public void killEnemy(World w)
        {
            //if(this.health <= 0 || ****Enemy is not on the window****)
            //{
            //    w.enemyList.Remove(this);
            //}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (alive == true)
            {
                spriteBatch.Draw(tex, pos, null, color, rotation, origin, scale, SpriteEffects.None, 0);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int sheet)
        {
            if (alive == true)
            {
                spriteSheets[sheet].DrawAnimation(spriteBatch);
            }
        }
    }
}
