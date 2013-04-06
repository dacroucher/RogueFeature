using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;

namespace RogueFeature.Backend
{
    public class Map
    {
        private uint _rows;
        private uint _columns;
        public uint rows
        {
            get
            {
                return _rows;
            }
        }
        public uint columns
        {
            get
            {
                return _columns;
            }
        }

        private Point[][] _points;

        public Map(uint rows, uint columns)
        {
            _rows = rows;
            _columns = columns;            
        }

        public void InitPoint(int x, int y, int imgID, Direction face, bool pass)
        {
            _points[x][y] = new Point(this, imgID, face, pass);
        }

        
        public void AddUnitToPoint(int x, int y, Unit u)
        {
            _points[x][y].AddUnit(u);
           
        }

        public bool Passable(int x, int y)
        {
            Point p = _points[x][y];
            if (p.passable)
            {
                Unit[] units = p.UnitList();
                foreach (Unit u in units)
                {
                    if (!u.passable)
                        return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        public Unit[] GetUnits(int x, int y)
        {
            return _points[x][y].UnitList();
        }
    }
}
