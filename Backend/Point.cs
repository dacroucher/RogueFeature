using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Units;

namespace Backend
{
    public class Point
    {
        private Map _parent;
        private List<Unit> _units;
        private int _imgID;
        private Direction _dir;
        private bool _passable;

        public Point(Map parent, int imageID, Direction face, bool passable)
        {
            _parent = parent;
            _imgID = imageID;
            _dir = face;
            _passable = passable;
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
