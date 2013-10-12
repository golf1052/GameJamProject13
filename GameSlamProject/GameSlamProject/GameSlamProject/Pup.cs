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
    /// <summary>
    /// Super class Pup represents the game's powerups. It has an
    /// abstracted usePup method, which alters the Player state
    /// depending on the power up.
    /// </summary>
    public class Pup : Sprite
    {
        #region CONSTANTS
        // The determined duration of the Obamacare powerup.
        public const int OBCARE_PUP_DURATION = 600;
        // The determined duration of the BearArms powerup.
        public const int BEARARMS_PUP_DURATION = 600;
        // The determined rofchange of the BearArms powerup.
        public const int BEARARMS_PUP_MELEECHANGE = 2;
        // The determined duration of the RapidFire powerup.
        public const int RAPIDFIRE_PUP_DURATION = 600;
        // The determined rofchange of the RapidFire powerup.
        public const int RAPIDFIRE_PUP_ROFCHANGE = 2;
        
        #endregion 
        public int duration;
        public Pup(Texture2D loadedTex, int duration)
            : base(loadedTex)
        {
        }
        abstract void UsePup(Player p);
    }
}


