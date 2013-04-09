using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;

namespace RogueFeature.Backend
{
    public class Point
    {
        private Map _parent;
        public List<Unit> Units { get { return _units; } }
        private List<Unit> _units;
        private string _imgID;
        public string ImgID { get { return _imgID; } }
        private Direction _dir;
        private bool _passable;
        public bool passable { get { return _passable; } }

        public Point(Map parent, String imageID, Direction face, bool passable)
        {
            _parent = parent;
            _units = new List<Unit>();
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

        public Unit[] UnitList()
        {
            return _units.ToArray();
        }

        public void Migrate(Unit u, int x, int y)
        {
            RemoveUnit(u);
            _parent.AddUnitToPoint(x, y, u);
            Core.delta.DeltaEdit(u);
        }

    }
}
