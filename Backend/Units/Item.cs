using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Item : BaseObject
    {
        public Item(int x, int y, String imgPath, Direction face, String name) : base(x,y,imgPath,face,name,true,true)
        {

        }
        
        
    }
}
