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
    class Boss : Enemy
    {
        public Boss(Texture2D loadedTx) : base(loadedTx)
        {
            // Assigning the inputted texture to this.tex.
            this.tex = loadedTx;

            // We are going to set the health & damage for all Bosses as:
            this.health = 500;
            this.damage = 20;

            // All vars inherited from Sprite, their values are set here.

        }
    }
}
