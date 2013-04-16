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
        private string _imgID;        
        private Direction _dir;
        private bool _passable;
        private Mobile _mob;
        private List<BaseObject> _objects;

        public bool passable { get { return _passable; } }
        public bool Occupied { get { return _mob != null; } }
        public List<Unit> Units { get { return GenerateUnitList(); } }
        public string ImgID { get { return _imgID; } }
        

        public Point(Map parent, String imageID, Direction face, bool passable)
        {
            _parent = parent;
            _objects = new List<BaseObject>();
            _imgID = imageID;
            _dir = face;
            _passable = passable;
        }

        public void AddUnit(Unit u)
        {
            if (u is BaseObject)
            {
                _objects.Add((BaseObject)u);
            }
            else if (u is Mobile)
            {
                if (_mob != null)
                    throw new Exception("Cannot add a mobile to an already occupied point");
                _mob = (Mobile)u;
            }
        }

        public void RemoveUnit(Unit u)
        {
            if (u is BaseObject)
            {
                if(_objects.Contains(u))
                    _objects.Remove((BaseObject)u);
            }
            else if (u is Mobile)
            {
                if (_mob == u)
                    _mob = null;
                else
                    throw new Exception("Cannot remove mobile, it is not occupying this point");
            }            
        }


        public void Migrate(Unit u, int x, int y)
        {
            RemoveUnit(u);
            _parent.AddUnitToPoint(x, y, u);
            Core.delta.DeltaEdit(u);
        }

        private List<Unit> GenerateUnitList()
        {
            List<Unit> units = new List<Unit>(_objects);
            units.Add(_mob);
            return units;
        }

        public Unit[] UnitList()
        {
            return GenerateUnitList().ToArray();
        }

        public Mobile GetMobile()
        {
            return _mob;
        }

        public BaseObject[] GetObjects()
        {
            return _objects.ToArray();
        }
        

    }
}
