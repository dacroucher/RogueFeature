using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Units
{
    public class Item : Object
    {
        public Item(int x, int y, int id, Direction face) : base(x,y,id,face,true,false)
        {

        }

    }
}
