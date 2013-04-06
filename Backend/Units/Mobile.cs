using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Units
{
    public class Mobile : Unit
    {
        protected int _hitsMax;
        protected int _hits;
        
        public Mobile(int x, int y, int id, Direction face, int hitsMax) :base(x,y,id,face, false)
        {
            _hitsMax = hitsMax;
        }

                
    }
}
