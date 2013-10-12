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
        }

        public void SetPreviousBackground(Background bg)
        {
            previous = bg;
        }

        public void SetNextBackground(Background bg)
        {
            next = bg;
        }

        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {

        }
    }
}
