using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameSlamProject
{
    /// <summary>
    /// Handles basic particle behavior. Based off Sprite.cs
    /// </summary>
    public class Particle : Sprite
    {
        /// <summary>
        /// Random to use in class. I don't know why it is here.
        /// </summary>
        public Random random = new Random();

        /// <summary>
        /// How long the particle will stay alive before despawning.
        /// </summary>
        public TimeSpan aliveTime = new TimeSpan();

        /// <summary>
        /// Main constructor. Loads the particle texture.
        /// </summary>
        /// <param name="loadedTex">Base texture parameter from Sprite.cs. Load a texture here.</param>
        public Particle(Texture2D loadedTex)
            : base(loadedTex)
        {
            tex = loadedTex;
            pos = Vector2.Zero;
            alive = false;
        }

        /// <summary>
        /// Spawns a particle. I have no idea how this is supposed to work as there are many different versions of SpawnParticle somewhere...will figure out.
        /// </summary>
        /// <param name="position">The initial spawn position of the particle</param>
        /// <param name="particleColor">What color the particle should be</param>
        /// <param name="velMultiplyMin">The slowest the particle can move. This number is multipled by a unit vector.</param>
        /// <param name="velMultiplyMax">The fastest the particle can move. This number is multipled by a unit vector.</param>
        /// <param name="aliveTimeMin">The shortest amount of time a particle can be alive. In milliseconds.</param>
        /// <param name="aliveTimeMax">The longest amount of time a particle can be alive. In milliseconds.</param>
        /// <param name="seed">Seed for spawning random particles...this was a weird bug</param>
        /// <param name="spread">The spread of the particles. Used for firing particles in a given direction.</param>
        /// <param name="rot">The current rotation of the sprite the particles are emitting from</param>
        //public void SpawnParticle(Vector2 position, Color particleColor, float velMultiplyMin, float velMultiplyMax, int aliveTimeMin, int aliveTimeMax, int seed, int spread, float rot)
        //{
        //    if (alive == false)
        //    {
        //        random = new Random(seed);
        //        alive = true;
        //        vel = new Vector2((float)Math.Cos((rot + MathHelper.ToRadians(random.Next(-spread, spread)))), (float)Math.Sin((rot + MathHelper.ToRadians(random.Next(-spread, spread))))) * random.Next((int)velMultiplyMin, (int)velMultiplyMax);
        //        alpha = 1.0f;
        //        color = particleColor;
        //        pos = position;
        //        aliveTime = TimeSpan.FromMilliseconds(random.Next(aliveTimeMin, aliveTimeMax));
        //    }
        //}

        public void SpawnParticle(Vector2 position, Color particleColor, float velMultiplyMin, float velMultiplyMax, int aliveTimeMin, int aliveTimeMax, int seed)
        {
            if (alive == false)
            {
                random = new Random(seed);
                alive = true;
                vel = AngleToVector(MathHelper.ToRadians(random.Next(0, 359))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                alpha = 1.0f;
                color = particleColor;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(random.Next(aliveTimeMin, aliveTimeMax));
            }
        }

        public void SpawnParticle(Vector2 position, Color particleColor, float velMultiplyMin, float velMultiplyMax, int aliveTimeMin, int aliveTimeMax, int seed, int spread, float rot)
        {
            if (alive == false)
            {
                random = new Random(seed);
                alive = true;
                vel = AngleToVector(MathHelper.ToRadians(rot + MathHelper.ToRadians(random.Next(-spread, spread)))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                alpha = 1.0f;
                color = particleColor;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(random.Next(aliveTimeMin, aliveTimeMax));
            }
        }

        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        public void UpdateParticle(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            vel *= 0.95f;
            pos += vel;
            aliveTime -= gameTime.ElapsedGameTime;

            spriteTransform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(pos, 0.0f));
            rect = CalculateBoundingRectangle(new Rectangle(0, 0, tex.Width, tex.Height), spriteTransform);

            if (aliveTime <= TimeSpan.FromSeconds(0))
            {
                alpha -= 0.1f;
                color *= alpha;
            }

            if (alpha <= 0.0f)
            {
                alive = false;
            }

            //if (pos.X < 0 || pos.Y < 0 || pos.X > graphics.GraphicsDevice.Viewport.Width || pos.Y > graphics.GraphicsDevice.Viewport.Height)
            //{
            //    alive = false;
            //}
            //else
            //{
            //    if (aliveTime <= TimeSpan.FromSeconds(0))
            //    {
            //        alpha -= 0.1f;
            //        color *= alpha;
            //    }

            //    if (alpha <= 0.0f)
            //    {
            //        alive = false;
            //    }
            //}
        }

        public void UpdateParticle2(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            pos += vel;
            drawRect.X = (int)pos.X;
            drawRect.Y = (int)pos.Y;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteTransform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(pos, 0.0f));
            rect = CalculateBoundingRectangle(new Rectangle(0, 0, tex.Width, tex.Height), spriteTransform);

            if (pos.X < 0 || pos.Y < 0 || pos.X > graphics.GraphicsDevice.Viewport.Width || pos.Y > graphics.GraphicsDevice.Viewport.Height)
            {
                alive = false;
            }
        }

        /// <summary>
        /// Draws the particle.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from class.</param>
        public void DrawParticle(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, color);
        }

        /// <summary>
        /// Gets a random value between two floats. The minimum number can also be negative!
        /// </summary>
        /// <param name="min">The smallest number you could get</param>
        /// <param name="max">The largest number you could get, not inclusive</param>
        /// <returns>Returns a random number of type float.</returns>
        public float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }
    }
}
