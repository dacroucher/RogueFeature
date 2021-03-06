﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class PlayerChar : Mobile
    {
        
        public PlayerChar(int x, int y, String imgPath, Direction face,String name, int hitsMax, int attack, int defense)
            : base(x, y, imgPath, face, name, hitsMax, attack, defense)
        {
            Core.pc = this;
        }

        public void Move(int newX, int newY)
        {
            x = newX;
            y = newY;
            _point.Migrate(this, x, y);
        }

        
    }
}
