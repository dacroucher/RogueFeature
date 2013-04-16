using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Actor : Mobile
    {
        public Actor(int x, int y, String imgPath, Direction face, String name, int hitsMax, int attack, int defense) : base (x,y,imgPath,face,name,hitsMax,attack,defense)
        {

        }

        public virtual void Act(Map m, PlayerChar pc)
        {
            if (Pathfind(pc.x, pc.y, m))
            {
                pc.TakeHit(this);
            }
            else
            {
                
            }
        }


        #region Pathfinding
        protected bool Pathfind(int tarX, int tarY, Map m)
        {
            int diffX = tarX - x;
            int diffY = tarY - y;

            if (Math.Abs(diffX) == 1 && Math.Abs(diffY) == 1)
            {
                return true; //found target
            }
            else
            {
                if (Math.Abs(diffX) >= Math.Abs(diffY)) //try x first
                {
                    if (AttemptX(diffX, m))
                    {

                    }
                    else
                    {
                        AttemptY(diffY, m);
                    }
                }
                else //try y first
                {
                    if(AttemptY(diffY, m))
                    {

                    }
                    else
                    {
                        AttemptX(diffX, m);
                    }
                }
            }
            return false;
        }

        private bool AttemptX(int diffX, Map m)
        {
            if (diffX < 0)
            {
                if (m.Passable(x - 1, y))
                {                    
                    x--;                    
                    _point.Migrate(this, x, y);
                    return true;                    
                }
            }
            else
            {
                if (m.Passable(x + 1, y))
                {
                    x++;
                    _point.Migrate(this, x, y);
                    return true;
                }
            }
            return false;
        }

        private bool AttemptY(int diffY, Map m)
        {
            if (diffY < 0)
            {
                if (m.Passable(y - 1, y))
                {
                    y--;
                    _point.Migrate(this, x, y);
                    return true;
                }
            }
            else
            {
                if (m.Passable(x + 1, y))
                {
                    y++;
                    _point.Migrate(this, x, y);
                    return true;
                }
            }
            return false;
        }
        #endregion


    }
}
