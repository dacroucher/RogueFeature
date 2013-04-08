using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Item : BaseObject
    {
        protected bool _consumed;
        public bool consumed { get { return _consumed; } }

        public Item(int x, int y, String imgPath, Direction face, String name) : base(x,y,imgPath,face,name,true,true)
        {
            _consumed = false;
        }
        
        
    }
}
