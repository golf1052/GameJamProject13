using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameSlamProject
{
    public class Background : Sprite
    {
        public Background previous;
        public Background next;

        public Background(Texture2D loadedTex)
            : base(loadedTex)
        {
            origin = Vector2.Zero;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            if (previous == null)
            {
                if (pos.X > 0)
                {
                    pos.X = 0;
                }
            }

            if (previous != null)
            {
                pos = new Vector2(previous.pos.X + previous.tex.Width, 0);
            }

            if (next == null)
            {
                if (pos.X + tex.Width < graphics.GraphicsDevice.Viewport.Width)
                {
                    pos.X = 0;
                }
            }
        }
    }
}
