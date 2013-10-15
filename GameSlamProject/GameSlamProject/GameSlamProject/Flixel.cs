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
        List<Color> colorList = new List<Color>();
        int randomSeed = 0;
        public Star[] stars = new Star[100];
        int starCount = 0;
        public bool exploding = false;

        public Flixel(Texture2D loadedTex)
            : base(loadedTex)
        {
            playing = false;
            drawRect = new Rectangle(0, 0, 10, 10);
            score = 0;
            colorList.Add(Color.Red);
            colorList.Add(Color.OrangeRed);
            colorList.Add(Color.Orange);
            colorList.Add(Color.Yellow);
            colorList.Add(Color.LightYellow);
            colorList.Add(Color.LightGoldenrodYellow);
            colorList.Add(Color.White);
            colorList.Add(Color.CornflowerBlue);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
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
        }

        public void Push(KeyboardState keyboardState, KeyboardState previousKeyboardState, Keys key, GameTime gameTime, GraphicsDeviceManager graphics, float gravityRate)
        {
            Random random = new Random(randomSeed);
            randomSeed = random.Next();

            t_score.pos = new Vector2(playerWindow.X + playerWindow.Width / 2 - t_score.MeasureString().X, graphics.GraphicsDevice.Viewport.Height - t_score.MeasureString().Y);
            t_position.pos = new Vector2(playerWindow.X + playerWindow.Width / 2 - t_position.MeasureString().X, t_score.pos.Y - t_position.MeasureString().Y);
            t_position.text = positon.ToString();

            if (alive == true)
            {
                if (pos.Y > graphics.GraphicsDevice.Viewport.Height - drawRect.Height - 20)
                {
                    pos.Y = graphics.GraphicsDevice.Viewport.Height - drawRect.Height - 15;
                    vel.Y = 0;
                }
                else
                {
                    vel.Y += 0.5f;
                }

                if (vel.Y > 2.0f)
                {
                    vel.Y = 2.0f;
                }
            }
            else
            {
                vel.Y = 0;
            }

            foreach (Particle particle in particles)
            {
                if (exploding == false)
                {
                    if (particle.alive == false)
                    {
                        int size = random.Next(3, 6);
                        particle.SpawnParticle(new Vector2(pos.X + 5, pos.Y + 7), colorList, 2000, 3000, new Rectangle(0, 0, size, size), 90, 7, 5.0f, 10.0f, Color.Gray, true, randomSeed);
                        break;
                    }
                }
                else
                {
                    if (particle.alive == false)
                    {
                        int size = random.Next(3, 6);
                        particle.SpawnParticle(new Vector2(pos.X + 5, pos.Y + 7), new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble()), 2000, 3000, new Rectangle(0, 0, size, size), 270, 180, 15.0f, 20.0f, Color.Gray, true, randomSeed);
                        break;
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
                    particle.UpdateParticle(gameTime, graphics, 1.0f, 0.1f, 0.1f, 1.0f, playerWindow);
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

            if (alive == true)
            {
                if (keyboardState.IsKeyDown(key) && previousKeyboardState.IsKeyUp(key))
                {
                    starCount++;
                    score += 1;
                    t_score.text = score.ToString();
                    pos.Y -= 5.0f;
                    vel.Y = -2.0f;

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
