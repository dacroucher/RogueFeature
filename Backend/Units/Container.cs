using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class Container : BaseObject
    {
        public bool locked;
        public bool open;

        public Container(int x, int y, String imgPath, Direction face, String name) :base(x,y,imgPath,face, name, false,true)
        {
            locked = false;
            open = false;
        }

        public void Lock()
        {
            locked = true;
        }

        public override void Interact(Mobile m)
        {
            if (locked)
                return;
            if (open)
            {
                open = false;
                Core.delta.DeltaEdit(this);
            }
            else
            {
                open = true;
                Core.delta.DeltaEdit(this);
            }
        }
        
    }
}
