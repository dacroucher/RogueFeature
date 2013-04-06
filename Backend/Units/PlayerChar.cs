using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class PlayerChar : Mobile
    {
        
        public PlayerChar(int x, int y, String imgPath, Direction face, int hitsMax)
            : base(x, y, imgPath, face, hitsMax)
        {

        }
    }
}
