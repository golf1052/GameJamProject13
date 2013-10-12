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

        Sprite ani;
        /// <summary>
        /// the player's faithful eagle companion
        /// </summary>
        Eagle attilla;

        /// <summary>
        /// the bullet that the player has
        /// </summary>
        Bullet gunshaver;

        Enemy testee;

        #region Map Particles
        List<Particle> particles = new List<Particle>();
        List<Particle> p_flagParticles = new List<Particle>();
        List<Particle> p_bloodParticles = new List<Particle>();
        List<Particle> p_fireGroundParticles = new List<Particle>();
        List<Particle> p_treeFireParticles = new List<Particle>();
        List<Particle> p_bushFireParticles = new List<Particle>();

        Texture2D particleTex;
        #endregion

        /// <summary>
        /// THE WORLD...BISH
        /// </summary>
        World world;

        Sprite background;

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

            dunkin = new Player(Content.Load<Texture2D>("PD_Stand_NoWep"), attilla, gunshaver);
            dunkin.pos = new Vector2(dunkin.tex.Width / 2 + 125, world.GROUND_HEIGHT - dunkin.tex.Height / 2);

            attilla = new Eagle(Content.Load<Texture2D>("Eagle"), 10, graphics);
            attilla.visible = false;

            gunshaver = new Bullet(Content.Load<Texture2D>("Bullet_Right"));
            gunshaver.visible = false;

            testee = new Republican(Content.Load<Texture2D>("Rep1_Stand"));
            testee.pos = new Vector2(testee.tex.Width / 2 + 500, world.GROUND_HEIGHT - testee.tex.Height / 2);

            particleTex = Content.Load<Texture2D>("flixel");

            background = new Sprite(Content.Load<Texture2D>("Level1_Chunk1"));
            background.origin = Vector2.Zero;
            world.worldObjects.Add(background);

            Texture2D aniX = Content.Load<Texture2D>("testspritesheet");
            List<Texture2D> aniList = new List<Texture2D>();
            aniList.Add(Content.Load<Texture2D>("testspritesheet"));
            ani = new Sprite(aniList, 10, 10, 5, 100, true, particleTex);
            ani.active = true;
            ani.alive = true;
            ani.visible = true;
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

            #region Map Specific Particles
            if (world.gameWindow.Contains((int)world.p_flagParticleSpawn.X, (int)world.p_flagParticleSpawn.Y))
            {
                p_flagParticles.Add(new Particle(particleTex, world.p_flagParticleSpawn, Color.Orange, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 90, 1.0f, 1.0f, Color.Gray, false));
                world.worldObjects.Add(p_flagParticles[p_flagParticles.Count - 1]);
            }

            if (world.gameWindow.Contains((int)world.p_bloodParticleSpawn.X, (int)world.p_bloodParticleSpawn.Y))
            {
                p_bloodParticles.Add(new Particle(particleTex, world.p_bloodParticleSpawn, Color.Red, 300, 500, new Rectangle(0, 0, 3, 3), 90, 7, 1.0f, 2.0f, Color.DarkRed, false));
                world.worldObjects.Add(p_bloodParticles[p_bloodParticles.Count - 1]);
            }

            if (world.gameWindow.Contains((int)world.p_fireGroundSpawn1.X, (int)world.p_fireGroundSpawn1.Y))
            {
                p_fireGroundParticles.Add(new Particle(particleTex, world.p_fireGroundSpawn1, Color.Orange, 1000, 2000, new Rectangle(0, 0, 5, 5), 270, 22, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_fireGroundParticles[p_fireGroundParticles.Count - 1]);
            }

            if (world.gameWindow.Contains((int)world.p_fireGroundSpawn2.X, (int)world.p_fireGroundSpawn2.Y))
            {
                p_fireGroundParticles.Add(new Particle(particleTex, world.p_fireGroundSpawn2, Color.Orange, 1000, 2000, new Rectangle(0, 0, 5, 5), 270, 22, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_fireGroundParticles[p_fireGroundParticles.Count - 1]);
            }

            if (world.gameWindow.Intersects(world.p_treeFireSpawn1))
            {
                p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn1.Left, world.p_treeFireSpawn1.Right), world.random.Next(world.p_treeFireSpawn1.Top, world.p_treeFireSpawn1.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            }

            if (world.gameWindow.Intersects(world.p_treeFireSpawn2))
            {
                p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn2.Left, world.p_treeFireSpawn2.Right), world.random.Next(world.p_treeFireSpawn2.Top, world.p_treeFireSpawn2.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            }

            if (world.gameWindow.Intersects(world.p_treeFireSpawn3))
            {
                p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn3.Left, world.p_treeFireSpawn3.Right), world.random.Next(world.p_treeFireSpawn3.Top, world.p_treeFireSpawn3.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            }

            if (world.gameWindow.Intersects(world.p_bushFireSpawn))
            {
                p_bushFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_bushFireSpawn.Left, world.p_bushFireSpawn.Right), world.random.Next(world.p_bushFireSpawn.Top, world.p_bushFireSpawn.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 20, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_bushFireParticles[p_bushFireParticles.Count - 1]);
            }
            #endregion

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                particles.Add(new Particle(particleTex, new Vector2(mouseState.X, mouseState.Y), world.sparkColors, 1000, 2000, new Rectangle(0, 0, 5, 5), 270, 22, 5.0f, 10.0f, Color.White, true));
            }
			
            dunkin.Move(keyboardState);

            dunkin.Jump(keyboardState, previousKeyboardState, world);

            dunkin.Update(gameTime, graphics);

            testee.move(dunkin);

            testee.Update(gameTime, graphics);

            ani.DefaultControlSprite(keyboardState, gamePadState, 5.0f);
            ani.Update(gameTime, graphics);

            if (background.pos.X > 0)
            {
                background.pos.X = 0;
            }

            if (background.pos.X + background.tex.Width < graphics.GraphicsDevice.Viewport.Width)
            {
                background.pos.X = -background.tex.Width + graphics.GraphicsDevice.Viewport.Width;
            }

            if (dunkin.pos.X > 400 - dunkin.tex.Width / 2)
            {
                dunkin.pos.X = 400  - dunkin.tex.Width / 2;

                //if (backgrounds[backgrounds.Count - 1].pos.X + backgrounds[backgrounds.Count - 1].tex.Width > graphics.GraphicsDevice.Viewport.Width)
                //{
                //    backgrounds[0].pos.X -= 1.0f;
                //}

                if (background.pos.X + background.tex.Width > graphics.GraphicsDevice.Viewport.Width)
                {
                    world.MoveWorld(new Vector2(-5.0f, 0.0f));
                }
            }

            if (dunkin.pos.X < 124 + dunkin.tex.Width / 2)
            {
                dunkin.pos.X = 124 + dunkin.tex.Width / 2;

                //if (backgrounds[0].pos.X < 0)
                //{
                //    backgrounds[0].pos.X += 1.0f;
                //}

                if (background.pos.X < 0)
                {
                    world.MoveWorld(new Vector2(5.0f, 0.0f));
                }
            }

            //foreach (Background background in backgrounds)
            //{
            //    background.Update(gameTime, graphics);
            //}

            #region Update Particle Code
            foreach (Particle particle in particles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.2f, 0.1f, world);
            }

            foreach (Particle particle in p_flagParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.05f, 0.1f, world);
            }

            foreach (Particle particle in p_bloodParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 0.999f, 0.0005f, 0.5f, world);
            }

            foreach (Particle particle in p_fireGroundParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.0005f, 0.5f, world);
            }

            foreach (Particle particle in p_treeFireParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.0005f, 0.5f, world);
            }

            foreach (Particle particle in p_bushFireParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.005f, 0.3f, world);
            }
            #endregion

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

            background.Draw(spriteBatch);

            #region Particle Draw Code
            foreach (Particle particle in particles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_flagParticles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_bloodParticles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_fireGroundParticles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_treeFireParticles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_bushFireParticles)
            {
                particle.DrawWithRect(spriteBatch);
            }
            #endregion

            if (dunkin.facing == Player.Facing.Left)
            {
                spriteBatch.Draw(dunkin.tex, dunkin.pos, null, dunkin.color, dunkin.rotation, dunkin.origin, dunkin.scale, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                spriteBatch.Draw(dunkin.tex, dunkin.pos, null, dunkin.color, dunkin.rotation, dunkin.origin, dunkin.scale, SpriteEffects.None, 0);
            }

            if (attilla.visible)
            {
                attilla.Draw(spriteBatch);
            }

            if (gunshaver.visible)
            {
                gunshaver.Draw(spriteBatch);
            }

            testee.Draw(spriteBatch);

            ani.DrawAnimation(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
