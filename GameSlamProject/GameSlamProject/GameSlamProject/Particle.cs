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
        /// How long the particle will stay alive before despawning.
        /// </summary>
        public TimeSpan aliveTime = new TimeSpan();

        /// <summary>
        /// Color to fade to from initial color
        /// </summary>
        public Color fadeToColor;

        /// <summary>
        /// How fast to shift the color
        /// </summary>
        public float colorShiftRate;

        /// <summary>
        /// Flag for if the particle has gravity
        /// </summary>
        public bool gravity;

        public Particle(Texture2D loadedTex, bool hasGravity)
            : base(loadedTex)
        {
            tex = loadedTex;
            pos = Vector2.Zero;
            alive = false;
            gravity = hasGravity;
        }

        /// <summary>
        /// Particle constructor for 1 color
        /// </summary>
        /// <param name="loadedTex">The particle texture</param>
        /// <param name="position">The position to spawn the particle at</param>
        /// <param name="particleColor">The color the particle is</param>
        /// <param name="aliveTimeMin">Minimum life time for particle (in milliseconds 1000 ms = 1 sec)</param>
        /// <param name="aliveTimeMax">Maximum life time for particle (in milliseconds 1000 ms = 1 sec</param>
        /// <param name="size">The size the particle should be (new Rectangle(0, 0, width, height))</param>
        /// <param name="rot">The direction the particle should fire in (in degrees) (0 points right) (90 points down) </param>
        /// <param name="spread">The random spread the particle can spawn in (in degrees) (value of 5 would be 10 degrees total spread)</param>
        /// <param name="velMultiplyMin">The minimum speed the particle can spawn at</param>
        /// <param name="velMultiplyMax">The maximum speed the particle can spawn at</param>
        /// <param name="fadeTo">The color the particle should fade to</param>
        /// <param name="hasGravity">If the particle has gravity</param>
        public Particle(Texture2D loadedTex, Vector2 position, Color particleColor, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity)
            : base(loadedTex)
        {
            tex = loadedTex;
            pos = Vector2.Zero;
            alive = false;
            gravity = hasGravity;
            SpawnParticle(position, particleColor, aliveTimeMin, aliveTimeMax, size, rot, spread, velMultiplyMin, velMultiplyMax, fadeTo, hasGravity);
        }

        /// <summary>
        /// Particle constructor for a list of colors
        /// </summary>
        /// <param name="loadedTex">The particle texture</param>
        /// <param name="position">The position to spawn the particle at</param>
        /// <param name="colorList">The list of colors the particle can be</param>
        /// <param name="aliveTimeMin">Minimum life time for particle (in milliseconds 1000 ms = 1 sec)</param>
        /// <param name="aliveTimeMax">Maximum life time for particle (in milliseconds 1000 ms = 1 sec</param>
        /// <param name="size">The size the particle should be (new Rectangle(0, 0, width, height))</param>
        /// <param name="rot">The direction the particle should fire in (in degrees) (0 points right) (90 points down) </param>
        /// <param name="spread">The random spread the particle can spawn in (in degrees) (value of 5 would be 10 degrees total spread)</param>
        /// <param name="velMultiplyMin">The minimum speed the particle can spawn at</param>
        /// <param name="velMultiplyMax">The maximum speed the particle can spawn at</param>
        /// <param name="fadeTo">The color the particle should fade to</param>
        /// <param name="hasGravity">If the particle has gravity</param>
        public Particle(Texture2D loadedTex, Vector2 position, List<Color> colorList, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity)
            : base(loadedTex)
        {
            tex = loadedTex;
            pos = Vector2.Zero;
            alive = false;
            gravity = hasGravity;
            SpawnParticle(position, colorList, aliveTimeMin, aliveTimeMax, size, rot, spread, velMultiplyMin, velMultiplyMax, fadeTo, hasGravity);
        }

        /// <summary>
        /// Spawn method for 1 color particle
        /// </summary>
        /// <param name="position">The position to spawn the particle at</param>
        /// <param name="particleColor">The color the particle is</param>
        /// <param name="aliveTimeMin">Minimum life time for particle (in milliseconds 1000 ms = 1 sec)</param>
        /// <param name="aliveTimeMax">Maximum life time for particle (in milliseconds 1000 ms = 1 sec</param>
        /// <param name="size">The size the particle should be (new Rectangle(0, 0, width, height))</param>
        /// <param name="rot">The direction the particle should fire in (in degrees) (0 points right) (90 points down) </param>
        /// <param name="spread">The random spread the particle can spawn in (in degrees) (value of 5 would be 10 degrees total spread)</param>
        /// <param name="velMultiplyMin">The minimum speed the particle can spawn at</param>
        /// <param name="velMultiplyMax">The maximum speed the particle can spawn at</param>
        /// <param name="fadeTo">The color the particle should fade to</param>
        /// <param name="hasGravity">If the particle has gravity</param>
        public void SpawnParticle(Vector2 position, Color particleColor, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity)
        {
            if (alive == false)
            {
                alive = true;
                alpha = 1.0f;
                vel = new Vector2((float)Math.Cos((MathHelper.ToRadians(rot) + MathHelper.ToRadians(random.Next(-spread, spread)))), (float)Math.Sin((MathHelper.ToRadians(rot) + MathHelper.ToRadians(random.Next(-spread, spread))))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                //vel = new Vector2(random.Next(-5, 6), random.Next(-5, 6));
                color = particleColor;
                fadeToColor = fadeTo;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(random.Next(aliveTimeMin, aliveTimeMax));
                drawRect = size;
                colorShiftRate = 1.0f;
            }
        }

        /// <summary>
        /// Spawn method for a multicolor particle
        /// </summary>
        /// <param name="position">The position to spawn the particle at</param>
        /// <param name="colorList">The list of colors the particle can be</param>
        /// <param name="aliveTimeMin">Minimum life time for particle (in milliseconds 1000 ms = 1 sec)</param>
        /// <param name="aliveTimeMax">Maximum life time for particle (in milliseconds 1000 ms = 1 sec</param>
        /// <param name="size">The size the particle should be (new Rectangle(0, 0, width, height))</param>
        /// <param name="rot">The direction the particle should fire in (in degrees) (0 points right) (90 points down) </param>
        /// <param name="spread">The random spread the particle can spawn in (in degrees) (value of 5 would be 10 degrees total spread)</param>
        /// <param name="velMultiplyMin">The minimum speed the particle can spawn at</param>
        /// <param name="velMultiplyMax">The maximum speed the particle can spawn at</param>
        /// <param name="fadeTo">The color the particle should fade to</param>
        /// <param name="hasGravity">If the particle has gravity</param>
        public void SpawnParticle(Vector2 position, List<Color> colorList, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity)
        {
            if (alive == false)
            {
                alive = true;
                alpha = 1.0f;
                vel = new Vector2((float)Math.Cos((MathHelper.ToRadians(rot) + MathHelper.ToRadians(random.Next(-spread, spread)))), (float)Math.Sin((MathHelper.ToRadians(rot) + MathHelper.ToRadians(random.Next(-spread, spread))))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                color = colorList[random.Next(0, colorList.Count - 1)];
                fadeToColor = fadeTo;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(random.Next(aliveTimeMin, aliveTimeMax));
                drawRect = size;
                colorShiftRate = 1.0f;
            }
        }

        public void SpawnParticle(Vector2 position, List<Color> colorList, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity, int seed)
        {
            Random moreRandom = new Random(seed);

            if (alive == false)
            {
                alive = true;
                alpha = 1.0f;
                vel = new Vector2((float)Math.Cos((MathHelper.ToRadians(rot) + MathHelper.ToRadians(moreRandom.Next(-spread, spread)))), (float)Math.Sin((MathHelper.ToRadians(rot) + MathHelper.ToRadians(moreRandom.Next(-spread, spread))))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                color = colorList[moreRandom.Next(0, colorList.Count - 1)];
                fadeToColor = fadeTo;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(moreRandom.Next(aliveTimeMin, aliveTimeMax));
                drawRect = size;
                colorShiftRate = 1.0f;
            }
        }

        public void SpawnParticle(Vector2 position, Color particleColor, int aliveTimeMin, int aliveTimeMax, Rectangle size, int rot, int spread, float velMultiplyMin, float velMultiplyMax, Color fadeTo, bool hasGravity, int seed)
        {
            Random moreRandom = new Random(seed);

            if (alive == false)
            {
                alive = true;
                alpha = 1.0f;
                vel = new Vector2((float)Math.Cos((MathHelper.ToRadians(rot) + MathHelper.ToRadians(moreRandom.Next(-spread, spread)))), (float)Math.Sin((MathHelper.ToRadians(rot) + MathHelper.ToRadians(moreRandom.Next(-spread, spread))))) * RandomBetween(velMultiplyMin, velMultiplyMax);
                color = particleColor;
                fadeToColor = fadeTo;
                pos = position;
                aliveTime = TimeSpan.FromMilliseconds(moreRandom.Next(aliveTimeMin, aliveTimeMax));
                drawRect = size;
                colorShiftRate = 1.0f;
            }
        }

        /// <summary>
        /// Update method for particle
        /// </summary>
        /// <param name="gameTime">gameTime from Game class</param>
        /// <param name="graphics">graphics from Game class</param>
        /// <param name="velDecayRate">Rate at which the particle should show down (0.0f instant slowdown, 1.0f no slowdown)</param>
        /// <param name="fadeRate">Rate at which the particle should fade to invisible (1.0f instant fade, 0.0f no fade MUST HAVE A FADE)</param>
        /// <param name="shiftRate">Rate at which the particle should switch from its starting color to its ending color (1.0f instant fade, 0.0f no switch MUST HAVE A SWITCH)</param>
        /// <param name="world">world from Game class</param>
        public void UpdateParticle(GameTime gameTime, GraphicsDeviceManager graphics, float velDecayRate, float fadeRate, float shiftRate, World world)
        {
            if (alive == true)
            {
                vel *= velDecayRate;

                if (gravity == true)
                {
                    vel.Y += world.GRAVITY;
                }

                pos += vel;
                aliveTime -= gameTime.ElapsedGameTime;

                if (pos.Y > world.GROUND_HEIGHT)
                {
                    pos.Y = world.GROUND_HEIGHT;
                    if (gravity == true)
                    {
                        vel.Y *= -0.5f;
                    }
                }

                if (aliveTime <= TimeSpan.FromSeconds(0))
                {
                    if (colorShiftRate > 0)
                    {
                        colorShiftRate -= shiftRate;
                        color = Color.Lerp(color, fadeToColor, colorShiftRate);
                    }
                }

                if (colorShiftRate <= 0)
                {
                    alpha -= fadeRate;
                    color *= alpha;
                }

                if (alpha <= 0.0f)
                {
                    alive = false;
                }

                Update(gameTime, graphics);
            }
        }

        /// <summary>
        /// Update method for particle
        /// </summary>
        /// <param name="gameTime">gameTime from Game class</param>
        /// <param name="graphics">graphics from Game class</param>
        /// <param name="velDecayRate">Rate at which the particle should show down (0.0f instant slowdown, 1.0f no slowdown)</param>
        /// <param name="fadeRate">Rate at which the particle should fade to invisible (1.0f instant fade, 0.0f no fade MUST HAVE A FADE)</param>
        /// <param name="shiftRate">Rate at which the particle should switch from its starting color to its ending color (1.0f instant fade, 0.0f no switch MUST HAVE A SWITCH)</param>
        /// <param name="world">world from Game class</param>
        public void UpdateParticle(GameTime gameTime, GraphicsDeviceManager graphics, float velDecayRate, float fadeRate, float shiftRate, float gravityRate, Rectangle playerRect)
        {
            if (alive == true)
            {
                vel *= velDecayRate;

                if (pos.X < playerRect.X)
                {
                    pos.X = playerRect.X;
                    vel.X *= -1;
                }

                if (pos.X > playerRect.X + playerRect.Width)
                {
                    pos.X = playerRect.X + playerRect.Width;
                    vel.X *= -1;
                }
                if (gravity == true)
                {
                    vel.Y += gravityRate;
                }

                pos += vel;
                aliveTime -= gameTime.ElapsedGameTime;

                if (gravity == true)
                {
                    vel.Y *= -0.5f;
                }

                if (aliveTime <= TimeSpan.FromSeconds(0))
                {
                    if (colorShiftRate > 0)
                    {
                        colorShiftRate -= shiftRate;
                        color = Color.Lerp(color, fadeToColor, colorShiftRate);
                    }
                }

                if (colorShiftRate <= 0)
                {
                    alpha -= fadeRate;
                    color *= alpha;
                }

                if (alpha <= 0.0f)
                {
                    alive = false;
                }

                Update(gameTime, graphics);
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
