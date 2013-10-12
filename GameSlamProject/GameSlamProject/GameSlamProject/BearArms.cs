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
    /// <summary> BearArms
    /// BearArms is a subclass of the Pup class. When touched, the BearArms
    /// powerup gives the player a faster melee speed.
    /// </summary>
    public class BearArms : Pup
    {
        // Constant. The melee speed change being applied to the Player.
        public int meleeChange;
        public BearArms(Texture2D loadedTex, int duration, int meleeChange)
            : base(loadedTex, duration)
        {
            this.duration = BEARARMS_PUP_DURATION;
            this.meleeChange = BEARARMS_PUP_MELEECHANGE;
        }
        // Using the Beararms powerup increases the rate of fire of the
        // player. It also locks the skills 2 3 and 4.
        public override void UsePup(Player p)
        {
            p.pupDuration = p.pupDuration + duration;
            p.rateOfFire = meleeChange;
            p.hasPup = true;
            p.canFire = false;
            p.canUseStrike = false;
            p.canUseFear = false;
        }
    }
}
