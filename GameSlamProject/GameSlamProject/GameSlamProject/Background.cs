using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class Background : Sprite
    {
        public Sprite leftSideCopy;
        public Sprite rightSideCopy;
        public Sprite topSideCopy;
        public Sprite bottomSideCopy;
        public Sprite topLeftCopy;
        public Sprite topRightCopy;
        public Sprite bottomLeftCopy;
        public Sprite bottomRightCopy;

        public List<Sprite> backgrounds = new List<Sprite>();

        public enum TileType
        {
            All,
            MultipleXAxis
        }

        public TileType tileType;

        public bool loop = true;

        public Background(Texture2D texture, Vector2 initialPosition, TileType type) : base(texture)
        {
            if (type == TileType.All)
            {
                pos = initialPosition;

                leftSideCopy = new Sprite(tex);
                rightSideCopy = new Sprite(tex);
                topSideCopy = new Sprite(tex);
                bottomSideCopy = new Sprite(tex);
                topLeftCopy = new Sprite(tex);
                topRightCopy = new Sprite(tex);
                bottomLeftCopy = new Sprite(tex);
                bottomRightCopy = new Sprite(tex);

                RefreshBackground();

                backgrounds.Add(leftSideCopy);
                backgrounds.Add(rightSideCopy);
                backgrounds.Add(topSideCopy);
                backgrounds.Add(bottomSideCopy);
                backgrounds.Add(topLeftCopy);
                backgrounds.Add(topRightCopy);
                backgrounds.Add(bottomLeftCopy);
                backgrounds.Add(bottomRightCopy);
                backgrounds.Add(this);
            }
        }

        /// <summary>
        /// Secondary Constructor. Used for scrolling backgrounds on one axis.
        /// </summary>
        /// <param name="texture">The first background that will be displayed</param>
        /// <param name="textures">A list of the rest of the backgrounds</param>
        /// <param name="initialPosition">The initial position of the first background</param>
        /// <param name="type">The type of background scrolling that will be used. Use single axis scrolling</param>
        public Background(Texture2D texture, List<Texture2D> textures, Vector2 initialPosition, TileType type) : base(texture)
        {
            if (type == TileType.MultipleXAxis)
            {
                backgrounds.Add(new Sprite(texture));

                foreach (Texture2D background in textures)
                {
                    backgrounds.Add(new Sprite(background));
                }

                RefreshBackground();
            }
        }

        public void RefreshBackground()
        {
            if (tileType == TileType.All)
            {
                leftSideCopy.pos = pos;
                rightSideCopy.pos = pos;
                topSideCopy.pos = pos;
                bottomSideCopy.pos = pos;
                topLeftCopy.pos = pos;
                topRightCopy.pos = pos;
                bottomLeftCopy.pos = pos;
                bottomRightCopy.pos = pos;

                leftSideCopy.pos.X -= tex.Width;
                rightSideCopy.pos.X += tex.Width;
                topSideCopy.pos.Y -= tex.Height;
                bottomSideCopy.pos.Y += tex.Height;
                topLeftCopy.pos += new Vector2(-tex.Width, -tex.Height);
                topRightCopy.pos += new Vector2(tex.Width, -tex.Height);
                bottomLeftCopy.pos += new Vector2(-tex.Width, tex.Height);
                bottomRightCopy.pos += new Vector2(tex.Width, tex.Height);
            }

            if (tileType == TileType.MultipleXAxis)
            {
                for (int i = 0; i < backgrounds.Count; i++)
                {
                    if (i != 0)
                    {
                        backgrounds[i].pos.X += tex.Width * i;
                    }
                }
            }
        }

        public void MoveBackground(Vector2 movementVector)
        {
            foreach (Sprite background in backgrounds)
            {
                background.pos += movementVector;
            }
        }

        public void UpdateBackground(GraphicsDeviceManager graphics)
        {
            if (tileType == TileType.All)
            {
                if (pos.X + tex.Width / 2 < 0)
                {
                    pos.X = rightSideCopy.pos.X;

                    RefreshBackground();
                }

                if (pos.X - tex.Width / 2 > graphics.GraphicsDevice.Viewport.Width)
                {
                    pos.X = leftSideCopy.pos.X;

                    RefreshBackground();
                }

                if (pos.Y + tex.Height / 2 < 0)
                {
                    pos.Y = bottomSideCopy.pos.Y;

                    RefreshBackground();
                }

                if (pos.Y - tex.Height / 2 > graphics.GraphicsDevice.Viewport.Height)
                {
                    pos.Y = topSideCopy.pos.Y;

                    RefreshBackground();
                }
            }

            if (tileType == TileType.MultipleXAxis)
            {
            }
        }

        public void DrawBackground(SpriteBatch spriteBatch)
        {
            foreach (Sprite background in backgrounds)
            {
                background.Draw(spriteBatch);
            }
        }
    }
}
