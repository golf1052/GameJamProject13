using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GameSlamProject
{
    class Player:Sprite
    {
        #region Constants
        const int MAX_JUMP_HEIGHT = 50;
        const int FLOOR_HEIGHT = 0;
        const int MOVE_DISTANCE = 5;
        const int JUMP_HEIGHT = 5;
        const int MELEE_RANGE = 90;
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
       
        public Player(Texture2D loadedTex)
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
        }

        /// <summary>
        /// move the player based on the ks parameter
        /// </summary>
        /// <param name="ks"></param>
        public void Move(KeyboardState ks)
        {
            KeyboardState newState = Keyboard.GetState();

            if(newState.IsKeyDown(Keys.Left)) 
            {
                this.pos.X -= MOVE_DISTANCE;
            }
            else if (newState.IsKeyDown(Keys.Right))
            {
                this.pos.X += MOVE_DISTANCE;        
            }
            else
            {
                //do nothing because left or right have not been pressed
            }
        }


        /// <summary>
        /// player uses one of four attacks based on ks parameter
        /// </summary>
        /// <param name="ks"></param>
        public void Attack(KeyboardState ks, List<Enemy> enemyList)
        {
            KeyboardState newState = Keyboard.GetState();

            if(newState.IsKeyDown(Keys.Z)) 
            {
                //use melee attack
                Rectangle meleeHurtBox = new Rectangle((int)this.pos.X + tex.Width, (int)this.pos.Y, MELEE_RANGE, this.rect.Height);
                foreach (Enemy e in enemyList)
                {

                }
            }
            else if (newState.IsKeyDown(Keys.X))
            {
                if (this.canFire)
                {
                    //use ranged attack
                }
            }
            else if (newState.IsKeyDown(Keys.C))
            {
                if (this.canUseStrike)
                {
                    //use Airforce One Strike
                }
            }
            else if (newState.IsKeyDown(Keys.V))
            {
                if (this.canUseFear)
                {
                    //use phobia attack
                }
            }
            else
            {
                // do nothing???
            }
        }

        /// <summary>
        /// check if the player has a pup and then use the pup
        /// </summary>
        /// <param name="p"></param>
        public void GetPup(Pup p)
        {
            Vector2 pickupRange = new Vector2(this.pos.X + 10, this.pos.Y);
            if ((p.pos.X = pickupRange.X) && (!this.hasPup))
            {
                this.hasPup = true;
                p.usePup(this);
            }

        }

        /// <summary>
        /// Player jumps and can still move
        /// </summary>
        public void Jump()
        {
            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Up))
            {
                isJumping = true;
                if ((isJumping) && (this.pos.Y < MAX_JUMP_HEIGHT) && (this.pos.Y > FLOOR_HEIGHT))
                {
                    this.pos.Y -= JUMP_HEIGHT;
                }
                else if (pos.Y == MAX_JUMP_HEIGHT)
                {
                    isJumping = false;
                    isFalling = true;

                    if ((isFalling) && (this.pos.Y < FLOOR_HEIGHT))
                    {
                        this.pos.Y += JUMP_HEIGHT;
                    }
                    else
                    {
                        isFalling = false;
                    }
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
                rateOfFire = 1;
                hasPup = false;
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
