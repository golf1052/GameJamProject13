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
    class Democrat : Enemy
    {
        public Democrat(Texture2D loadedTx) : base(loadedTx)
        {
            // Assigning the inputted texture to this.tex.
            this.tex = loadedTx;

            // We are going to set these as the standard values for health & damage.
            health = 100;
            damage = 1;

            // All vars inherited from Sprite, their values are set here.
            this.alive = true;
            this.visible = true;
            this.pos.X = 0;     // ASSIGN ME
            this.pos.Y = 0;     // ASSIGN ME


        }
    }
}
