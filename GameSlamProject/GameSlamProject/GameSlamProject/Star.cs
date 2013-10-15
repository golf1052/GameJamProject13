using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class Star : Sprite
    {
        int size;

        public Star(Texture2D loadedTex)
            : base(loadedTex)
        {
            alive = false;
        }

        public void SpawnStar(Rectangle playerWindow, int seed)
        {
            Random random = new Random(seed);
            alive = true;
            size = random.Next(5, 9);
            pos.X = random.Next(playerWindow.X, playerWindow.X + playerWindow.Width);
            pos.Y = -size;
            drawRect = new Rectangle((int)pos.X, (int)pos.Y, size, size);
        }

        public void UpdateStar(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            pos.Y += 10.0f;

            if (pos.Y > graphics.GraphicsDevice.Viewport.Height)
            {
                alive = false;
            }

            Update(gameTime, graphics);
        }
    }
}
