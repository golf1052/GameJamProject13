using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameSlamProject
{
    public class Flixel : Sprite
    {
        public Rectangle playerWindow;
        public int playerNumber;
        public bool playing;
        public float worldBottom;
        public int positon = 0;
        public int score;
        public TextItem t_position;
        public TextItem t_score;

        public enum Side
        {
            Left,
            Right,
            Center
        }

        public Side side = Side.Center;
        List<Color> fireList = new List<Color>();
        List<Color> redList = new List<Color>();
        List<Color> orangeList = new List<Color>();
        List<Color> yellowList = new List<Color>();
        List<Color> greenList = new List<Color>();
        List<Color> blueList = new List<Color>();
        List<Color> purpleList = new List<Color>();
        List<Color> whiteList = new List<Color>();
        List<Color> blackList = new List<Color>();
        List<Color> pinkList = new List<Color>();
        int randomSeed = 0;
        public Star[] stars = new Star[100];
        int starCount = 0;
        public bool exploding = false;
        public Sky sky;
        public List<List<Color>> colorLists = new List<List<Color>>();
        public int selectedColor = 0;
        public List<string> nameOfColors = new List<string>();

        public Flixel(Texture2D loadedTex)
            : base(loadedTex)
        {
            playing = false;
            drawRect = new Rectangle(0, 0, 10, 10);
            score = 0;
            #region Color Lists
            #region Fire Colors
            fireList.Add(Color.Red);
            fireList.Add(Color.OrangeRed);
            fireList.Add(Color.Orange);
            fireList.Add(Color.Yellow);
            fireList.Add(Color.LightYellow);
            fireList.Add(Color.LightGoldenrodYellow);
            fireList.Add(Color.White);
            fireList.Add(Color.CornflowerBlue);
            #endregion

            #region Red List
            redList.Add(Color.Red);
            redList.Add(Color.OrangeRed);
            redList.Add(Color.DarkRed);
            redList.Add(Color.IndianRed);
            redList.Add(Color.MediumVioletRed);
            redList.Add(Color.PaleVioletRed);
            #endregion

            #region Orange List
            orangeList.Add(Color.Orange);
            orangeList.Add(Color.DarkOrange);
            orangeList.Add(Color.DarkOrange);
            orangeList.Add(Color.Coral);
            #endregion

            #region Yellow List
            yellowList.Add(Color.Yellow);
            yellowList.Add(Color.GreenYellow);
            yellowList.Add(Color.LightGoldenrodYellow);
            yellowList.Add(Color.LightYellow);
            yellowList.Add(Color.YellowGreen);
            yellowList.Add(Color.Gold);
            yellowList.Add(Color.DarkGoldenrod);
            yellowList.Add(Color.Goldenrod);
            yellowList.Add(Color.PaleGoldenrod);
            yellowList.Add(Color.Cornsilk);
            #endregion

            #region Green List
            greenList.Add(Color.DarkGreen);
            greenList.Add(Color.DarkOliveGreen);
            greenList.Add(Color.DarkSeaGreen);
            greenList.Add(Color.ForestGreen);
            greenList.Add(Color.Green);
            greenList.Add(Color.GreenYellow);
            greenList.Add(Color.LawnGreen);
            greenList.Add(Color.LightGreen);
            greenList.Add(Color.LightSeaGreen);
            greenList.Add(Color.LimeGreen);
            greenList.Add(Color.MediumSeaGreen);
            greenList.Add(Color.MediumSpringGreen);
            greenList.Add(Color.PaleGreen);
            greenList.Add(Color.SeaGreen);
            greenList.Add(Color.SpringGreen);
            greenList.Add(Color.YellowGreen);
            #endregion

            #region Blue List
            blueList.Add(Color.AliceBlue);
            blueList.Add(Color.Blue);
            blueList.Add(Color.BlueViolet);
            blueList.Add(Color.CadetBlue);
            blueList.Add(Color.CornflowerBlue);
            blueList.Add(Color.DarkBlue);
            blueList.Add(Color.DarkSlateBlue);
            blueList.Add(Color.DeepSkyBlue);
            blueList.Add(Color.DodgerBlue);
            blueList.Add(Color.LightBlue);
            blueList.Add(Color.LightSkyBlue);
            blueList.Add(Color.LightSteelBlue);
            blueList.Add(Color.MediumBlue);
            blueList.Add(Color.MediumSlateBlue);
            blueList.Add(Color.MidnightBlue);
            blueList.Add(Color.PowderBlue);
            blueList.Add(Color.RoyalBlue);
            blueList.Add(Color.SkyBlue);
            blueList.Add(Color.SlateBlue);
            blueList.Add(Color.SteelBlue);
            blueList.Add(Color.Navy);
            #endregion

            #region Purple List
            purpleList.Add(Color.Purple);
            purpleList.Add(Color.MediumPurple);
            purpleList.Add(Color.Indigo);
            purpleList.Add(Color.Violet);
            purpleList.Add(Color.BlueViolet);
            purpleList.Add(Color.DarkViolet);
            purpleList.Add(Color.Crimson);
            #endregion

            #region White List
            whiteList.Add(Color.White);
            whiteList.Add(Color.AntiqueWhite);
            whiteList.Add(Color.FloralWhite);
            whiteList.Add(Color.GhostWhite);
            whiteList.Add(Color.NavajoWhite);
            whiteList.Add(Color.WhiteSmoke);
            whiteList.Add(Color.AliceBlue);
            #endregion

            #region Black List
            blackList.Add(Color.Black);
            blackList.Add(Color.DarkGray);
            blackList.Add(Color.DarkSlateGray);
            blackList.Add(Color.DimGray);
            blackList.Add(Color.Gray);
            blackList.Add(Color.LightGray);
            blackList.Add(Color.LightSlateGray);
            blackList.Add(Color.SlateGray);
            #endregion

            #region Pink List
            pinkList.Add(Color.DeepPink);
            pinkList.Add(Color.HotPink);
            pinkList.Add(Color.LightPink);
            pinkList.Add(Color.Pink);
            pinkList.Add(Color.Magenta);
            #endregion
            #endregion

            colorLists.Add(fireList);
            colorLists.Add(redList);
            colorLists.Add(orangeList);
            colorLists.Add(yellowList);
            colorLists.Add(greenList);
            colorLists.Add(blueList);
            colorLists.Add(purpleList);
            colorLists.Add(whiteList);
            colorLists.Add(blackList);
            colorLists.Add(pinkList);

            nameOfColors.Add("fire");
            nameOfColors.Add("red");
            nameOfColors.Add("orange");
            nameOfColors.Add("yellow");
            nameOfColors.Add("green");
            nameOfColors.Add("blue");
            nameOfColors.Add("purple");
            nameOfColors.Add("white");
            nameOfColors.Add("black");
            nameOfColors.Add("pink");
            sky = new Sky(loadedTex);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sky.DrawWithRect(spriteBatch);

            foreach (Star star in stars)
            {
                if (star.alive == true)
                {
                    star.DrawWithRect(spriteBatch);
                }
            }

            foreach (Particle particle in particles)
            {
                if (particle.alive == true)
                {
                    particle.DrawWithRect(spriteBatch);
                }
            }

            t_score.DrawString(spriteBatch);
            t_position.DrawString(spriteBatch);

            if (alive == true)
            {
                DrawWithRect(spriteBatch);
            }
        }

        public void SetUp(int numOfPlayers, GraphicsDeviceManager graphics, int number)
        {
            playerNumber = number;
            // Nat wrote this line. Fuck him.
            playerWindow.X = (graphics.GraphicsDevice.Viewport.Width / numOfPlayers) * (playerNumber - 1);
            playerWindow.Y = 0;
            playerWindow.Width = graphics.GraphicsDevice.Viewport.Width / numOfPlayers;
            playerWindow.Height = graphics.GraphicsDevice.Viewport.Height;

            // Nat wrote this line. Fuck him.
            pos = new Vector2(playerWindow.X + playerWindow.Width / 2, graphics.GraphicsDevice.Viewport.Height - drawRect.Height - 20);
            worldBottom = graphics.GraphicsDevice.Viewport.Height - drawRect.Height - 20;
            sky.drawRect = playerWindow;
        }

        public void Push(KeyboardState keyboardState, KeyboardState previousKeyboardState, Keys key, GameTime gameTime, GraphicsDeviceManager graphics, float gravityRate)
        {
            Random random = new Random(randomSeed);
            randomSeed = random.Next();

            t_position.text = positon.ToString();
            t_position.pos = new Vector2(playerWindow.X + playerWindow.Width / 2 - t_position.MeasureString().X, t_score.pos.Y - t_position.MeasureString().Y);

            foreach (Particle particle in particles)
            {
                if (exploding == false)
                {
                    if (particle.alive == false)
                    {
                        int size = random.Next(3, 6);
                        particle.SpawnParticle(new Vector2(pos.X + 5, pos.Y + 7), colorLists[selectedColor], 2000, 3000, new Rectangle(0, 0, size, size), 90, 7, 5.0f, 10.0f, Color.Gray, true, randomSeed);
                        break;
                    }
                }
                else
                {
                    if (particle.alive == false)
                    {
                        int size = random.Next(3, 6);
                        particle.SpawnParticle(new Vector2(pos.X + 5, pos.Y + 7), colorLists[selectedColor], 2000, 3000, new Rectangle(0, 0, size, size), 270, 180, 2.0f, 5.0f, Color.Gray, false, randomSeed);
                    }
                }
            }

            foreach (Particle particle in particles)
            {
                if (exploding == false)
                {
                    particle.UpdateParticle(gameTime, graphics, 1.0f, 0.1f, 0.1f, gravityRate, playerWindow);
                }
                else
                {
                    particle.gravity = false;
                    particle.UpdateParticle(gameTime, graphics, 1.0f, 0.1f, 0.1f, 1.5f, playerWindow);
                }
            }

            if (starCount >= 1)
            {
                starCount = 0;
                foreach (Star star in stars)
                {
                    if (star.alive == false)
                    {
                        star.SpawnStar(playerWindow, randomSeed);
                        break;
                    }
                }
            }

            foreach (Star star in stars)
            {
                star.UpdateStar(gameTime, graphics);
            }

            if (exploding == true)
            {
                vel.X = 0;
            }

            if (alive == true)
            {
                if (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                {
                    starCount++;
                    sky.fadeRate += 0.04f;
                    score += 1;
                    t_score.text = score.ToString();
                    t_score.pos = new Vector2(playerWindow.X + playerWindow.Width / 2 - t_score.MeasureString().X, graphics.GraphicsDevice.Viewport.Height - t_score.MeasureString().Y);
                    if (score < 50)
                    {
                        pos.Y -= 5.0f;
                    }
                    else if (score < 100)
                    {
                        pos.Y -= 4.0f;
                    }
                    else if (score < 150)
                    {
                        pos.Y -= 2.0f;
                    }
                    else
                    {
                        pos.Y -= 1.0f;
                    }

                    if (side == Side.Center)
                    {
                        side = (Side)random.Next(0, 2);
                    }

                    if (side == Side.Left)
                    {
                        side = Side.Right;
                        vel.X = 1.0f;
                    }
                    else if (side == Side.Right)
                    {
                        side = Side.Left;
                        vel.X = -1.0f;
                    }
                }
            }

            sky.Fade();

            if (pos.X < playerWindow.X + 25)
            {
                pos.X = playerWindow.X + 25;
            }

            if (pos.X > playerWindow.X + playerWindow.Width - 25)
            {
                pos.X = playerWindow.X + playerWindow.Width - 25;
            }

            if (pos.Y < 100)
            {
                pos.Y = 100;
            }
        }
    }
}
