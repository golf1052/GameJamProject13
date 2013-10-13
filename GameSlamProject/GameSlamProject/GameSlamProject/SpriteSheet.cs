using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class SpriteSheet : Sprite
    {
        public int frameWidth;
        public int frameHeight;
        public int frameCount;
        public int frameTime;
        public bool looping;
        public Texture2D sheet;
        public int elapsedTime;
        public int currentFrame;
        public Rectangle sourceRect = new Rectangle();
        public bool active;

        public SpriteSheet(Texture2D loadedTex, int width, int height, int count, int time, bool loop)
            : base(loadedTex)
        {
            sheet = loadedTex;
            frameWidth = width;
            frameHeight = height;
            frameCount = count;
            frameTime = time;
            looping = loop;
            active = true;
            origin = Vector2.Zero;
        }

        public void UpdateAnimation(GameTime gameTime)
        {
            if (active == false)
            {
                return;
            }
            else
            {
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
        }

        public void DrawAnimation(SpriteBatch spriteBatch)
        {
            if (active == true)
            {
                if (facing == Facing.Left)
                {
                    spriteBatch.Draw(tex, pos, sourceRect, color, rotation, origin, scale, SpriteEffects.FlipHorizontally, 0);
                }
                else if (facing == Facing.Right)
                {
                    spriteBatch.Draw(tex, pos, sourceRect, color, rotation, origin, scale, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(tex, pos, sourceRect, color, rotation, origin, scale, SpriteEffects.None, 0);
                }
            }
        }
    }
}
