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
    /// <summary> Obamacare
    /// Obamacare is a subclass of the Pup class. When touched, The player 
    /// recieves invulnerability for 10 seconds.
    /// </summary>
    public class Obamacare : Pup
    {
        // True. The obcare field is meant to make the player invulnerable.
        public bool obcare;
        public Obamacare(Texture2D loadedTex, int duration, bool Obcare)
            : base(loadedTex, duration)
        {
            this.duration = OBCARE_PUP_DURATION;
            this.obcare = true;
        }
        // Using the obamacare powerup makes the player invulnerable for the 
        // OBCARE_PUP_DURATION.
        public override void UsePup(Player p)
        {
            p.pupDuration = p.pupDuration + duration;
            p.isInvulnerable = obcare;
            p.hasPup = true;
            p.canUseStrike = false;
            p.canUseFear = false;
        }
    }
}
