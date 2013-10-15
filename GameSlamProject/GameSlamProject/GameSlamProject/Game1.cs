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
        // Number of lines Nat has written: 2

        enum GameState
        {
            Duncan,
            Other
        }

        enum MenuState
        {
            Menu,
            Game
        }

        GameState gameState = GameState.Duncan;
        MenuState menuState = MenuState.Menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Previous input states
        KeyboardState previousKeyboardState = Keyboard.GetState();
        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
        MouseState previousMouseState = Mouse.GetState();

        #region Mr President
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

        List<Background> backgrounds = new List<Background>();
#endregion

        #region Other
        SpriteFont fp2StartFont;
        TextItem fp2F;
        TextItem fp2P;
        TextItem fp22;
        TimeSpan countDownToLaunch = TimeSpan.FromSeconds(5);

        SpriteFont fp2MenuFont;
        TextItem t_flixel;
        TextItem t_push;
        TextItem t_2;

        Earth earth;

        SpriteFont fp2PlayerFont;
        TextItem t_1player;
        TextItem t_2player;
        TextItem t_3player;
        TextItem t_4player;

        int menuSelection = 0;

        Flixel flixel1;
        Flixel flixel2;
        Flixel flixel3;
        Flixel flixel4;

        TimeSpan startDown = TimeSpan.FromSeconds(4);
        TimeSpan countDown = TimeSpan.FromSeconds(10);
        SpriteFont fp2CountDownFont;
        TextItem t_countDown;
        bool started = false;
        #endregion

        /// <summary>
        /// THE WORLD...BISH
        /// </summary>
        World world;

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

            #region Mr President
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

            repEnemySpriteSheets.Add(new SpriteSheet(Content.Load<Texture2D>("flixel"), 450, 450, 1, 0, true));
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
            corporateFatCat.pos = new Vector2(0, world.GROUND_HEIGHT - 450);
            corporateFatCat.pos.X = 3000;
            corporateFatCat.vel = new Vector2(-2.0f, 0.0f);
            corporateFatCat.facing = Sprite.Facing.Right;
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
            #endregion

            #region Other
            fp2StartFont = Content.Load<SpriteFont>("fp2startfont");
            fp2F = new TextItem(fp2StartFont, "f");
            fp2P = new TextItem(fp2StartFont, "p");
            fp22 = new TextItem(fp2StartFont, "2");
            fp2F.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 6 - fp2F.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - fp2F.MeasureString().Y / 2);
            fp2P.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - fp2P.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - fp2P.MeasureString().Y / 2);
            fp22.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width - graphics.GraphicsDevice.Viewport.Width / 6 - fp22.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - fp22.MeasureString().Y / 2);

            fp2MenuFont = Content.Load<SpriteFont>("fp2menufont");
            t_flixel = new TextItem(fp2MenuFont, "flixel");
            t_push = new TextItem(fp2MenuFont, "push");
            t_2 = new TextItem(fp2MenuFont, "2");
            t_flixel.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_flixel.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 6);
            t_push.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_push.MeasureString().X / 2, t_flixel.pos.Y + t_flixel.MeasureString().Y);
            t_2.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_2.MeasureString().X / 2, t_push.pos.Y + t_push.MeasureString().Y);

            earth = new Earth(Content.Load<Texture2D>("earth"));
            earth.origin = Vector2.Zero;
            earth.pos = new Vector2(0, graphics.GraphicsDevice.Viewport.Height);

            fp2PlayerFont = Content.Load<SpriteFont>("fp2playerfont");
            t_1player = new TextItem(fp2PlayerFont, "1 player");
            t_2player = new TextItem(fp2PlayerFont, "2 player");
            t_3player = new TextItem(fp2PlayerFont, "3 player");
            t_4player = new TextItem(fp2PlayerFont, "4 player");
            t_1player.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 5 - t_1player.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height - t_1player.MeasureString().Y - 20);
            t_2player.pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 5) * 2 - t_2player.MeasureString().X / 2, t_1player.pos.Y);
            t_3player.pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 5) * 3 - t_2player.MeasureString().X / 2, t_1player.pos.Y);
            t_4player.pos = new Vector2((graphics.GraphicsDevice.Viewport.Width / 5) * 4 - t_2player.MeasureString().X / 2, t_1player.pos.Y);

            flixel1 = new Flixel(particleTex);
            flixel1.t_position = new TextItem(fp2PlayerFont, "");
            flixel1.t_score = new TextItem(fp2PlayerFont, "");
            flixel1.particles = new Particle[1000];
            for (int i = 0; i < flixel1.particles.Length; i++)
            {
                flixel1.particles[i] = new Particle(particleTex, true);
            }
            for (int i = 0; i < flixel1.stars.Length; i++)
            {
                flixel1.stars[i] = new Star(particleTex);
            }

            flixel2 = new Flixel(particleTex);
            flixel2.t_position = new TextItem(fp2PlayerFont, "");
            flixel2.t_score = new TextItem(fp2PlayerFont, "");
            flixel2.particles = new Particle[1000];
            for (int i = 0; i < flixel2.particles.Length; i++)
            {
                flixel2.particles[i] = new Particle(particleTex, true);
            }
            for (int i = 0; i < flixel2.stars.Length; i++)
            {
                flixel2.stars[i] = new Star(particleTex);
            }

            flixel3 = new Flixel(particleTex);
            flixel3.t_position = new TextItem(fp2PlayerFont, "");
            flixel3.t_score = new TextItem(fp2PlayerFont, "");
            flixel3.particles = new Particle[1000];
            for (int i = 0; i < flixel3.particles.Length; i++)
            {
                flixel3.particles[i] = new Particle(particleTex, true);
            }
            for (int i = 0; i < flixel3.stars.Length; i++)
            {
                flixel3.stars[i] = new Star(particleTex);
            }

            flixel4 = new Flixel(particleTex);
            flixel4.t_position = new TextItem(fp2PlayerFont, "");
            flixel4.t_score = new TextItem(fp2PlayerFont, "");
            flixel4.particles = new Particle[1000];
            for (int i = 0; i < flixel4.particles.Length; i++)
            {
                flixel4.particles[i] = new Particle(particleTex, true);
            }
            for (int i = 0; i < flixel4.stars.Length; i++)
            {
                flixel4.stars[i] = new Star(particleTex);
            }

            fp2CountDownFont = Content.Load<SpriteFont>("fp2countdown");
            t_countDown = new TextItem(fp2CountDownFont, "3");
            t_countDown.alive = false;
            t_countDown.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_countDown.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_countDown.MeasureString().Y / 2);
            #endregion
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

            #region Mr President Code
            if (gameState == GameState.Duncan)
            {
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
                    if (world.gameWindow.Contains((int)e.pos.X, (int)e.pos.Y))
                    {
                        e.outOfBounds = false;
                    }

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
            }
            #endregion

            #region Other
            #region Launch Game
            if (gameState == GameState.Duncan)
            {
                if (keyboardState.IsKeyDown(Keys.F))
                {
                    fp2F.alive = true;
                    fp2F.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                    if (keyboardState.IsKeyDown(Keys.P))
                    {
                        fp2P.alive = true;
                        fp2P.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                        if (keyboardState.IsKeyDown(Keys.D2))
                        {
                            fp22.alive = true;
                            fp22.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                            if (keyboardState.IsKeyDown(Keys.LeftShift))
                            {
                                if (countDownToLaunch >= TimeSpan.Zero)
                                {
                                    countDownToLaunch -= gameTime.ElapsedGameTime;
                                }
                            }
                        }
                    }
                }
                else
                {
                    fp2F.alive = false;
                    fp2P.alive = false;
                    fp22.alive = false;
                    countDownToLaunch = TimeSpan.FromSeconds(5);
                }
            }

            if (countDownToLaunch <= TimeSpan.Zero)
            {
                gameState = GameState.Other;
                fp2F.alive = false;
                fp2P.alive = false;
                fp22.alive = false;
            }
            #endregion

            if (gameState == GameState.Other)
            {
                if (menuState == MenuState.Menu)
                {
                    if (earth.moving == true)
                    {
                        if (earth.pos.Y <= 494)
                        {
                            earth.moving = false;
                            earth.pos.Y = 494;
                        }
                        else
                        {
                            earth.pos.Y -= 1.0f;
                        }
                    }

                    #region Menu Selection Code
                    #region Select Player
                    if (keyboardState.IsKeyDown(Keys.Left) && previousKeyboardState.IsKeyUp(Keys.Left))
                    {
                        if (menuSelection == 0)
                        {
                            menuSelection = 3;
                        }
                        else
                        {
                            menuSelection--;
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.Right) && previousKeyboardState.IsKeyUp(Keys.Right))
                    {
                        if (menuSelection == 3)
                        {
                            menuSelection = 0;
                        }
                        else
                        {
                            menuSelection++;
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.Enter) && previousKeyboardState.IsKeyUp(Keys.Enter))
                    {
                        menuState = MenuState.Game;
                        world.numOfPlayers = menuSelection + 1;
                        world.playersAlive = world.numOfPlayers;

                        if (menuSelection == 0)
                        {
                            flixel1.playing = true;
                            flixel1.SetUp(world.numOfPlayers, graphics, 1);
                        }
                        else if (menuSelection == 1)
                        {
                            flixel1.playing = true;
                            flixel1.SetUp(world.numOfPlayers, graphics, 1);

                            flixel2.playing = true;
                            flixel2.SetUp(world.numOfPlayers, graphics, 2);
                        }
                        else if (menuSelection == 2)
                        {
                            flixel1.playing = true;
                            flixel1.SetUp(world.numOfPlayers, graphics, 1);

                            flixel2.playing = true;
                            flixel2.SetUp(world.numOfPlayers, graphics, 2);

                            flixel3.playing = true;
                            flixel3.SetUp(world.numOfPlayers, graphics, 3);
                        }
                        else if (menuSelection == 3)
                        {
                            flixel1.playing = true;
                            flixel1.SetUp(world.numOfPlayers, graphics, 1);

                            flixel2.playing = true;
                            flixel2.SetUp(world.numOfPlayers, graphics, 2);

                            flixel3.playing = true;
                            flixel3.SetUp(world.numOfPlayers, graphics, 3);

                            flixel4.playing = true;
                            flixel4.SetUp(world.numOfPlayers, graphics, 4);
                        }
                    }
                    #endregion

                    #region Color TextItems
                    if (menuSelection == 0)
                    {
                        t_1player.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                    }
                    else
                    {
                        t_1player.color = Color.White;
                    }

                    if (menuSelection == 1)
                    {
                        t_2player.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                    }
                    else
                    {
                        t_2player.color = Color.White;
                    }

                    if (menuSelection == 2)
                    {
                        t_3player.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                    }
                    else
                    {
                        t_3player.color = Color.White;
                    }

                    if (menuSelection == 3)
                    {
                        t_4player.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                    }
                    else
                    {
                        t_4player.color = Color.White;
                    }
                    #endregion
                    #endregion
                }

                if (menuState == MenuState.Game)
                {
                    List<int> position = new List<int>();

                    t_countDown.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_countDown.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_countDown.MeasureString().Y / 2);

                    if (flixel1.playing == true)
                    {
                        
                        flixel1.Push(keyboardState, previousKeyboardState, Keys.Tab, gameTime, graphics, 20.0f);
                        flixel1.Update(gameTime, graphics);

                        if (flixel1.alive == true)
                        {
                            position.Add(flixel1.score);
                        }
                    }

                    if (flixel2.playing == true)
                    {
                        
                        flixel2.Push(keyboardState, previousKeyboardState, Keys.R, gameTime, graphics, 20.0f);
                        flixel2.Update(gameTime, graphics);

                        if (flixel2.alive == true)
                        {
                            position.Add(flixel2.score);
                        }
                    }

                    if (flixel3.playing == true)
                    {
                        
                        flixel3.Push(keyboardState, previousKeyboardState, Keys.U, gameTime, graphics, 20.0f);
                        flixel3.Update(gameTime, graphics);

                        if (flixel3.alive == true)
                        {
                            position.Add(flixel3.score);
                        }
                    }

                    if (flixel4.playing == true)
                    {
                        
                        flixel4.Push(keyboardState, previousKeyboardState, Keys.P, gameTime, graphics, 20.0f);
                        flixel4.Update(gameTime, graphics);

                        if (flixel4.alive == true)
                        {
                            position.Add(flixel4.score);
                        }
                    }

                    position.Sort();
                    position.Reverse();

                    for (int i = 0; i < world.playersAlive; i++)
                    {
                        if (position[i] == flixel1.score)
                        {
                            flixel1.positon = i + 1;
                        }

                        if (position[i] == flixel2.score)
                        {
                            flixel2.positon = i + 1;
                        }

                        if (position[i] == flixel3.score)
                        {
                            flixel3.positon = i + 1;
                        }

                        if (position[i] == flixel4.score)
                        {
                            flixel4.positon = i + 1;
                        }
                    }

                    if (started == false)
                    {
                        startDown -= gameTime.ElapsedGameTime;
                        t_countDown.text = startDown.Seconds.ToString();

                        if (startDown <= TimeSpan.Zero)
                        {
                            started = true;
                        }
                    }

                    if (started == true)
                    {
                        countDown -= gameTime.ElapsedGameTime;
                        t_countDown.text = countDown.Seconds.ToString();

                        if (countDown.Seconds <= 3)
                        {
                            t_countDown.color = new Color((float)world.random.NextDouble(), (float)world.random.NextDouble(), (float)world.random.NextDouble());
                        }
                        else
                        {
                            t_countDown.color = Color.White;
                        }

                        if (countDown <= TimeSpan.Zero)
                        {
                            countDown = TimeSpan.FromSeconds(10);

                            if (position.Count > 0)
                            {
                                while (true)
                                {
                                    if (flixel1.alive == true)
                                    {
                                        if (flixel1.positon == world.playersAlive)
                                        {
                                            flixel1.alive = false;
                                            flixel1.exploding = true;
                                            world.playersAlive--;
                                            break;
                                        }
                                    }

                                    if (flixel2.alive == true)
                                    {
                                        if (flixel2.positon == world.playersAlive)
                                        {
                                            flixel2.alive = false;
                                            flixel2.exploding = true;
                                            world.playersAlive--;
                                            break;
                                        }
                                    }

                                    if (flixel3.alive == true)
                                    {
                                        if (flixel3.positon == world.playersAlive)
                                        {
                                            flixel3.alive = false;
                                            flixel3.exploding = true;
                                            world.playersAlive--;
                                            break;
                                        }
                                    }

                                    if (flixel4.alive == true)
                                    {
                                        if (flixel4.positon == world.playersAlive)
                                        {
                                            flixel4.alive = false;
                                            flixel4.exploding = true;
                                            world.playersAlive--;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
            if (gameState == GameState.Duncan)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
            }
            else
            {
                GraphicsDevice.Clear(new Color(0, 0, 25));
            }

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

            #region Mr President
            if (gameState == GameState.Duncan)
            {
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
            }
            #endregion

            #region Other
            if (fp2F.alive == true)
            {
                fp2F.DrawString(spriteBatch);
            }

            if (fp2P.alive == true)
            {
                fp2P.DrawString(spriteBatch);
            }

            if (fp22.alive == true)
            {
                fp22.DrawString(spriteBatch);
            }

            if (gameState == GameState.Other)
            {
                if (menuState == MenuState.Menu)
                {
                    earth.Draw(spriteBatch);
                    t_flixel.DrawString(spriteBatch);
                    t_push.DrawString(spriteBatch);
                    t_2.DrawString(spriteBatch);
                    t_1player.DrawString(spriteBatch);
                    t_2player.DrawString(spriteBatch);
                    t_3player.DrawString(spriteBatch);
                    t_4player.DrawString(spriteBatch);
                }
                else if (menuState == MenuState.Game)
                {
                    if (flixel1.playing == true)
                    {
                        flixel1.Draw(spriteBatch);
                    }

                    if (flixel2.playing == true)
                    {
                        flixel2.Draw(spriteBatch);
                    }

                    if (flixel3.playing == true)
                    {
                        flixel3.Draw(spriteBatch);
                    }

                    if (flixel4.playing == true)
                    {
                        flixel4.Draw(spriteBatch);
                    }

                    t_countDown.DrawString(spriteBatch);
                }
            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
