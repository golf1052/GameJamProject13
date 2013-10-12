using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameSlamProject
{
    public class Player:Sprite
    {
        #region Constants
        const int MAX_JUMP_HEIGHT = 600;
        const int FLOOR_HEIGHT = 768;
        const int MOVE_DISTANCE = 5;
        const int JUMP_HEIGHT = 15;
        const int MELEE_RANGE = 90;
        Vector2 PLAYER_VELOCITY = new Vector2(5, 0);
        #endregion

        /// <summary>
        /// Player's health
        /// </summary>
        public int health;

        /// <summary>
        /// is the player invulnerable to enemies?
        /// </summary>
        public bool isInvulnerable;
        
        /// <summary>
        /// rate of fire coefficient for attacks 
        /// </summary>
        public int rateOfFire;

        /// <summary>
        /// duration in seconds of pup
        /// </summary>
        public int pupDuration;

        /// <summary>
        /// kills that the player has in this level
        /// </summary>
        public int killCount;

        /// <summary>
        /// does the player have a pup
        /// </summary>
        public bool hasPup;

        /// <summary>
        /// player's score
        /// </summary>
        public int score;
        
        /// <summary>
        /// can the player fire X attack?
        /// </summary>
        public bool canFire;

        /// <summary>
        /// can the player fire C attack?
        /// </summary>
        public bool canUseStrike;

        /// <summary>
        /// can the player fire V attack?
        /// </summary>
        public bool canUseFear;

        /// <summary>
        /// is the player in a jump
        /// </summary>
        public bool isJumping;

        /// <summary>
        /// is the player falling
        /// </summary>
        public bool isFalling;

        /// <summary>
        /// is the player colliding with an object
        /// </summary>
        public bool isColliding;

        /// <summary>
        /// the bullet that exists as part of the player
        /// </summary>
        public Bullet myBullet;

        /// <summary>
        /// the eagle that exists as part of the player
        /// </summary>
        public Eagle myEagle;

        /// <summary>
        /// Is the player currently using fear
        /// </summary>
        public bool fearOn;
       
        public Player(Texture2D loadedTex, Eagle myEagle, Bullet myBullet)
            : base(loadedTex)
        {
            health = 100;
            isInvulnerable = false;
            rateOfFire = 1;
            pupDuration = 30;
            killCount = 0;
            hasPup = false;
            score = 0;
            canFire = true;
            canUseStrike = true;
            canUseFear = true;
            isJumping = false;
            isFalling = false;
            isColliding = false;
            myBullet.visible = false;
            myEagle.visible = false;

        }

        /// <summary>
        /// move the player based on the ks parameter
        /// </summary>
        /// <param name="ks"></param>
        public void Move(KeyboardState ks)
        {
            if(ks.IsKeyDown(Keys.Left)) 
            {
                this.pos.X -= MOVE_DISTANCE;
                facing = Facing.Left;
            }
            else if (ks.IsKeyDown(Keys.Right))
            {
                this.pos.X += MOVE_DISTANCE;
                facing = Facing.Right;
            }
        }


        /// <summary>
        /// player uses one of four attacks based on ks parameter
        /// </summary>
        /// <param name="ks"></param>
        public void Attack(KeyboardState ks, List<Enemy> enemyList)
        {
            if(ks.IsKeyDown(Keys.Z)) 
            {
                //RAISE UR ERECTIONS
                Rectangle meleeHurtBox = new Rectangle((int)this.pos.X + tex.Width, (int)this.pos.Y, MELEE_RANGE, this.rect.Height);
                foreach (Enemy e in enemyList)
                {
                    if (meleeHurtBox.Intersects(e.rect))
                    {
                        e.health -= 1;
                    }

                }
            }
            else if (ks.IsKeyDown(Keys.X))
            {
                if (this.canFire)
                {
                    myBullet.visible = false;
                    this.canFire = false;
                }
                else
                {
                    myBullet.visible = true;
                    myBullet.pos.X = this.origin.X;
                    myBullet.pos.Y = this.origin.Y;
                    myBullet.moveBullet(this);
                }


            }
            else if (ks.IsKeyDown(Keys.C))
            {
                if (this.canUseStrike)
                {
                    myEagle.visible = false;
                    this.canFire = false;
                }
                else
                {
                    myEagle.visible = true;
                    myEagle.pos.X = this.pos.X;
                    myEagle.pos.Y = 0;
                }
            }
            else if (ks.IsKeyDown(Keys.V))
            {
                if (this.canUseFear)
                {
                    fearOn = true;
                    hasPup = true;
                }
            }
        }

        /// <summary>
        /// check if the player has a pup and then use the pup
        /// </summary>
        /// <param name="p"></param>
        public void GetPup(Pup p)
        {
            //RAISE UR PUPPIES
            Vector2 pickupRange = new Vector2(this.pos.X + 10, this.pos.Y);
            if ((p.pos.X == pickupRange.X) && (!this.hasPup))
            {
                this.hasPup = true;
                p.UsePup(this);
                
            }

        }

        /// <summary>
        /// Player jumps and can still move
        /// </summary>
        public void Jump(KeyboardState ks, KeyboardState ls, World world)
        {
                if ((ks.IsKeyDown(Keys.Up)) && (ls.IsKeyUp(Keys.Up)))
                {
                    /*
                    if (this.pos.Y < FLOOR_HEIGHT - tex.Height / 2)
                    {
                        isJumping = false;
                        isFalling = false;
                    }
                    else
                     */
                    isJumping = true;
                    isFalling = false;
                }

                if (isJumping)
                {
                    this.pos.Y -= JUMP_HEIGHT;
                }

                if (this.pos.Y + tex.Height / 2 <= MAX_JUMP_HEIGHT)
                {
                    isJumping = false;
                    isFalling = true;
                }

                if (isFalling)
                {
                    if (this.pos.Y + tex.Height / 2 < world.GROUND_HEIGHT)
                    {
                        this.pos.Y += JUMP_HEIGHT;
                    }
                    else
                    {
                        isJumping = false;
                        isFalling = false;
                        this.pos.Y = world.GROUND_HEIGHT - tex.Height / 2;
                    }
                }   
        }

        /// <summary>
        /// Revert the player back to original state when the pup has ended
        /// </summary>
        public void RevertPlayer()
        {
            if (pupDuration == 0)
            {
                isInvulnerable = false;
                fearOn = false;
                rateOfFire = 1;
                hasPup = false;
            }

            if (!isColliding)
            {
                this.vel = PLAYER_VELOCITY;
            }

        }

        /// <summary>
        /// decrements the pup duration by one
        /// </summary>
        public void DecrementPupDuration()
        {
            pupDuration -= 1;
        }


    }
}
