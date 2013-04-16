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
        
        public Mobile(int x, int y, String imgPath, Direction face, String name, int hitsMax, int attack, int defense) :base(x,y,imgPath,face, name, false)
        {
            this.hitsMax = hitsMax;
            hits = hitsMax;
            atk = attack;
            def = defense;
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
            _point.RemoveUnit(this);
        }

    }
}
