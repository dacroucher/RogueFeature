using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend;

namespace Backend.Units
{
    
    public class Unit
    {
        
        protected int _x;
        protected int _y;
        protected int _imgId;
        protected Direction _dir;
        protected bool _passable;

        public Unit(int x, int y, int imageID, Direction face, bool passable)
        {
            _x = x;
            _y = y;
            _imgId = imageID;
            _dir = face;
        }
            
    }
}
