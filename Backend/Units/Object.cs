using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Units
{
    public class Object : Unit
    {
        protected bool _interactable;

        public Object(int x, int y, int id, Direction face, bool passable, bool interactable) :base(x,y,id, face, passable)
        {
            _interactable = interactable;
        }
    }
}
