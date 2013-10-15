using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class Earth : Sprite
    {
        public bool moving;

        public Earth(Texture2D loadedTex)
            : base(loadedTex)
        {
            moving = true;
        }
    }
}
