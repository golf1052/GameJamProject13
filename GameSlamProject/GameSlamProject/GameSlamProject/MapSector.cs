using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameSlamProject
{
    public class MapSector
    {
        public Rectangle start;
        public Rectangle republican;
        public Rectangle democrat;
        public Rectangle boss;
        public World w;

        public MapSector(Rectangle start, Rectangle republican, Rectangle democrat, Rectangle boss, World w)
        {
            start = new Rectangle(0, 0, w.START_SIZE, 768);
            republican = new Rectangle(w.START_SIZE, 0, w.REPUBLICAN_SIZE, 768);
            democrat = new Rectangle(w.START_SIZE + w.REPUBLICAN_SIZE, 0, w.DEMOCRAT_SIZE, 768);
            boss = new Rectangle(w.START_SIZE + w.REPUBLICAN_SIZE + w.DEMOCRAT_SIZE + w.BOSS_SIZE, 0, w.BOSS_SIZE, 768);
        }

    }
}
