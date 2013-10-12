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
    /// Based off PlayerBase.cs. Enemies now have all player functionalities which includes health, weapons, and shooting.
    /// </summary>
    public class Enemy : PlayerBase
    {
        public enum Modes
        {
            Patrol
        }

        public Modes mode = Modes.Patrol;

        public Vector2 targetPosition = Vector2.Zero;

        /// <summary>
        /// Main constructor. Nothing is set here because the enemies do not have any
        /// other declarations. Still need to load a texture though.
        /// </summary>
        /// <param name="texture">Base texture parameter from Sprite.cs. Load a texture here.</param>
        public Enemy(Texture2D texture) : base(texture)
        {
        }

        /// <summary>
        /// Sets the velocity of the enemy to move torwards the player (as a unit vector) and have the correct
        /// rotated orientation towards them also.
        /// </summary>
        /// <param name="player"></param>
        public void FindPlayer(PlayerBase player)
        {
            vel = GetVector(player.pos);
            AimSprite(player.pos);
        }

        /// <summary>
        /// Custom update method for enemies. Was used in It Came From Back Pond so this will probably change.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        /// <param name="player">Player that should be attacked</param>
        /// <param name="world">World2D from class</param>
        public void Update(GameTime gameTime, PlayerBase player, World2D world)
        {
            pos += vel;
            drawRect.X = (int)pos.X;
            drawRect.Y = (int)pos.Y;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteTransform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(pos, 0.0f));
            rect = CalculateBoundingRectangle(rect, spriteTransform);
        }
    }
}
