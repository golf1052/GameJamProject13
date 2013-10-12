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
    /// Class to store texture lines that are used for "hitscan" weapons
    /// </summary>
    public class RayLine : Sprite
    {
        /// <summary>
        /// Flag for if the ray is being displayed on screen.
        /// </summary>
        public bool isDisplayed;

        /// <summary>
        /// Stores how much damage the ray should give to the target.
        /// </summary>
        public int damage;

        /// <summary>
        /// Main constructor. Sets isDisplayed for the ray to be false.
        /// </summary>
        /// <param name="texture">Base texture parameter from Sprite.cs. Load a texture here.</param>
        public RayLine(Texture2D texture)
            : base(texture)
        {
            isDisplayed = false;
        }
    }
}
