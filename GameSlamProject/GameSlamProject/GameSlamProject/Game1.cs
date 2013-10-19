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

        GameState gameState = GameState.Other;
        MenuState menuState = MenuState.Menu;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Previous input states
        KeyboardState previousKeyboardState = Keyboard.GetState();
        GamePadState previousGamePadState = GamePad.GetState(PlayerIndex.One);
        MouseState previousMouseState = Mouse.GetState();

        #region Other
        Texture2D particleTex;
        SpriteFont fp2StartFont;
        TextItem fp2F;
        TextItem fp2P;
        TextItem fp22;
        TimeSpan countDownToLaunch = TimeSpan.FromSeconds(3);

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

        SpriteFont fp2ColorFont;
        TextItem t_color;
        bool readyToSelect = true;
        Flixel selecting;
        TimeSpan displayTime = TimeSpan.FromSeconds(0.5);
        bool fade = false;

        SpriteFont fp2Exploded;
        TextItem t_f1Exploded;
        TextItem t_f2Exploded;
        TextItem t_f3Exploded;
        TextItem t_f4Exploded;
        List<TextItem> explodedTexts = new List<TextItem>();

        TextItem t_playerWins;
        TextItem t_playerWinsScore;

        bool tie = false;
        bool winner = false;
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
            graphics.IsFullScreen = true;
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

            #region Other
            particleTex = Content.Load<Texture2D>("flixel");
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

            fp2ColorFont = Content.Load<SpriteFont>("fp2colorfont");
            t_color = new TextItem(fp2ColorFont, "fire");
            t_color.alive = false;
            t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
            selecting = null;

            fp2Exploded = Content.Load<SpriteFont>("fp2exploded");
            t_f1Exploded = new TextItem(fp2Exploded, "player 1 exploded");
            t_f1Exploded.alive = false;
            t_f1Exploded.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_f1Exploded.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 5 - t_f1Exploded.MeasureString().Y / 2);
            t_f2Exploded = new TextItem(fp2Exploded, "player 2 exploded");
            t_f2Exploded.alive = false;
            t_f2Exploded.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_f2Exploded.MeasureString().X / 2, (graphics.GraphicsDevice.Viewport.Height / 5) * 2 - t_f2Exploded.MeasureString().Y / 2);
            t_f3Exploded = new TextItem(fp2Exploded, "player 3 exploded");
            t_f3Exploded.alive = false;
            t_f3Exploded.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_f3Exploded.MeasureString().X / 2, (graphics.GraphicsDevice.Viewport.Height / 5) * 3 - t_f3Exploded.MeasureString().Y / 2);
            t_f4Exploded = new TextItem(fp2Exploded, "player 4 exploded");
            t_f4Exploded.alive = false;
            t_f4Exploded.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_f4Exploded.MeasureString().X / 2, (graphics.GraphicsDevice.Viewport.Height / 5) * 4 - t_f4Exploded.MeasureString().Y / 2);

            explodedTexts.Add(t_f1Exploded);
            explodedTexts.Add(t_f2Exploded);
            explodedTexts.Add(t_f3Exploded);
            explodedTexts.Add(t_f4Exploded);

            t_playerWins = new TextItem(fp2Exploded, "player 1 wins");
            t_playerWins.alive = false;
            t_playerWins.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_playerWins.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 3 - t_playerWins.MeasureString().Y / 2);
            t_playerWinsScore = new TextItem(fp2Exploded, "score: 0");
            t_playerWinsScore.alive = false;
            t_playerWinsScore.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_playerWinsScore.MeasureString().X / 2, (graphics.GraphicsDevice.Viewport.Height / 3) * 2 - t_playerWinsScore.MeasureString().Y / 2);
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
                    countDownToLaunch = TimeSpan.FromSeconds(3);
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
                    #region Select Color
                    if (keyboardState.IsKeyDown(Keys.Tab) && previousKeyboardState.IsKeyUp(Keys.Tab))
                    {
                        if (readyToSelect == true)
                        {
                            readyToSelect = false;
                            t_color.alive = true;
                            fade = false;
                            selecting = flixel1;
                            t_color.alpha = 1.0f;
                            displayTime = TimeSpan.FromSeconds(0.5);
                            t_color.color = selecting.colorLists[selecting.selectedColor][0];

                            t_color.text = selecting.nameOfColors[selecting.selectedColor];
                            t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                        }
                        else
                        {
                            if (selecting == flixel1)
                            {
                                displayTime = TimeSpan.FromSeconds(0.5);
                                t_color.alpha = 1.0f;
                                fade = false;
                                if (selecting.selectedColor < selecting.colorLists.Count - 1)
                                {
                                    selecting.selectedColor++;
                                }
                                else
                                {
                                    selecting.selectedColor = 0;
                                }
                                t_color.color = selecting.colorLists[selecting.selectedColor][0];
                                t_color.text = selecting.nameOfColors[selecting.selectedColor];
                                t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                            }
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.R) && previousKeyboardState.IsKeyUp(Keys.R))
                    {
                        if (readyToSelect == true)
                        {
                            readyToSelect = false;
                            t_color.alive = true;
                            fade = false;
                            selecting = flixel2;
                            t_color.alpha = 1.0f;
                            displayTime = TimeSpan.FromSeconds(0.5);
                            t_color.color = selecting.colorLists[selecting.selectedColor][0];

                            t_color.text = selecting.nameOfColors[selecting.selectedColor];
                            t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                        }
                        else
                        {
                            if (selecting == flixel2)
                            {
                                displayTime = TimeSpan.FromSeconds(0.5);
                                t_color.alpha = 1.0f;
                                fade = false;
                                if (selecting.selectedColor < selecting.colorLists.Count - 1)
                                {
                                    selecting.selectedColor++;
                                }
                                else
                                {
                                    selecting.selectedColor = 0;
                                }
                                t_color.color = selecting.colorLists[selecting.selectedColor][0];
                                t_color.text = selecting.nameOfColors[selecting.selectedColor];
                                t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                            }
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
                    {
                        if (readyToSelect == true)
                        {
                            readyToSelect = false;
                            t_color.alive = true;
                            fade = false;
                            selecting = flixel3;
                            t_color.alpha = 1.0f;
                            displayTime = TimeSpan.FromSeconds(0.5);
                            t_color.color = selecting.colorLists[selecting.selectedColor][0];

                            t_color.text = selecting.nameOfColors[selecting.selectedColor];
                            t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                        }
                        else
                        {
                            if (selecting == flixel3)
                            {
                                displayTime = TimeSpan.FromSeconds(0.5);
                                t_color.alpha = 1.0f;
                                fade = false;
                                if (selecting.selectedColor < selecting.colorLists.Count - 1)
                                {
                                    selecting.selectedColor++;
                                }
                                else
                                {
                                    selecting.selectedColor = 0;
                                }
                                t_color.color = selecting.colorLists[selecting.selectedColor][0];
                                t_color.text = selecting.nameOfColors[selecting.selectedColor];
                                t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                            }
                        }
                    }

                    if (keyboardState.IsKeyDown(Keys.P) && previousKeyboardState.IsKeyUp(Keys.P))
                    {
                        if (readyToSelect == true)
                        {
                            readyToSelect = false;
                            t_color.alive = true;
                            fade = false;
                            selecting = flixel4;
                            t_color.alpha = 1.0f;
                            displayTime = TimeSpan.FromSeconds(0.5);
                            t_color.color = selecting.colorLists[selecting.selectedColor][0];

                            t_color.text = selecting.nameOfColors[selecting.selectedColor];
                            t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                        }
                        else
                        {
                            if (selecting == flixel4)
                            {
                                displayTime = TimeSpan.FromSeconds(0.5);
                                t_color.alpha = 1.0f;
                                fade = false;
                                if (selecting.selectedColor < selecting.colorLists.Count - 1)
                                {
                                    selecting.selectedColor++;
                                }
                                else
                                {
                                    selecting.selectedColor = 0;
                                }
                                t_color.color = selecting.colorLists[selecting.selectedColor][0];
                                t_color.text = selecting.nameOfColors[selecting.selectedColor];
                                t_color.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_color.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_color.MeasureString().Y / 2);
                            }
                        }
                    }

                    if (fade == true)
                    {
                        t_color.alpha -= 0.1f;
                        t_color.color *= t_color.alpha;
                    }

                    if (displayTime > TimeSpan.Zero)
                    {
                        displayTime -= gameTime.ElapsedGameTime;
                    }

                    if (selecting != null)
                    {
                        t_color.color = selecting.colorLists[selecting.selectedColor][world.random.Next(0, selecting.colorLists[selecting.selectedColor].Count)];
                        t_color.color *= t_color.alpha;
                    }

                    if (fade == true)
                    {
                        if (t_color.alpha <= 0.0f)
                        {
                            t_color.alive = false;
                            readyToSelect = true;
                            fade = false;
                        }
                    }

                    if (displayTime <= TimeSpan.Zero)
                    {
                        fade = true;
                    }
                    #endregion

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
                        SetUpGame();

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
                        if (started == true)
                        {
                            flixel1.Push(keyboardState, previousKeyboardState, Keys.Tab, gameTime, graphics, 20.0f);
                        }

                        flixel1.Update(gameTime, graphics);

                        if (flixel1.alive == true)
                        {
                            position.Add(flixel1.score);
                        }
                    }

                    if (flixel2.playing == true)
                    {
                        if (started == true)
                        {
                            flixel2.Push(keyboardState, previousKeyboardState, Keys.R, gameTime, graphics, 20.0f);
                        }

                        flixel2.Update(gameTime, graphics);

                        if (flixel2.alive == true)
                        {
                            position.Add(flixel2.score);
                        }
                    }

                    if (flixel3.playing == true)
                    {
                        if (started == true)
                        {
                            flixel3.Push(keyboardState, previousKeyboardState, Keys.U, gameTime, graphics, 20.0f);
                        }

                        flixel3.Update(gameTime, graphics);

                        if (flixel3.alive == true)
                        {
                            position.Add(flixel3.score);
                        }
                    }

                    if (flixel4.playing == true)
                    {
                        if (started == true)
                        {
                            flixel4.Push(keyboardState, previousKeyboardState, Keys.P, gameTime, graphics, 20.0f);
                        }

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
                            t_countDown.pos = Shake(t_countDown.pos, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_countDown.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 2 - t_countDown.MeasureString().Y / 2));
                        }
                        else
                        {
                            t_countDown.color = Color.White;
                        }

                        if (countDown <= TimeSpan.Zero)
                        {
                            if (position.Count == 0)
                            {
                                menuState = MenuState.Menu;
                            }

                            countDown = TimeSpan.FromSeconds(10);

                            if (position.Count > 0)
                            {
                                List<Flixel> playersKilled = new List<Flixel>();

                                if (position.Count == 1)
                                {
                                    winner = true;

                                    if (flixel1.score == position[0])
                                    {
                                        t_playerWins.text = "player 1 wins";
                                        t_playerWinsScore.text = "score: " + flixel1.score.ToString();
                                    }
                                    else if (flixel2.score == position[0])
                                    {
                                        t_playerWins.text = "player 2 wins";
                                        t_playerWinsScore.text = "score: " + flixel2.score.ToString();
                                    }
                                    else if (flixel3.score == position[0])
                                    {
                                        t_playerWins.text = "player 3 wins";
                                        t_playerWinsScore.text = "score: " + flixel3.score.ToString();
                                    }
                                    else if (flixel4.score == position[0])
                                    {
                                        t_playerWins.text = "player 4 wins";
                                        t_playerWinsScore.text = "score: " + flixel4.score.ToString();
                                    }

                                    t_playerWins.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_playerWins.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 3 - t_playerWins.MeasureString().Y / 2);
                                    t_playerWinsScore.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_playerWinsScore.MeasureString().X / 2, (graphics.GraphicsDevice.Viewport.Height / 3) * 2 - t_playerWinsScore.MeasureString().Y / 2);
                                }

                                if (flixel1.alive == true)
                                {
                                    if (flixel1.score == position[position.Count - 1])
                                    {
                                        flixel1.alive = false;
                                        flixel1.exploding = true;
                                        t_f1Exploded.alive = true;
                                        t_f1Exploded.alpha = 1.0f;
                                        playersKilled.Add(flixel1);
                                    }
                                }

                                if (flixel2.alive == true)
                                {
                                    if (flixel2.score == position[position.Count - 1])
                                    {
                                        flixel2.alive = false;
                                        flixel2.exploding = true;
                                        t_f2Exploded.alive = true;
                                        t_f2Exploded.alpha = 1.0f;
                                        playersKilled.Add(flixel2);
                                    }
                                }

                                if (flixel3.alive == true)
                                {
                                    if (flixel3.score == position[position.Count - 1])
                                    {
                                        flixel3.alive = false;
                                        flixel3.exploding = true;
                                        t_f3Exploded.alive = true;
                                        t_f3Exploded.alpha = 1.0f;
                                        playersKilled.Add(flixel3);
                                    }
                                }

                                if (flixel4.alive == true)
                                {
                                    if (flixel4.score == position[position.Count - 1])
                                    {
                                        flixel4.alive = false;
                                        flixel4.exploding = true;
                                        t_f4Exploded.alive = true;
                                        t_f4Exploded.alpha = 1.0f;
                                        playersKilled.Add(flixel4);
                                    }
                                }

                                if (winner == false)
                                {
                                    if (playersKilled.Count == world.playersAlive)
                                    {
                                        tie = true;
                                    }
                                }

                                foreach (Flixel flixel in playersKilled)
                                {
                                    world.playersAlive--;
                                }

                                if (world.playersAlive == 0)
                                {
                                    if (tie == true)
                                    {
                                        t_playerWins.alive = true;
                                        t_playerWins.text = "nobody wins";
                                        t_playerWins.color = Color.White;
                                        t_playerWins.pos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2 - t_playerWins.MeasureString().X / 2, graphics.GraphicsDevice.Viewport.Height / 4 - t_playerWins.MeasureString().Y / 2);
                                    }

                                    if (winner == true)
                                    {
                                        t_playerWins.alive = true;
                                        t_playerWinsScore.alive = true;
                                    }

                                    countDown = TimeSpan.FromSeconds(6);
                                }
                            }
                        }

                        foreach (TextItem item in explodedTexts)
                        {
                            if (item.alive == true)
                            {
                                item.alpha -= 0.001f;
                                item.color *= item.alpha;

                                if (item.alpha <= 0.0f)
                                {
                                    item.alive = false;
                                    item.alpha = 1.0f;
                                    item.color.A = (byte)1.0f;
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

        Vector2 Shake(Vector2 pos, Vector2 originalPosition)
        {
            if (pos == originalPosition)
            {
                return new Vector2(pos.X + world.random.Next(-15, 16), pos.Y + world.random.Next(-15, 16));
            }
            else
            {
                return originalPosition;
            }
        }

        void SetUpGame()
        {
            startDown = TimeSpan.FromSeconds(4);
            countDown = TimeSpan.FromSeconds(10);
            t_countDown.color = Color.White;
            started = false;
            tie = false;
            winner = false;
            t_countDown.alive = false;
            t_color.alive = false;
            t_f1Exploded.alive = false;
            t_f2Exploded.alive = false;
            t_f3Exploded.alive = false;
            t_f4Exploded.alive = false;
            t_playerWins.alive = false;
            t_playerWinsScore.alive = false;

            flixel1.positon = 0;
            flixel1.score = 0;
            flixel1.alive = true;
            flixel1.exploding = false;
            flixel1.side = Flixel.Side.Center;
            flixel1.sky.fadeRate = 0.0f;
            flixel1.sky.currentColor = 0;
            flixel1.sky.color = flixel1.sky.fadeToList[0];
            flixel1.playing = false;
            t_f1Exploded.alpha = 1.0f;
            flixel1.t_score.text = "0";
            t_f1Exploded.color = Color.White;

            flixel2.positon = 0;
            flixel2.score = 0;
            flixel2.alive = true;
            flixel2.exploding = false;
            flixel2.side = Flixel.Side.Center;
            flixel2.sky.fadeRate = 0.0f;
            flixel2.sky.currentColor = 0;
            flixel2.sky.color = flixel2.sky.fadeToList[0];
            flixel2.playing = false;
            t_f2Exploded.alpha = 1.0f;
            flixel2.t_score.text = "0";
            t_f2Exploded.color = Color.White;

            flixel3.positon = 0;
            flixel3.score = 0;
            flixel3.alive = true;
            flixel3.exploding = false;
            flixel3.side = Flixel.Side.Center;
            flixel3.sky.fadeRate = 0.0f;
            flixel3.sky.currentColor = 0;
            flixel3.sky.color = flixel3.sky.fadeToList[0];
            flixel3.playing = false;
            t_f3Exploded.alpha = 1.0f;
            flixel3.t_score.text = "0";
            t_f3Exploded.color = Color.White;

            flixel4.positon = 0;
            flixel4.score = 0;
            flixel4.alive = true;
            flixel4.exploding = false;
            flixel4.side = Flixel.Side.Center;
            flixel4.sky.fadeRate = 0.0f;
            flixel4.sky.currentColor = 0;
            flixel4.sky.color = flixel4.sky.fadeToList[0];
            flixel4.playing = false;
            t_f4Exploded.alpha = 1.0f;
            flixel4.t_score.text = "0";
            t_f4Exploded.color = Color.White;
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

                    if (t_color.alive == true)
                    {
                        t_color.DrawString(spriteBatch);
                    }
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

                    if (t_f1Exploded.alive == true)
                    {
                        t_f1Exploded.DrawString(spriteBatch);
                    }

                    if (t_f2Exploded.alive == true)
                    {
                        t_f2Exploded.DrawString(spriteBatch);
                    }

                    if (t_f3Exploded.alive == true)
                    {
                        t_f3Exploded.DrawString(spriteBatch);
                    }

                    if (t_f4Exploded.alive == true)
                    {
                        t_f4Exploded.DrawString(spriteBatch);
                    }

                    if (t_playerWins.alive == true)
                    {
                        t_playerWins.DrawString(spriteBatch);
                    }

                    if (t_playerWinsScore.alive == true)
                    {
                        t_playerWinsScore.DrawString(spriteBatch);
                    }
                }
            }
            #endregion

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
