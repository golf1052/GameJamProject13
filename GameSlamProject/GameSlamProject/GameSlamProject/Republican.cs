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
    class Republican : Enemy
    {
        public Republican(Texture2D loadedTx) : base(loadedTx)
        {
            // We are going to set these as the standard values for health & damage.
            health = 100;
            damage = 1;
        }
    }
}
