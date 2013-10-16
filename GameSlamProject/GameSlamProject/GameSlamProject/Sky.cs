using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class Sky : Sprite
    {
        public List<Color> fadeToList = new List<Color>();
        public float fadeRate = 0.0f;
        public int currentColor = 0;

        public Sky(Texture2D loadedTex)
            : base(loadedTex)
        {
            fadeToList.Add(Color.LightSkyBlue);
            fadeToList.Add(Color.SkyBlue);
            fadeToList.Add(Color.DeepSkyBlue);
            fadeToList.Add(Color.CornflowerBlue);
            fadeToList.Add(Color.DarkBlue);
            fadeToList.Add(Color.MidnightBlue);
            fadeToList.Add(Color.Black);

            color = fadeToList[0];
        }

        public void Fade()
        {
            if (fadeRate >= 1.0f)
            {
                fadeRate = 0.0f;
                if (currentColor < fadeToList.Count - 1)
                {
                    currentColor++;
                }
            }

            if (currentColor < fadeToList.Count - 1)
            {
                color = Color.Lerp(fadeToList[currentColor], fadeToList[currentColor + 1], fadeRate);
            }
            else
            {
                color = fadeToList[6];
            }
        }
    }
}
