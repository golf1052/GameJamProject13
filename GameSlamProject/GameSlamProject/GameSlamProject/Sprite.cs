using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Sprite.cs: Holds basically all the variables you would need for a 2D object/image on the screen
    /// Typical usage:
    /// <code>Sprite sprite = new Sprite(Content.Load&lt;Texture2D>("sprite"));</code>
    /// </summary>
    public class Sprite
    {
        #region CONSTANTS
        public const int FLOOR = 20;
        #endregion

        /// <summary>
        /// Holds the 2D texture information.
        /// </summary>
        public Texture2D tex; 

        /// <summary>
        /// Flag for alive state.
        /// </summary>
        public bool alive;

        /// <summary>
        /// Flag for whether or not this should be drawn.
        /// </summary>
        public bool visible;

        /// <summary>
        /// Holds the X and Y coordinates of the sprite.
        /// </summary>
        public Vector2 pos;

        /// <summary>
        /// Holds the X and Y velocities of the sprite.
        /// </summary>
        public Vector2 vel;

        /// <summary>
        /// Can be used for drawing an image into a certain rectangle size.
        /// </summary>
        public Rectangle drawRect;

        /// <summary>
        /// Holds the bounding rectangle for the sprite.
        /// </summary>
        public Rectangle rect;

        /// <summary>
        /// Holds the color of the sprite.
        /// </summary>
        public Color color;

        /// <summary>
        /// Holds the alpha value of the sprite. Between 0 and 1.
        /// </summary>
        public float alpha;

        /// <summary>
        /// Holds the color data for the sprite. Used for pixel perfect collision.
        /// </summary>
        public Color[] colorData;

        /// <summary>
        /// Holds the origin X and Y coordinates of the sprite.
        /// </summary>
        public Vector2 origin;

        /// <summary>
        /// Specifies the angle (in radians) to rotate the sprite about its center.
        /// </summary>
        public float rotation;

        /// <summary>
        /// Holds the scale of the sprite/texture. 1.0 will give the same sized texture.
        /// </summary>
        public float scale;

        /// <summary>
        /// Holds the particle data for the sprite. See 
        /// <see cref="GreenGamesLibraryWin8.Particle"/>.
        /// </summary>
        public Particle[] particles;

        /// <summary>
        /// Holds sprite transform data for pixel perfect collision.
        /// </summary>
        public Matrix spriteTransform;

        /// <summary>
        /// Flag for collision. True if colliding with other objects.
        /// </summary>
        public bool collision;

        /// <summary>
        /// Lets you generate random numbers.
        /// </summary>
        public Random random;

        /// <summary>
        /// If you want the sprite to auto collide with the edge of the screen
        /// </summary>
        public bool windowCollision;

        public enum Facing
        {
            Left,
            Right,
            None
        }

        public Facing facing;

        #region Animation Stuff
        /// <summary>
        /// Flag for if this sprite has an animation
        /// </summary>
        public bool isAnimatable;

        /// <summary>
        /// The sprite strip the sprite is refering from.
        /// </summary>
        public List<SpriteSheet> spriteSheets;
        #endregion

        /// <summary>
        /// Main contructor. Sets values to all the declarations above
        /// </summary>
        /// <param name="loadedTex">The texture that is being loaded. Must load a texture when creating a new sprite.</param>
        public Sprite(Texture2D loadedTex)
        {
            tex = loadedTex;
            alive = true;
            visible = true;
            colorData = new Color[tex.Width * tex.Height];
            tex.GetData(colorData);
            pos = Vector2.Zero;
            vel = Vector2.Zero;
            drawRect = new Rectangle((int)pos.X, (int)pos.Y, 0, 0);
            color = Color.White;
            alpha = 1.0f;
            rotation = 0.0f;
            scale = 1.0f;
            collision = false;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            random = new Random();
            origin = new Vector2(tex.Width / 2, tex.Height / 2);
            windowCollision = false;
            facing = Facing.Right;
            isAnimatable = false;
        }

        public Sprite(List<SpriteSheet> loadedSheets)
        {
            spriteSheets = loadedSheets;
            isAnimatable = true;

            tex = loadedSheets[0].tex;
            colorData = new Color[tex.Width * tex.Height];
            tex.GetData(colorData);
            pos = Vector2.Zero;
            vel = Vector2.Zero;
            drawRect = new Rectangle((int)pos.X, (int)pos.Y, 0, 0);
            color = Color.White;
            alpha = 1.0f;
            rotation = 0.0f;
            scale = 1.0f;
            collision = false;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            random = new Random();
            origin = Vector2.Zero;
            windowCollision = false;
            facing = Facing.Right;
        }

        /// <summary>
        /// Secondary contructor. Used for <see cref="GreenGamesLibarary.TextItem.cs"/>.
        /// </summary>
        /// <param name="loadedFont">The SpriteFont that is being loaded. Must load a sprite font when using this constructor</param>
        public Sprite(SpriteFont loadedFont)
        {
        }

        /// <summary>
        /// Update method for animated sprites. Needs to be changed so that it can override the Sprite.cs Update function.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        //public void UpdateAnimation(GameTime gameTime)
        //{
        //    if (active == false)
        //    {
        //        return;
        //    }

        //    elapsedTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

        //    if (elapsedTime > frameTime)
        //    {
        //        currentFrame++;

        //        if (currentFrame == frameCount)
        //        {
        //            currentFrame = 0;
        //            if (looping == false)
        //            {
        //                active = false;
        //            }
        //        }

        //        elapsedTime = 0;
        //    }

        //    sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
        //}

        /// <summary>
        /// Updates the sprite. Adds vel to pos, changes the drawRect, updates rect and the pixel perfect code.
        /// </summary>
        /// <param name="gameTime">GameTime from class</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            pos += vel;
            drawRect.X = (int)pos.X;
            drawRect.Y = (int)pos.Y;
            rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
            spriteTransform = Matrix.CreateTranslation(new Vector3(-origin, 0.0f)) * Matrix.CreateScale(scale) * Matrix.CreateRotationZ(rotation) * Matrix.CreateTranslation(new Vector3(pos, 0.0f));
            rect = CalculateBoundingRectangle(new Rectangle(0, 0, tex.Width, tex.Height), spriteTransform);

            #region Window Collision
            if (windowCollision == true)
            {
                if (origin == Vector2.Zero)
                {
                    if (pos.X < 0)
                    {
                        pos.X = 0;
                    }

                    if (pos.X > graphics.GraphicsDevice.Viewport.Width - tex.Width)
                    {
                        pos.X = graphics.GraphicsDevice.Viewport.Width - tex.Width;
                    }

                    if (pos.Y < 0)
                    {
                        pos.Y = 0;
                    }

                    if (pos.Y > graphics.GraphicsDevice.Viewport.Height - tex.Height)
                    {
                        pos.Y = graphics.GraphicsDevice.Viewport.Height - tex.Height;
                    }
                }

                if (origin == new Vector2(tex.Width / 2, tex.Height / 2))
                {
                    if (pos.X < 0 + tex.Width / 2)
                    {
                        pos.X = tex.Width / 2;
                    }

                    if (pos.X > graphics.GraphicsDevice.Viewport.Width - tex.Width / 2)
                    {
                        pos.X = graphics.GraphicsDevice.Viewport.Width - tex.Width / 2;
                    }

                    if (pos.Y < 0 + tex.Height / 2)
                    {
                        pos.Y = tex.Height / 2;
                    }

                    if (pos.Y > graphics.GraphicsDevice.Viewport.Height - tex.Height / 2)
                    {
                        pos.Y = graphics.GraphicsDevice.Viewport.Height - tex.Height / 2;
                    }
                }
            }
            #endregion
        }

        public void UpdateAnimation(GameTime gameTime, GraphicsDeviceManager graphics, int index)
        {
            spriteSheets[index].UpdateAnimation(gameTime);
            spriteSheets[index].pos = pos;
            spriteSheets[index].Update(gameTime, graphics);
        }

        /// <summary>
        /// Draws the sprite. See
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Draw(Texture2D, Vector2, Nullable&lt;Rectangle>, Color, Single, Vector2, Single, SpriteEffects, Single)"/>.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from class</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, null, color, rotation, origin, scale, SpriteEffects.None, 0);
        }

        public void DrawAnimation(SpriteBatch spriteBatch, int index)
        {
            spriteSheets[index].DrawAnimation(spriteBatch);
        }

        /// <summary>
        /// Draws the sprite using the draw rect. See
        /// <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch.Draw(Texture2D, Vector2, Nullable&lt;Rectangle>, Color, Single, Vector2, Single, SpriteEffects, Single)"/>
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from class</param>
        public void DrawWithRect(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, drawRect, null, color, rotation, origin, SpriteEffects.None, 0);
        }

        /// <summary>
        /// Recolors the sprite.
        /// <remarks>Probably don't need to use this...</remarks>
        /// </summary>
        /// <param name="color">The new color that you want the sprite to be</param>
        public void RecolorSprite(Color color)
        {
            this.color = color;
        }

        /// <summary>
        /// Used in
        /// <see cref="Move(KeyboardState keyboardState, float speed, MovementDirection movementDirection, Keys key)"/>
        /// to specify the direction the sprite should move.
        /// </summary>
        public enum MovementDirection
        {
            Up,
            Down,
            Left,
            Right
        }

        /// <summary>
        /// Handles basic sprite movement.
        /// </summary>
        /// <param name="keyboardState">KeyboardState from class</param>
        /// <param name="speed">Speed you want the sprite to move</param>
        /// <param name="movementDirection">Direction you want the sprite to move</param>
        /// <param name="key">Key you want to associate with that direction</param>
        public void Move(KeyboardState keyboardState, float speed, MovementDirection movementDirection, Keys key)
        {
            if (movementDirection == MovementDirection.Up)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    pos.Y -= speed;
                }
            }

            if (movementDirection == MovementDirection.Down)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    pos.Y += speed;
                }
            }

            if (movementDirection == MovementDirection.Left)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    pos.X -= speed;
                }
            }

            if (movementDirection == MovementDirection.Right)
            {
                if (keyboardState.IsKeyDown(key))
                {
                    pos.X += speed;
                }
            }
        }

        public void MoveGamePad(GamePadState gamePadState, float speed, MovementDirection movementDirection)
        {
        }

        /// <summary>
        /// Quick way of moving a sprite. Handles keyboard and gamepad movement.
        /// </summary>
        /// <param name="keyboardState">KeyboardState from class</param>
        /// <param name="gamePadState">GamePadState from class</param>
        /// <param name="speed">Speed you want the sprite to move</param>
        public void DefaultControlSprite(KeyboardState keyboardState, GamePadState gamePadState, float speed)
        {
            if (keyboardState.IsKeyDown(Keys.W))
            {
                pos.Y -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                pos.Y += speed;
            }

            if (keyboardState.IsKeyDown(Keys.A))
            {
                pos.X -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.D))
            {
                pos.X += speed;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pos.Y -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                pos.Y += speed;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                pos.X -= speed;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                pos.X += speed;
            }

            pos.X += gamePadState.ThumbSticks.Left.X * speed;
            pos.Y -= gamePadState.ThumbSticks.Left.Y * speed;
        }

        /// <summary>
        /// Allows you point the sprite at a position. Rotates the sprite accordingly.
        /// </summary>
        /// <param name="targetPosition">The position you want the sprite to point</param>
        public void AimSprite(Vector2 targetPosition)
        {
            float XDistance = targetPosition.X - pos.X;
            float YDistance = targetPosition.Y - pos.Y;
            float angle = (float)Math.Atan2(YDistance, XDistance);
            rotation = angle;
        }

        /// <summary>
        /// Allows you to aim the sprite using the mouse.
        /// </summary>
        /// <param name="mouseState">MouseState from class</param>
        public void AimSprite(MouseState mouseState)
        {
            float XDistance = mouseState.X - pos.X;
            float YDistance = mouseState.Y - pos.Y;
            float angle = (float)Math.Atan2(YDistance, XDistance);
            rotation = angle;
        }

        /// <summary>
        /// Allows you to aim the sprite using the gamepad. Variation 1.
        /// </summary>
        /// <param name="gamePadState">GamePadState from class</param>
        public void AimSprite(GamePadState gamePadState)
        {
            float angle = (float)Math.Atan2(gamePadState.ThumbSticks.Right.Y, gamePadState.ThumbSticks.Right.X);
            rotation = angle - MathHelper.PiOver2;
        }

        public void AimSpriteLeftStick(GamePadState gamePadState)
        {
            float x = (gamePadState.ThumbSticks.Left.X + 1);
            float y = (gamePadState.ThumbSticks.Left.Y + 1);

            if (gamePadState.ThumbSticks.Left != Vector2.Zero)
            {
                rotation = (float)Math.Atan2(-gamePadState.ThumbSticks.Left.Y, gamePadState.ThumbSticks.Left.X);
            }
        }

        /// <summary>
        /// Allows you to aim the sprite using the gamepad. Variation 2.
        /// </summary>
        /// <param name="gamePadState">GamePadState from class</param>
        public void AimSpriteRightStick(GamePadState gamePadState)
        {
            float x = (gamePadState.ThumbSticks.Right.X + 1);
            float y = (gamePadState.ThumbSticks.Right.Y + 1);

            if (gamePadState.ThumbSticks.Right != Vector2.Zero)
            {
                rotation = (float)Math.Atan2(-gamePadState.ThumbSticks.Right.Y, gamePadState.ThumbSticks.Right.X);
            }
        }

        /// <summary>
        /// Returns the vector the sprite should move when moving torwards a position.
        /// </summary>
        /// <param name="targetPosition">The position you want the sprite to move</param>
        /// <returns>Returns the normalized vector the sprite should move</returns>
        public Vector2 GetVector(Vector2 targetPosition)
        {
            float XDistance;
            float YDistance;
            XDistance = targetPosition.X - pos.X;
            YDistance = targetPosition.Y - pos.Y;
            return Vector2.Normalize(new Vector2(XDistance, YDistance));
        }

        public Vector2 RotateVector(Vector2 vector, float radians)
        {
            float angle = VectorToAngle(vector) + radians;
            return AngleToVector(angle) * vector.Length();
        }

        public Vector2 RotateVector2(Vector2 vector, float radians)
        {
            float angle = VectorToAngle(vector) + radians;
            return AngleToVector(angle);
        }

        public Vector2 AngleToVector(float angle)
        {
            return Vector2.Normalize(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)));
        }

        public float VectorToAngle(Vector2 vector)
        {
            return (float)Math.Atan2(vector.Y, vector.X);
        }

        /// <summary>
        /// Used for pixel perfect collision.
        /// </summary>
        /// <param name="rectangle">The current bounding rectangle</param>
        /// <param name="transform">The current sprite transform matrix</param>
        /// <returns>Returns a new bounding rectangle</returns>
        public Rectangle CalculateBoundingRectangle(Rectangle rectangle, Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        /// <summary>
        /// Does collision between current sprite and given sprite.
        /// <remarks>Only works with circles</remarks>
        /// </summary>
        /// <param name="collided">Sprite it is colliding with</param>
        public void CircleCollision(Sprite collided)
        {
            if (alive == true && collided.alive == true)
            {
                //First find distance between two centers
                //Probably assumes the origin of the sprite is 0
                float XDistance;
                float YDistance;
                XDistance = collided.pos.X - pos.X;
                YDistance = collided.pos.Y - pos.Y;
                Vector2 intersectVector = new Vector2(XDistance, YDistance);

                if (intersectVector.Length() < ((tex.Width / 2) + (collided.tex.Width / 2)))
                {
                    float distanceToMove = Math.Abs((((tex.Width / 2) + (collided.tex.Width / 2)) + 1) - intersectVector.Length());

                    distanceToMove /= 2;
                    intersectVector.Normalize();
                    intersectVector *= distanceToMove;
                    pos -= intersectVector;
                    collided.pos += intersectVector;

                    collision = true;
                    collided.collision = true;
                }
            }
        }

        public void RectangleCollision(Sprite collided)
        {
            if (alive == true && collided.alive == true)
            {
                //rect = new Rectangle((int)pos.X, (int)pos.Y, tex.Width, tex.Height);
                //collided.rect = new Rectangle((int)collided.pos.X, (int)collided.pos.Y, collided.tex.Width, collided.tex.Height);

                if (rect.Intersects(collided.rect))
                {
                    if (rect.Bottom > collided.rect.Top)
                    {
                        //float YDistance;
                        //YDistance = pos.Y - collided.pos.Y;
                        //Vector2 intersectVector = new Vector2(0, YDistance);

                        //float distanceToMove = Math.Abs((((tex.Height / 2) + (collided.tex.Height / 2)) + 1) - intersectVector.Length());

                        //distanceToMove /= 2;
                        //intersectVector.Normalize();
                        //intersectVector *= distanceToMove;
                        //pos -= intersectVector;
                        //collided.pos += intersectVector;

                        pos.Y = collided.rect.Top - (tex.Height / 2) - 1;

                        collision = true;
                        collided.collision = true;
                    }

                    //if (rect.Top < collided.rect.Bottom)
                    //{
                    //    float YDistance;
                    //    YDistance = collided.pos.Y - pos.Y;
                    //    Vector2 intersectVector = new Vector2(0, YDistance);

                    //    float distanceToMove = Math.Abs((((tex.Height / 2) + (collided.tex.Height / 2)) + 1) - intersectVector.Length());

                    //    distanceToMove /= 2;
                    //    intersectVector.Normalize();
                    //    intersectVector *= distanceToMove;
                    //    pos -= intersectVector;
                    //    collided.pos += intersectVector;

                    //    collision = true;
                    //    collided.collision = true;
                    //}

                    //if (rect.Left < collided.rect.Right || rect.Right > collided.rect.Left)
                    //{
                    //    if (collisionEdge == CollisionEdge.None)
                    //    {
                    //        collisionEdge = CollisionEdge.XSides;

                    //        float XDistance;
                    //        XDistance = collided.pos.X - pos.X;
                    //        Vector2 intersectVector = new Vector2(XDistance, 0);

                    //        float distanceToMove = Math.Abs((((tex.Width / 2) + (collided.tex.Width / 2)) + 1) - intersectVector.Length());

                    //        distanceToMove /= 2;
                    //        intersectVector.Normalize();
                    //        intersectVector *= distanceToMove;
                    //        pos -= intersectVector;
                    //        collided.pos += intersectVector;

                    //        collision = true;
                    //        collided.collision = true;
                    //    }
                    //}
                }
            }
        }

        public bool PreventiveRectangleCollision(Sprite collided)
        {
            if (alive == true && collided.alive == true)
            {
                if (rect.Intersects(collided.rect))
                {
                    //if (rect.Bottom > collided.rect.Top)
                    //{
                    //    float YDistance;
                    //    YDistance = pos.Y - collided.pos.Y;
                    //    Vector2 intersectVector = new Vector2(0, YDistance);

                    //    float distanceToMove = Math.Abs((((tex.Height / 2) + (collided.tex.Height / 2)) + 5) - intersectVector.Length());

                    //    intersectVector.Normalize();
                    //    intersectVector *= distanceToMove;
                    //    pos -= intersectVector;

                    //    collision = true;
                    //    collided.collision = true;
                    //}

                    ////if (rect.Top < collided.rect.Bottom)
                    ////{
                    ////    float YDistance;
                    ////    YDistance = collided.pos.Y - pos.Y;
                    ////    Vector2 intersectVector = new Vector2(0, YDistance);

                    ////    float distanceToMove = Math.Abs((((tex.Height / 2) + (collided.tex.Height / 2)) + 1) - intersectVector.Length());

                    ////    distanceToMove /= 2;
                    ////    intersectVector.Normalize();
                    ////    intersectVector *= distanceToMove;
                    ////    pos -= intersectVector;
                    ////    collided.pos += intersectVector;

                    ////    collision = true;
                    ////    collided.collision = true;
                    ////}

                    ////if (rect.Left < collided.rect.Right || rect.Right > collided.rect.Left)
                    ////{
                    ////    if (collisionEdge == CollisionEdge.None)
                    ////    {
                    ////        collisionEdge = CollisionEdge.XSides;

                    ////        float XDistance;
                    ////        XDistance = collided.pos.X - pos.X;
                    ////        Vector2 intersectVector = new Vector2(XDistance, 0);

                    ////        float distanceToMove = Math.Abs((((tex.Width / 2) + (collided.tex.Width / 2)) + 1) - intersectVector.Length());

                    ////        distanceToMove /= 2;
                    ////        intersectVector.Normalize();
                    ////        intersectVector *= distanceToMove;
                    ////        pos -= intersectVector;
                    ////        collided.pos += intersectVector;

                    ////        collision = true;
                    ////        collided.collision = true;
                    ////    }
                    ////}

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
