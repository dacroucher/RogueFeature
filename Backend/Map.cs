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

        public void InitPoint(int x, int y, String imagePath, Direction face, bool pass)
        {
            _points[x][y] = new Point(this, imagePath, face, pass);
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

        public Mobile[] GetSurroundingMobiles(int baseX, int baseY, int range)
        {
            List<Unit> units = new List<Unit>();
            List<Mobile> mobs = new List<Mobile>();

            for (int i = 3; i < range; i++)
            {
                BoxSearch(baseX - (i / 2), baseY - (i / 2), i);
            }

            foreach (Unit u in units)
            {
                if (u is Mobile)
                {
                    mobs.Add((Mobile)u);
                }
            }
            return mobs.ToArray();
        }

        private List<Unit> BoxSearch(int originX, int originY, int length)
        {
            List<Unit> units = new List<Unit>();
            
            for (int i = 0; i < length; i++ )
            {
                units.AddRange(GetUnits(i, originY));
            }

            for (int j = 1; j < length; j++)
            {
                units.AddRange(GetUnits(originX+1, j));
            }

            for (int i = 1; i < length; i++)
            {
                units.AddRange(GetUnits(originX +1 , originY + length-1));
            }

            for (int j = 1; j < length-1; j++)
            {
                units.AddRange(GetUnits(originX + length - 1, originY + j));
            }

            return units;
        }

        

    }
}
