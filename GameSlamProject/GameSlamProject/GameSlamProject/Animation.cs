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
	/// <summary>
    /// Needs to be worked on. Sprite functionality is all there but some animation stuff doesnt work. Use at your own discretion.
    /// </summary>
    public class Animation : Sprite
    {
        /// <summary>
        /// The sprite strip the sprite is refering from.
        /// </summary>
        public Texture2D spriteStrip;
        
        /// <summary>
        /// How long has gone by from the last frame.
        /// </summary>
        public int elapsedTime;

        /// <summary>
        /// How long each frame should last in milliseconds.
        /// </summary>
        public int frameTime;

        /// <summary>
        /// The number of frames in the animation.
        /// </summary>
        public int frameCount;

        /// <summary>
        /// The current frame we are displaying.
        /// </summary>
        public int currentFrame;

        /// <summary>
        /// The rectangle on the sprite sheet that is actually being drawn.
        /// </summary>
        public Rectangle sourceRect = new Rectangle();

        /// <summary>
        /// The width of the frame on the sprite sheet.
        /// </summary>
        public int frameWidth;

        /// <summary>
        /// The height of the frame on the sprite sheet.
        /// </summary>
        public int frameHeight;

        /// <summary>
        /// Flag for if the animation is currently active.
        /// </summary>
        public bool active;

        /// <summary>
        /// Flag for if the animation should loop.
        /// </summary>
        public bool looping;

        /// <summary>
        /// Main constructor. When making a new animation all these parameters must be set.
        /// </summary>
        /// <param name="texture">Base texture parameter from Sprite.cs. Load the sprite sheet here.</param>
        /// <param name="width">The width of the frame on the sprite sheet.</param>
        /// <param name="height">The height of the frame on the sprite sheet.</param>
        /// <param name="count">The number of frames in the animation.</param>
        /// <param name="time">How long each frame should last in milliseconds.</param>
        /// <param name="loop">If the animation should loop</param>
        public Animation(Texture2D texture, int width, int height, int count, int time, bool loop)
            : base(texture)
        {
            frameWidth = width;
            frameHeight = height;
            frameCount = count;
            frameTime = time;
            looping = loop;
            spriteStrip = texture;
            elapsedTime = 0;
            currentFrame = 0;
            active = true;
        }

        /// <summary>
        /// Animate the current sprite.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        public void Animate(GameTime gameTime)
        {
            if (active == false)
            {
                return;
            }

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime)
            {
                currentFrame++;

                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if (looping == false)
                    {
                        active = false;
                    }
                }

                elapsedTime = 0;
            }

            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
        }

        /// <summary>
        /// Update method for animated sprites. Needs to be changed so that it can override the Sprite.cs Update function.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        public void Update(GameTime gameTime)
        {
            if (active == false)
            {
                return;
            }

            elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > frameTime)
            {
                currentFrame++;

                if (currentFrame == frameCount)
                {
                    currentFrame = 0;
                    if (looping == false)
                    {
                        active = false;
                    }
                }

                elapsedTime = 0;
            }

            sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
        }

        /// <summary>
        /// Draw method for animated sprites. Needs to be changed so that it can override the Sprite.cs Draw function
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from class</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                spriteBatch.Draw(spriteStrip, pos, sourceRect, color);
            }
        }
    }
}
