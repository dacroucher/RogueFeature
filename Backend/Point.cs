using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Units;

namespace Backend
{
    public class Point
    {
        private Grid _parent;
        private List<Unit> _units;

        public Point(Grid parent)
        {
            _parent = parent;
        }

        public void AddUnit(Unit u)
        {
            if(_units.Contains(u))
                return;
            _units.Add(u);
        }

        public void RemoveUnit(Unit u)
        {
            if (!_units.Contains(u))
                return;
            _units.Remove(u);
        }

    }
}
