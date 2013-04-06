using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Corpse : BaseObject
    {
        public Corpse(int x, int y, String imgPath, Direction face) : base(x,y,imgPath,face,true,false)
        {

        }
    }
}
