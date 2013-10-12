using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;

namespace GameSlamProject
{
	/// <summary>
	/// TextItem.cs: Allows easier handling of SpriteFonts displayed on the screen.
	/// </summary>
    public class TextItem : Sprite
    {
        /// <summary>
        /// The text that should be displayed
        /// </summary>
        public string text;

        /// <summary>
        /// The font that the text is in
        /// </summary>
        public SpriteFont font;

        /// <summary>
        /// Makes a new TextItem
        /// </summary>
        /// <param name="loadedFont">The SpriteFont that the text item should have</param>
        public TextItem(SpriteFont loadedFont, string spriteText) : base(loadedFont)
        {
            font = loadedFont;
            text = spriteText;
            pos = Vector2.Zero;
            rect = new Rectangle((int)pos.X, (int)pos.Y, (int)MeasureString().X, (int)MeasureString().Y);
            vel = Vector2.Zero;
            collision = false;
            color = Color.White;
            scale = 1.0f;
            rotation = 0.0f;
        }

        /// <summary>
        /// Makes a new TextItem and sets all usefull properties
        /// </summary>
        /// <param name="loadedFont">The SpriteFont that the text item should have</param>
        /// <param name="loadedText">The text that should be displayed</param>
        /// <param name="position">The position that the text item should initiall have, use the GetDefaultLocation <see cref="Menu.cs"/> </param>
        public TextItem(SpriteFont loadedFont, string spriteText, Vector2 position) : base(loadedFont)
        {
            font = loadedFont;
            pos = position;
            text = spriteText;
            rect = new Rectangle((int)pos.X, (int)pos.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
            vel = Vector2.Zero;
            collision = false;
            color = Color.White;
            scale = 1.0f;
            rotation = 0.0f;
        }

        /// <summary>
        /// Measures the text item, easier and shorter than spritefont.MeasureString(text)
        /// </summary>
        /// <returns>MeasureString from the SpriteFont</returns>
        public Vector2 MeasureString()
        {
            return font.MeasureString(text);
        }

        /// <summary>
        /// Draws the TextItem on the screen.
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.DrawString Method (SpriteFont, StringBuilder, Vector2, Color, Single, Vector2, Single, SpriteEffects, Single)"/>
        /// </summary>
        /// <param name="spritebatch">SpriteBatch from class</param>
        public void DrawString(SpriteBatch spritebatch)
        {
            spritebatch.DrawString(font, text, pos, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Updates the TextItem. Just updates the rect because usually sprites do not move. May add more update functionality.
        /// </summary>
        public void UpdateString()
        {
            rect = new Rectangle((int)pos.X, (int)pos.Y, (int)MeasureString().X, (int)MeasureString().Y);
        }
    }
}
