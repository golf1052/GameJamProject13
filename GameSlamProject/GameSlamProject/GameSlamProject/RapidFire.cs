using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media; 

namespace GameSlamProject
{
    /// <summary> RapidFire
    /// RapidFire is a subclass if the Pup class. When touched, the RapidFire
    /// powerup gives the player a faster shooting speed.
    /// </summary>
    public class RapidFire : Pup
    {
        // Constant. Rate of Fire speed change.
        public int rofChange;
        public RapidFire(Texture2D loadedTex, int duration, int rofChange)
            : base(loadedTex, duration)
        {
            this.duration = RAPIDFIRE_PUP_DURATION;
            this.rofChange = RAPIDFIRE_PUP_ROFCHANGE;
        }
        // Using the RapidFire powerup increases the players shooting 
        // rate of fire. It also locks skills 3 and 4.
        public void UsePup(Player p)
        {
            p.pupDuration = p.pupDuration + duration;
            p.rateOfFire = rofChange;
            p.hasPup = true;
            p.canUseStrike = false;
            p.canUseFear = false;
        }
    }
}
