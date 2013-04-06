using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class BaseObject : Unit
    {
        public bool interactable;

        public BaseObject(int x, int y, String imgPath, Direction face, bool passable, bool interactable) :base(x,y,imgPath, face, passable)
        {
            this.interactable = interactable;
        }

        public virtual void Interact(PlayerChar pc)
        {
            if (!interactable)
                return;            
        }
    }
}
