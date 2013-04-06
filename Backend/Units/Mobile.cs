using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Mobile : Unit
    {
        public int hitsMax;
        public int hits;
        public int atk;
        public int def;
        
        public Mobile(int x, int y, String imgPath, Direction face, int hitsMax) :base(x,y,imgPath,face, false)
        {
            this.hitsMax = hitsMax;
            hits = hitsMax;            
        }

        public virtual void TakeHit(Mobile m)
        {
            hits = hits - (m.atk - def);            
            if(hits <= 0)
                Die();
        }

        protected virtual void Die()
        {
            Core.delta.DeltaRemove(this);            
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
                if (Math.Abs(diffX) > Math.Abs(diffY)) //try x first
                {
                    if (diffX < 0) 
                    {
                        if (m.Passable(x-1, y))
                        {
                            x--;
                            Core.delta.DeltaEdit(this);
                        }
                        else
                        {
                            AttemptY(diffY, m); // try y second
                        }
                    }
                    else
                    {
                        if (m.Passable(x+1, y))
                        {
                            x++;
                            Core.delta.DeltaEdit(this);
                        }
                        else
                        {
                            AttemptY(diffY, m);// try y second
                        }
                    }
                }
                else //try y first
                {
                    if (diffY < 0)
                    {
                        if (m.Passable(y - 1, y))
                        {
                            y--;
                            Core.delta.DeltaEdit(this);
                        }
                        else
                        {
                            AttemptX(diffX, m); //try x second
                        }
                    }
                    else
                    {
                        if (m.Passable(x + 1, y))
                        {
                            y++;
                            Core.delta.DeltaEdit(this);
                        }
                        else
                        {
                            AttemptX(diffX, m); //try x second
                        }
                    }
                }
            }
            return false;
        }

        private void AttemptX(int diffX, Map m)
        {
            if (diffX < 0)
            {
                if (m.Passable(x - 1, y))
                {
                    x--;
                    Core.delta.DeltaEdit(this);
                }
            }
            else
            {
                if (m.Passable(x + 1, y))
                {
                    x++;
                    Core.delta.DeltaEdit(this);
                }
            }
        }

        private void AttemptY(int diffY, Map m)
        {
            if (diffY < 0)
            {
                if (m.Passable(y - 1, y))
                {
                    y--;
                    Core.delta.DeltaEdit(this);
                }
            }
            else
            {
                if (m.Passable(x + 1, y))
                {
                    y++;
                    Core.delta.DeltaEdit(this);
                }
            }
        }
        
        



                
    }
}
