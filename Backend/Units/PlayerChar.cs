using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Backend.Units
{
    public class PlayerChar : Mobile
    {
        public PlayerChar(int x, int y, int id, Direction face, int hitsMax)
            : base(x, y, id, face, hitsMax)
        {

        }
    }
}
