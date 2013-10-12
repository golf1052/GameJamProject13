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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Previous input states
        KeyboardState previousKeyboardState = Keyboard.GetState();
        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
        MouseState previousMouseState = Mouse.GetState();

        Player dunkin;
        List<Particle> particles = new List<Particle>();
        Texture2D particleTex;

        /// <summary>
        /// THE WORLD...BISH
        /// </summary>
        World world;

        Texture2D backgroundTest;

        List<Background> backgrounds = new List<Background>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Window size
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            world = new World(graphics);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgrounds.Add(new Background(Content.Load<Texture2D>("testbackground1")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("testbackground2")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("testbackground3")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("testbackground4")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("testbackground5")));

            world.GenerateBackgroundList(backgrounds);

            dunkin = new Player(Content.Load<Texture2D>("PD_Stand_NoWep"));
            dunkin.pos = new Vector2(dunkin.tex.Width / 2 + 125, graphics.GraphicsDevice.Viewport.Height - dunkin.tex.Height / 2);

            particleTex = Content.Load<Texture2D>("flixel");

            backgroundTest = Content.Load<Texture2D>("Level1_Chunk1");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Get new states of inputs
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();
			
            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                particles.Add(new Particle(particleTex, new Vector2(mouseState.X, mouseState.Y), Color.BurlyWood, 1000, 2000, new Rectangle(0, 0, 5, 5)));
            }
			
            dunkin.Move(keyboardState);

            dunkin.Update(gameTime, graphics);

            if (dunkin.pos.X > 900 - dunkin.tex.Width / 2)
            {
                dunkin.pos.X = 900  - dunkin.tex.Width / 2;

                if (backgrounds[backgrounds.Count - 1].pos.X + backgrounds[backgrounds.Count - 1].tex.Width > graphics.GraphicsDevice.Viewport.Width)
                {
                    backgrounds[0].pos.X -= 1.0f;
                }
            }

            if (dunkin.pos.X < 124 + dunkin.tex.Width / 2)
            {
                dunkin.pos.X = 124 + dunkin.tex.Width / 2;

                if (backgrounds[0].pos.X < 0)
                {
                    backgrounds[0].pos.X += 1.0f;
                }
            }

            foreach (Background background in backgrounds)
            {
                background.Update(gameTime, graphics);
            }

            foreach (Particle particle in particles)
            {
                particle.UpdateParticle(gameTime, graphics);
            }

            // Set previous states to current states
            previousKeyboardState = keyboardState;
            previousGamePadState = gamePadState;
            previousMouseState = mouseState;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            /*
             * Things draw from top to bottom so in the list:
             * - background
             * - player
             * - powerup
             * Background will draw first, player will draw over the background
             * and the powerup will draw over the background and the player
             */
            spriteBatch.Begin();

            // ALL DRAW CODE GOES IN HERE

            foreach (Background background in backgrounds)
            {
                background.Draw(spriteBatch);
            }

            if (dunkin.facing == Player.Facing.Left)
            {
                spriteBatch.Draw(dunkin.tex, dunkin.pos, null, dunkin.color, dunkin.rotation, dunkin.origin, dunkin.scale, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(dunkin.tex, dunkin.pos, null, dunkin.color, dunkin.rotation, dunkin.origin, dunkin.scale, SpriteEffects.None, 0);
            }

            foreach (Particle particle in particles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
