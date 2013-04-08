using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend;

namespace RogueFeature.Backend.Units
{
    
    public class Unit
    {
        
        public int x;
        public int y;
        public String imgPath;
        public Direction dir;
        public bool passable;
        public String name;
        protected Point _point;

        public Unit(int x, int y, String imagePath, Direction face, String name, bool passable)
        {
            this.x = x;
            this.y = y;
            this.imgPath = imagePath;
            this.dir = face;
            this.passable = passable;
        }

        public virtual void SetPoint(Point p)
        {
            _point = p;
        }
            
    }
}
