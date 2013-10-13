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
        Texture2D republicanTex;

        List<SpriteSheet> repEnemySpriteSheets = new List<SpriteSheet>();

        Enemy testee2;

        Sprite healthBar;
        SpriteFont HUDfont;
        TextItem healthText;

        /// <summary>
        /// the player's faithful eagle companion
        /// </summary>
        Eagle attilla;

        /// <summary>
        /// the bullet that the player has
        /// </summary>
        Bullet gunshaver;

        /*
         * HERE LIES TESTEE
         * RESTEE TESTEE
         */

        Enemy corporateFatCat;

        #region Map Particles
        List<Particle> particles = new List<Particle>();
        List<Particle> p_bloodParticles = new List<Particle>();
        List<Particle> p_treeFireParticles = new List<Particle>();
        List<Particle> p_bushFireParticles = new List<Particle>();

        Texture2D particleTex;
        #endregion

        /// <summary>
        /// THE WORLD...BISH
        /// </summary>
        World world;

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

            #region Load Backgrounds
            backgrounds.Add(new Background(Content.Load<Texture2D>("Level1_Chunk1")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("Level1_Chunk2")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("Level1_Chunk2")));
            backgrounds.Add(new Background(Content.Load<Texture2D>("Level1_Chunk2")));

            world.GenerateBackgroundList(backgrounds);
            #endregion

            #region Load Player
            List<SpriteSheet> dunkinSpriteSheets = new List<SpriteSheet>();
            dunkinSpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("PD_Stand_NoWep"), 187, 286, 1, 0, true));
            dunkinSpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("PD_Test1_NoWep_cleansheet"), 241, 238, 3, 200, true));
            dunkin = new Player(dunkinSpriteSheets, attilla, gunshaver);
            dunkin.origin = new Vector2(dunkin.spriteSheets[0].tex.Width / 2, dunkin.spriteSheets[0].tex.Height / 2);
            dunkin.pos = new Vector2(dunkin.spriteSheets[0].tex.Width + 125, world.GROUND_HEIGHT - dunkin.spriteSheets[0].tex.Height / 2);
            #endregion

            repEnemySpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("Fat_Cat"), 60, 146, 1, 0, true));
            //repEnemySpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("Rep_Run_cleansheet"), 70, 148, 8, 75, true));
            repEnemySpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("FatCat_Run"), 450, 450, 6, 200, true));
            //testee2 = new Enemy(repEnemySpriteSheets, 300, 3.0f, 7.0f, world);
            //testee2.pos = new Vector2(testee2.tex.Width + 500, world.GROUND_HEIGHT - testee2.tex.Height);
            //testee2.pos = new Vector2(testee2.tex.Width + 500, world.GROUND_HEIGHT - testee2.tex.Height);
            //testee2.vel = new Vector2(5.0f, 0.0f);
            //testee2.isAnimatable = true;
            //world.enemyList.Add(testee2);

            attilla = new Eagle(Content.Load<Texture2D>("therealeagle"), 10, graphics);
            attilla.visible = false;
            dunkin.myEagle = attilla;

            gunshaver = new Bullet(Content.Load<Texture2D>("bullet"));
            gunshaver.visible = false;
            dunkin.myBullet = gunshaver;

            //testee = new Republican(Content.Load<Texture2D>("Rep1_Stand"));
            //testee.pos = new Vector2(testee.tex.Width / 2 + 500, world.GROUND_HEIGHT - testee.tex.Height / 2);
            //testee.vel = new Vector2(5.0f, 0.0f);
            //world.enemyList.Add(testee);

            corporateFatCat = new Enemy(repEnemySpriteSheets, 300, 2.0f, 2.0f, world);
            corporateFatCat.alive = true;
            corporateFatCat.visible = true;
            corporateFatCat.isAnimatable = true;
            corporateFatCat.pos = new Vector2(corporateFatCat.tex.Width / 2 + 5000, world.GROUND_HEIGHT - corporateFatCat.tex.Height / 2);
            //corporateFatCat.pos.X = 500;
            corporateFatCat.vel = new Vector2(-2.0f, 0.0f);
            world.enemyList.Add(corporateFatCat);
            
            particleTex = Content.Load<Texture2D>("flixel");
            republicanTex = Content.Load<Texture2D>("Rep1_Stand");
            
            HUDfont = Content.Load<SpriteFont>("StatusFont");
            healthBar = new Sprite(Content.Load<Texture2D>("flixel"));
            healthBar.origin = Vector2.Zero;
            healthBar.pos = new Vector2(50, 50);
            healthBar.drawRect = new Rectangle((int)healthBar.pos.X, (int)healthBar.pos.Y, 200, 20);
            healthBar.color = Color.Red;

            healthText = new TextItem(HUDfont, dunkin.health.ToString());
            healthText.origin = Vector2.Zero;
            healthText.pos = new Vector2(healthBar.pos.X + healthBar.drawRect.Width + 20, healthBar.pos.Y - healthText.MeasureString().Y / 3);
            healthText.color = Color.Red;     
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
            #region Input Initalizations
            // Get new states of inputs
            KeyboardState keyboardState = Keyboard.GetState();
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            MouseState mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }
            #endregion

            #region Map Specific Particles
            //if (world.gameWindow.Contains((int)world.p_bloodParticleSpawn.X, (int)world.p_bloodParticleSpawn.Y))
            //{
            //    p_bloodParticles.Add(new Particle(particleTex, world.p_bloodParticleSpawn, Color.Red, 300, 500, new Rectangle(0, 0, 3, 3), 90, 7, 1.0f, 2.0f, Color.DarkRed, false));
            //    world.worldObjects.Add(p_bloodParticles[p_bloodParticles.Count - 1]);
            //}

            //if (world.gameWindow.Intersects(world.p_treeFireSpawn1))
            //{
            //    p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn1.Left, world.p_treeFireSpawn1.Right), world.random.Next(world.p_treeFireSpawn1.Top, world.p_treeFireSpawn1.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
            //    world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            //}

            //if (world.gameWindow.Intersects(world.p_treeFireSpawn2))
            //{
            //    p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn2.Left, world.p_treeFireSpawn2.Right), world.random.Next(world.p_treeFireSpawn2.Top, world.p_treeFireSpawn2.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
            //    world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            //}

            //if (world.gameWindow.Intersects(world.p_treeFireSpawn3))
            //{
            //    p_treeFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_treeFireSpawn3.Left, world.p_treeFireSpawn3.Right), world.random.Next(world.p_treeFireSpawn3.Top, world.p_treeFireSpawn3.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 10, 1.0f, 1.5f, Color.Gray, false));
            //   world.worldObjects.Add(p_treeFireParticles[p_treeFireParticles.Count - 1]);
            //}

            if (world.gameWindow.Intersects(world.p_bushFireSpawn))
            {
                p_bushFireParticles.Add(new Particle(particleTex, new Vector2(world.random.Next(world.p_bushFireSpawn.Left, world.p_bushFireSpawn.Right), world.random.Next(world.p_bushFireSpawn.Top, world.p_bushFireSpawn.Bottom)), world.fireColors, 500, 1000, new Rectangle(0, 0, 5, 5), 270, 20, 1.0f, 1.5f, Color.Gray, false));
                world.worldObjects.Add(p_bushFireParticles[p_bushFireParticles.Count - 1]);
            }

            //if (mouseState.LeftButton == ButtonState.Pressed)
            //{
            //    particles.Add(new Particle(particleTex, new Vector2(mouseState.X, mouseState.Y), world.sparkColors, 1000, 2000, new Rectangle(0, 0, 5, 5), 270, 22, 5.0f, 10.0f, Color.White, true));
            //}
            #endregion

            //does not work because the bg is changed
            //if (world.gameWindow.Intersects(world.start))
            //{
            //    world.bossList.Add(corporateFatCat);
            //}

            this.dunkin.RevertPlayer();
            if (dunkin.pupDuration != 0)
            {
                dunkin.pupDuration = dunkin.pupDuration - 1;
            }
            
            for (int i = 0; i < world.enemyList.Count(); i++)
            {
                world.enemyList[i].collide(dunkin);
                healthText.UpdateString();
            }

            healthText.text = dunkin.health.ToString();
            healthBar.drawRect = new Rectangle((int)healthBar.pos.X, (int)healthBar.pos.Y, dunkin.health * 2, 20);
            

            #region Bullet Stuff
            dunkin.myBullet.offScreen(dunkin, world);
            dunkin.myBullet.moveBullet(dunkin);
            dunkin.myBullet.hitEnemies(world.enemyList, dunkin);
            #endregion

            world.enemyList.Add(new Enemy(republicanTex, 300, 3.0f, 7.0f, world));
            foreach (Enemy e in world.enemyList)
            {
                if (e != corporateFatCat)
                {
                    e.isAnimatable = false;
                }
                else
                {
                    foreach (SpriteSheet sheet in e.spriteSheets)
                    {
                        sheet.pos = e.pos;
                    }
                }
            }
            //world.enemyList[world.enemyList.Count - 1].pos = new Vector2(world.enemyList[world.enemyList.Count - 1].tex.Width / 2 + 500, world.GROUND_HEIGHT - world.enemyList[world.enemyList.Count - 1].tex.Height / 2);

            //THIS IS HOW YOU DIE
            if (dunkin.health <= 0)
            {
                this.Exit();
            }

            /*
             * This comment must remain here till the end of time
             * Here used to lie peniswalls
             * The best List<Enemy> known to man
             * RIP in penis
             */

            #region Duncan's Fun House 2013 (Dunkin update stuff)

            dunkin.Move(keyboardState);
            dunkin.Jump(keyboardState, previousKeyboardState, world);
            dunkin.Attack(keyboardState, previousKeyboardState, world.enemyList);
            dunkin.myEagle.Update(gameTime, graphics);
            if (dunkin.animationState == Player.Animations.Idle)
            {
                dunkin.Update(gameTime, graphics, 0);
            }
            if (dunkin.animationState == Player.Animations.Running)
            {
                dunkin.Update(gameTime, graphics, 1);
            }
            foreach (SpriteSheet sheet in dunkin.spriteSheets)
            {
                sheet.pos = dunkin.pos;
            }
            #endregion

            //corporateFatCat.Update(gameTime, dunkin, world, 0);

            #region Background Code
            if (dunkin.pos.X > graphics.GraphicsDevice.Viewport.Width / 2 + 200 - dunkin.tex.Width / 2)
            {
                dunkin.pos.X = graphics.GraphicsDevice.Viewport.Width / 2 + 200 - dunkin.tex.Width / 2;

                if (backgrounds[backgrounds.Count - 1].pos.X + backgrounds[backgrounds.Count - 1].tex.Width > graphics.GraphicsDevice.Viewport.Width)
                {
                    backgrounds[0].pos.X -= 5.0f;
                    world.MoveWorld(new Vector2(-5.0f, 0.0f));
                }
            }

            //if (world.gameWindow.Intersects(world.boss))
            //{
            //    corporateFatCat.alive = true;
            //    corporateFatCat.visible = true;
            //    //world.enemyList.Add(corporateFatCat);
            //}

            if (dunkin.pos.X < graphics.GraphicsDevice.Viewport.Width / 2 - 200 + dunkin.tex.Width / 2)
            {
                dunkin.pos.X = graphics.GraphicsDevice.Viewport.Width / 2 - 200 + dunkin.tex.Width / 2;

                if (backgrounds[0].pos.X < 0)
                {
                    backgrounds[0].pos.X += 5.0f;
                    world.MoveWorld(new Vector2(-5.0f, 0.0f));
                }
            }

            foreach (Background bg in backgrounds)
            {
                bg.Update(gameTime, graphics);
                bg.Update(gameTime, graphics, 0);
            }

#endregion

            foreach (Enemy e in world.enemyList)
            {
                //if (e.animationState == Enemy.Animations.Running)
                //{
                //    e.Update(gameTime, dunkin, world, 1);
                //}
                //else
                //{
                //    e.Update(gameTime, dunkin, world, 0);
                //}
                if (e != corporateFatCat)
                {
                    e.Update(gameTime, dunkin, world, 0);
                }
                else
                {
                    e.Update(gameTime, dunkin, world, 1);
                }
            }

            #region Update Particle Code
            foreach (Particle particle in particles)
            {
                particle.UpdateParticle(gameTime, graphics, 1.0f, 0.2f, 0.1f, world);
            }

            foreach (Particle particle in p_bloodParticles)
            {
                particle.UpdateParticle(gameTime, graphics, 0.999f, 0.0005f, 0.5f, world);
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

            #region End of Update Code
            // Set previous states to current states
            previousKeyboardState = keyboardState;
            previousGamePadState = gamePadState;
            previousMouseState = mouseState;

            base.Update(gameTime);
            #endregion
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

            foreach (Background bg in backgrounds)
            {
                bg.Draw(spriteBatch);
            }
            

            #region Particle Draw Code
            foreach (Particle particle in particles)
            {
                particle.DrawWithRect(spriteBatch);
            }

            foreach (Particle particle in p_bloodParticles)
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

            #region Gun Draw Code
            if (gunshaver.visible)
            {
                if (dunkin.facing == Player.Facing.Left)
                {
                    spriteBatch.Draw(gunshaver.tex, gunshaver.pos, null, gunshaver.color, gunshaver.rotation, gunshaver.origin, gunshaver.scale, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(gunshaver.tex, gunshaver.pos, null, gunshaver.color, gunshaver.rotation, gunshaver.origin, gunshaver.scale, SpriteEffects.None, 0);
                }
            }
            #endregion

            if (corporateFatCat.visible == true)
            {
                if (corporateFatCat.alive == true)
                {
                    corporateFatCat.Draw(spriteBatch);
                }
            }

            #region Player Draw Code
            if (dunkin.animationState == Player.Animations.Idle)
            {
                dunkin.Draw(spriteBatch, 0);
            }
            else if (dunkin.animationState == Player.Animations.Running)
            {
                dunkin.Draw(spriteBatch, 1);
            }
            else
            {
                dunkin.Draw(spriteBatch);
            }
            #endregion

            #region Enemy Draw Code
            foreach (Enemy e in world.enemyList)
            {
                if (e.alive == true)
                {
                    if (e != corporateFatCat)
                    {
                        e.Draw(spriteBatch);
                    }
                    else
                    {
                        if (e.animationState == Enemy.Animations.Running)
                        {
                            e.Draw(spriteBatch, 1);
                        }
                        else
                        {
                            e.Draw(spriteBatch, 0);
                        }
                    }
                }
            }

            foreach (Enemy b in world.bossList)
            {
                b.Draw(spriteBatch);
            }

            #endregion

            if (attilla.visible)
            {
                if (dunkin.facing == Player.Facing.Left)
                {
                    spriteBatch.Draw(attilla.tex, attilla.pos, null, attilla.color, attilla.rotation, attilla.origin, attilla.scale, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spriteBatch.Draw(attilla.tex, attilla.pos, null, attilla.color, attilla.rotation, attilla.origin, attilla.scale, SpriteEffects.None, 0);
                }
            }

            //Draw(corporateFatCat.tex, corporateFatCat.pos, null, corporateFatCat.color, corporateFatCat.rotation, corporateFatCat.origin, corporateFatCat.scale, SpriteEffects.FlipHorizontally, 0);
            
            healthBar.DrawWithRect(spriteBatch);
            healthText.DrawString(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
