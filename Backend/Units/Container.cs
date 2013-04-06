using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Units
{
    public class Container : Object
    {
        
        public Container(int x, int y, int id, Direction face) :base(x,y,id,face, false,true)
        {
            
        }
        
    }
}
