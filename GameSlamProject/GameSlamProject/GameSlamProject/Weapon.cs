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
    /// Holds weapon stats.
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// Types of weapons defined.
        /// </summary>
        public enum WeaponType
        {
            Pistol,
            Shotgun,
            SMG,
            Carbine,
            SniperRifle,
            Flamethrower,
            Custom
        }

        /// <summary>
        /// How many bullets are in one shot. Used in shotguns.
        /// </summary>
        public int bulletsInShot;

        /// <summary>
        /// How many bullets does the weapon hold in one clip.
        /// </summary>
        public int bulletsInClip;

        /// <summary>
        /// The spread of the shot from where the player is aiming in degrees.
        /// </summary>
        public int shotSpread;

        /// <summary>
        /// How much damage the attack/bullet should give.
        /// </summary>
        public int damageInBullet;

        /// <summary>
        /// How long it takes to reload the weapon. In seconds. This will probably change because I
        /// just realized how crazy using seconds was.
        /// </summary>
        public int reloadTime;

        /// <summary>
        /// From what I gather it determines the lowest value of how long it takes the weapon to fire
        /// between shots in rounds per minute.
        /// </summary>
        public double rateOfFireLow;
        
        /// <summary>
        /// Determines the highest value of how long it takes the weapon to fire betewen shots. Rounds per minute.
        /// </summary>
        public double rateOfFireHigh;

        /// <summary>
        /// How long it will take for the weapon to fire between shots in rounds per minute.
        /// </summary>
        public TimeSpan fireStartTime;

        /// <summary>
        /// Random for random numbers.
        /// </summary>
        public Random random = new Random();

        /// <summary>
        /// Used with WeaponType enum. Stores what weapon this weapon is.
        /// </summary>
        public WeaponType weaponType;

        /// <summary>
        /// Secondary constructor. Allows you to define a new weapon on the fly. I have no idea if it will work.
        /// </summary>
        /// <param name="bulletInShot">How many bullets are in one shot. Used in shotguns.</param>
        /// <param name="bulletInClip">How many bullets does the weapon hold in one clip.</param>
        /// <param name="shotDeviation">The spread of the shot from where the player is aiming in degrees.</param>
        /// <param name="damageFromBullet">How much damage the attack/bullet should give.</param>
        /// <param name="ROFL">Determines the lowest value of how long it takes the weapon to fire
        /// between shots in rounds per minute.</param>
        /// <param name="ROFH">Determines the highest value of how long it takes the weapon to fire
        /// between shots in rounds per minute.</param>
        /// <param name="reloadSpeed">How long it takes to reload the weapon. In seconds. This will probably change because I
        /// just realized how crazy using seconds was.</param>
        public Weapon(int bulletInShot, int bulletInClip, int shotDeviation, int damageFromBullet, int ROFL, int ROFH, int reloadSpeed)
        {
            weaponType = WeaponType.Custom;
            bulletsInShot = bulletInShot;
            bulletsInClip = bulletInClip;
            shotSpread = shotDeviation;
            damageInBullet = damageFromBullet;
            rateOfFireLow = ROFL;
            rateOfFireHigh = ROFH;
            reloadTime = reloadSpeed;
            fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
        }

        /// <summary>
        /// Main constructor. When making a new weapon the WeaponType must be passed to this constructor.
        /// Then it sets up all the weapon stats for that weapon.
        /// </summary>
        /// <param name="weaponKind">The type of weapon that you want to create.</param>
        public Weapon(WeaponType weaponKind)
        {
            if (weaponKind == WeaponType.Carbine)
            {
                weaponType = WeaponType.Carbine;
                bulletsInShot = 1;
                bulletsInClip = 30;
                shotSpread = 3;
                damageInBullet = 8;
                rateOfFireLow = 700;
                rateOfFireHigh = 950;
                reloadTime = 3;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }

            if (weaponKind == WeaponType.Flamethrower)
            {
                weaponType = WeaponType.Flamethrower;
                bulletsInShot = 1;
                bulletsInClip = 30000;
                shotSpread = 20;
                damageInBullet = 1;
                rateOfFireLow = 700;
                rateOfFireHigh = 950;
                reloadTime = 5;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }

            if (weaponKind == WeaponType.Pistol)
            {
                weaponType = WeaponType.Pistol;
                bulletsInShot = 1;
                bulletsInClip = 12;
                shotSpread = 7;
                damageInBullet = 4;
                rateOfFireLow = 1100;
                rateOfFireHigh = 1200;
                reloadTime = 1;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }

            if (weaponKind == WeaponType.Shotgun)
            {
                weaponType = WeaponType.Shotgun;
                bulletsInShot = 6;
                bulletsInClip = 6;
                shotSpread = 10;
                damageInBullet = 10;
                rateOfFireLow = 300;
                rateOfFireHigh = 350;
                reloadTime = 5;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }

            if (weaponKind == WeaponType.SMG)
            {
                weaponType = WeaponType.SMG;
                bulletsInShot = 1;
                bulletsInClip = 30;
                shotSpread = 5;
                damageInBullet = 6;
                rateOfFireLow = 700;
                rateOfFireHigh = 900;
                reloadTime = 2;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }

            if (weaponKind == WeaponType.SniperRifle)
            {
                weaponType = WeaponType.SniperRifle;
                bulletsInShot = 1;
                bulletsInClip = 20;
                shotSpread = 0;
                damageInBullet = 50;
                rateOfFireLow = 25;
                rateOfFireHigh = 75;
                reloadTime = 10;
                fireStartTime = TimeSpan.FromMinutes(1.0 / (double)random.Next((int)rateOfFireLow, (int)rateOfFireHigh));
            }
        }
    }
}
