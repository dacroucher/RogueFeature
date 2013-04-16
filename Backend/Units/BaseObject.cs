using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class BaseObject : Unit
    {
        public bool interactable;

        public BaseObject(int x, int y, String imgPath, Direction face, String name, bool passable, bool interactable) :base(x,y,imgPath, face, name, passable)
        {
            this.interactable = interactable;
        }

        public virtual void Interact(Mobile m)
        {
            if (!interactable)
                return;
        }
    }
}
