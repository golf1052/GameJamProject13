using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using Microsoft.Xna.Framework.Media;
using System.Diagnostics;


namespace GameSlamProject
{
    /// <summary>
    /// Holds the basic information for the world that sprites and objects would exist in. Create one in the main game class to use its usefull functions.
    /// </summary>
    public class World
    {
        /// <summary>
        /// Random to use.
        /// </summary>
        public Random random = new Random();

        /// <summary>
        /// The list of objects in the world. Add objects to this list so you can do stuff to them.
        /// </summary>
        public List<Sprite> worldObjects = new List<Sprite>();

        /// <summary>
        /// The game window.
        /// </summary>
        public Rectangle gameWindow;

        /// <summary>
        /// Holds the graphics info
        /// </summary>
        public GraphicsDeviceManager graphics;

        /// <summary>
        /// Main constructor. Needs to be passed the GraphicsDeviceManager so that it can get the game window.
        /// </summary>
        /// <param name="graphics">GraphicsDeviceManager from class</param>
        public World(GraphicsDeviceManager gfx)
        {
            graphics = gfx;
            gameWindow = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);
        }

        public void GenerateBackgroundList(List<Background> backgrounds)
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                if (i == 0)
                {
                    backgrounds[i].previous = null;
                    backgrounds[i].next = backgrounds[i + 1];
                    backgrounds[i].pos = Vector2.Zero;
                }
                else if (i > 0 && i < backgrounds.Count - 1)
                {
                    backgrounds[i].previous = backgrounds[i - 1];
                    backgrounds[i].next = backgrounds[i + 1];
                    backgrounds[i].pos = new Vector2(backgrounds[i].previous.pos.X + backgrounds[i].previous.tex.Width, 0);
                }
                else
                {
                    backgrounds[i].previous = backgrounds[i - 1];
                    backgrounds[i].next = null;
                    backgrounds[i].pos = new Vector2(backgrounds[i].previous.pos.X + backgrounds[i].previous.tex.Width, 0);
                }
            }
        }

        /// <summary>
        /// Recolors all the sprites in a specified list of sprites
        /// </summary>
        /// <param name="sprites">The list of sprites to recolor</param>
        /// <param name="color">The color all sprites should be recolored</param>
        public void RecolorAllSprites(List<Sprite> sprites, Color color)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.color = color;
            }
        }

        /// <summary>
        /// Updates all the sprites in the world objects list. Also applies gravity to them if they have the gravity flag set to true.
        /// </summary>
        /// <param name="gameTime">gameTime from class</param>
        public void Update(GameTime gameTime, GraphicsDeviceManager graphics)
        {
        }

        public void MoveWorld(Vector2 movementVector)
        {
            foreach (Sprite sprite in worldObjects)
            {
                sprite.pos += movementVector;
            }
        }

        //public float ReturnCorrectRotation(float f, float s)
        //{
        //    if (f <= s)
        //    {
        //        if (f <= 180 && s >= 180 && s-f > 180)
        //        {
        //            return 360 - (s-f);
        //        }
        //        else
        //        {
        //            return -(s-f);
        //        }
        //    }
        //    else
        //    {
        //        if (s <= 180 && f >= 180 && f-s > 180)
        //        {
        //            return 360 - (f-s);
        //        }
        //        else
        //        {
        //            return (f - s);
        //        }
        //    }
        //}

        //public void Test()
        //{
        //    Debug.WriteLine(ReturnCorrectRotation(10, -10)); // 20
        //    Debug.WriteLine(ReturnCorrectRotation(10, -5)); //15
        //    Debug.WriteLine(ReturnCorrectRotation(400, -40)); //80
        //    Debug.WriteLine(ReturnCorrectRotation(-400, 40)); // 80
        //    Debug.WriteLine(ReturnCorrectRotation(0, 0)); //0
        //    Debug.WriteLine(ReturnCorrectRotation(0, 10)); //10
        //    Debug.WriteLine(ReturnCorrectRotation(-10, 0)); //10
        //}

        /// <summary>
        /// Draws all the sprites in the world objects list. Will be changed.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from class</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Sprite sprite in worldObjects)
            {
                if (sprite.alive == true)
                {
                    sprite.Draw(spriteBatch);
                }
            }
        }

        /// <summary>
        /// Distance formula.
        /// </summary>
        /// <param name="vector1">Point A</param>
        /// <param name="vector2">Point B</param>
        /// <returns>Returns the distance between Point A and Point B</returns>
        public float DistanceBetweenTwoVectors(Vector2 vector1, Vector2 vector2)
        {
            return (float)Math.Sqrt(Math.Pow((vector1.X - vector2.X), 2) + Math.Pow((vector1.Y - vector2.Y), 2));
        }

        public float GetAngle(Vector2 pointA, Vector2 pointB)
        {
            float XDistance = pointB.X - pointA.X;
            float YDistance = pointB.Y - pointA.Y;
            float angle = (float)Math.Atan2(YDistance, XDistance);
            return angle;
        }

        public bool FOVCheck(Sprite hider, Sprite seeker, int FOV)
        {
            if (MathHelper.ToDegrees((float)Math.Acos(Vector3.Dot(new Vector3(AngleToVector(seeker.rotation), 0), new Vector3(Vector2.Normalize(hider.pos - seeker.pos), 0)))) <= FOV)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        /// Quickly sets all text items in a list to a certain color.
        /// </summary>
        /// <param name="textItems">List of TextItems</param>
        /// <param name="color">The color you want to change them too</param>
        public void RecolorAllText(List<TextItem> textItems, Color color)
        {
            foreach (TextItem item in textItems)
            {
                item.color = color;
            }
        }

        /// <summary>
        /// Gets the position a sprite needs to be to be placed in the center of the game window.
        /// </summary>
        /// <param name="item">The sprite that needs to be placed</param>
        /// <param name="graphics">GraphicsDeviceManager from class</param>
        /// <returns>Returns the position a sprite needs to be to be placed in the center of the game window.</returns>
        public Vector2 GetMiddleOfScreen(Sprite item, GraphicsDeviceManager graphics)
        {
            return new Vector2((graphics.GraphicsDevice.Viewport.Width / 2) - (item.tex.Width / 2), (graphics.GraphicsDevice.Viewport.Height / 2) - (item.tex.Height / 2));
        }

        /// <summary>
        /// Checks if a ray in the ray list hit a sprite in the world objects list. Needs to be optimiezed heavily.
        /// </summary>
        /// <param name="ray">The ray that is being checked</param>
        /// <param name="rayOrigin">The sprite the ray came from</param>
        /// <returns>Returns the sprite that was hit by the ray. Returns null if no sprite was hit.</returns>
        public Sprite RayTest(Sprite ray, Sprite rayOrigin)
        {
            List<Sprite> collidedObjects = new List<Sprite>();
            Dictionary<float, Sprite> distances = new Dictionary<float, Sprite>();
            foreach (Sprite spriteObject in worldObjects)
            {
                if (spriteObject.alive == true)
                {
                    if (spriteObject.rect.Intersects(ray.rect))
                    {
                        if (PixelPerfectCheck(ray.spriteTransform, ray.tex.Width, ray.tex.Height, ray.colorData, spriteObject.spriteTransform, spriteObject.tex.Width, spriteObject.tex.Height, spriteObject.colorData) == true)
                        {
                            collidedObjects.Add(spriteObject);
                        }
                    }
                }
            }

            if (collidedObjects.Count != 0)
            {
                foreach (Sprite spriteObject in collidedObjects)
                {
                    float distanceToOrigin;
                    distanceToOrigin = (float)Math.Pow((spriteObject.pos.X - rayOrigin.pos.X), 2);
                    distanceToOrigin += (float)Math.Pow((spriteObject.pos.Y - rayOrigin.pos.Y), 2);
                    distanceToOrigin = (float)Math.Sqrt(distanceToOrigin);
                    distances.Add(distanceToOrigin, spriteObject);
                }

                float smallestDistance = 0;

                foreach (KeyValuePair<float, Sprite> distance in distances)
                {
                    if (smallestDistance == 0)
                    {
                        smallestDistance = distance.Key;
                    }
                    else
                    {
                        if (distance.Key < smallestDistance)
                        {
                            smallestDistance = distance.Key;
                        }
                    }
                }

                Sprite objectToReturn;

                if (distances.TryGetValue(smallestDistance, out objectToReturn) == true)
                {
                    collidedObjects.Clear();
                    distances.Clear();
                    return objectToReturn;
                }

                collidedObjects.Clear();
                distances.Clear();

                return null;
            }

            collidedObjects.Clear();
            distances.Clear();

            return null;
        }

        /// <summary>
        /// Pixel perfect checking code.
        /// </summary>
        /// <param name="rectangleA">Rectangle from sprite A</param>
        /// <param name="dataA">Color data from sprite A</param>
        /// <param name="rectangleB">Rectangle from sprite B</param>
        /// <param name="dataB">Color data from sprite B</param>
        /// <returns>Returns true if the two sprites are collidng, returns false if they are not.</returns>
        public bool PixelPerfectCheck(Rectangle rectangleA, Color[] dataA, Rectangle rectangleB, Color[] dataB)
        {
            // Find the bounds of the rectangle intersection
            int top = Math.Max(rectangleA.Top, rectangleB.Top);
            int bottom = Math.Min(rectangleA.Bottom, rectangleB.Bottom);
            int left = Math.Max(rectangleA.Left, rectangleB.Left);
            int right = Math.Min(rectangleA.Right, rectangleB.Right);

            // Check every point within the intersection bounds
            for (int y = top; y < bottom; y++)
            {
                for (int x = left; x < right; x++)
                {
                    // Get the color of both pixels at this point
                    Color colorA = dataA[(x - rectangleA.Left) +
                                         (y - rectangleA.Top) * rectangleA.Width];
                    Color colorB = dataB[(x - rectangleB.Left) +
                                         (y - rectangleB.Top) * rectangleB.Width];

                    // If both pixels are not completely transparent,
                    if (colorA.A != 0 && colorB.A != 0)
                    {
                        // then an intersection has been found
                        return true;
                    }
                }
            }

            // No intersection found
            return false;
        }

        /// <summary>
        /// Pixel perfect checking code. Takes rotation into consideration.
        /// </summary>
        /// <param name="transformA">Sprite transform data for sprite A</param>
        /// <param name="widthA">Width of sprite A</param>
        /// <param name="heightA">Height of sprite A</param>
        /// <param name="dataA">Color data of sprite A</param>
        /// <param name="transformB">Sprite transform data for sprite B</param>
        /// <param name="widthB">Width of sprite B</param>
        /// <param name="heightB">Height of sprite B</param>
        /// <param name="dataB">Color data of sprite B</param>
        /// <returns>Returns true if the two sprites are collidng, returns false if they are not.</returns>
        public bool PixelPerfectCheck(Matrix transformA, int widthA, int heightA, Color[] dataA, Matrix transformB, int widthB, int heightB, Color[] dataB)
        {
            // Calculate a matrix which transforms from A's local space into
            // world space and then into B's local space
            Matrix transformAToB = transformA * Matrix.Invert(transformB);

            // When a point moves in A's local space, it moves in B's local space with a
            // fixed direction and distance proportional to the movement in A.
            // This algorithm steps through A one pixel at a time along A's X and Y axes
            // Calculate the analogous steps in B:
            Vector2 stepX = Vector2.TransformNormal(Vector2.UnitX, transformAToB);
            Vector2 stepY = Vector2.TransformNormal(Vector2.UnitY, transformAToB);

            // Calculate the top left corner of A in B's local space
            // This variable will be reused to keep track of the start of each row
            Vector2 yPosInB = Vector2.Transform(Vector2.Zero, transformAToB);

            // For each row of pixels in A
            for (int yA = 0; yA < heightA; yA++)
            {
                // Start at the beginning of the row
                Vector2 posInB = yPosInB;

                // For each pixel in this row
                for (int xA = 0; xA < widthA; xA++)
                {
                    // Round to the nearest pixel
                    int xB = (int)Math.Round(posInB.X);
                    int yB = (int)Math.Round(posInB.Y);

                    // If the pixel lies within the bounds of B
                    if (0 <= xB && xB < widthB &&
                        0 <= yB && yB < heightB)
                    {
                        // Get the colors of the overlapping pixels
                        Color colorA = dataA[xA + yA * widthA];
                        Color colorB = dataB[xB + yB * widthB];

                        // If both pixels are not completely transparent,
                        if (colorA.A != 0 && colorB.A != 0)
                        {
                            // then an intersection has been found
                            return true;
                        }
                    }

                    // Move to the next pixel in the row
                    posInB += stepX;
                }

                // Move to the next row
                yPosInB += stepY;
            }

            // No intersection found
            return false;
        }
    }
}
