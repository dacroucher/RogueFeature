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
        private uint _id;
        private string _name;
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

        public uint ID
        {
            get
            {
                return _id;
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Point[,] Points { get { return _points; } }
        private Point[,] _points;

        public Map(uint rows, uint columns, uint id, string name)
        {
            _rows = rows;
            _columns = columns;
            _id = id;
            _name = name;
            _points = new Point[rows,columns];
        }

        //Initialise a point in the map
        public void InitPoint(int x, int y, String imagePath, Direction face, bool pass)
        {                                        
                try
                {
                    _points[x, y] = new Point(this, imagePath, face, pass);
                }
                catch (IndexOutOfRangeException)
                {
                    throw new IndexOutOfRangeException("Attempted to intialise a point outside the bounds of map");
                }
        }
        
        //Add a unit to a point in the map
        public void AddUnitToPoint(int x, int y, Unit u)
        {
            try
            {
                if (_points[x,y] == null)
                    throw new NullReferenceException("Point " + x + ";" + y + " is not initialised");
                _points[x,y].AddUnit(u);
                u.SetPoint(_points[x,y]);
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException("Attempted to add unit to a point outside the bounds of map");
            }            
        }
        
        //Check if a point is passable
        public bool Passable(int x, int y)
        {
            if (BoundCheck(x, y))
                return false;
            Point p = _points[x,y];
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

        //Get a list of units from a point
        public Unit[] GetUnits(int x, int y)
        {            
            try
            {
                return _points[x,y].UnitList();
            }
            catch (IndexOutOfRangeException)
            {
                throw new Exception("Attempted to retrieve a unit list from a point outside bounds of map.");
            }
        }

        #region RangeCheck
        //Perform a sequence of box searches surrounding origin x;y up to a range to find all mobiles
        public Mobile[] GetMobilesInRange(int originX, int originY, int range)
        {
            List<Mobile> mobs = new List<Mobile>();
            for (int i = 1; i < range; i++)
            {
                mobs.AddRange(TileBoxSearch(originX - i, originY - i, (i*2) + 1));
            }
            
            return mobs.ToArray();
        }
        private List<Mobile> TileBoxSearch(int xTopLeft, int yTopLeft, int sideLength)
        {
            List<Mobile> mobs = new List<Mobile>();
            
            //top bar R2L
            for (int i = 0; i < sideLength; i++ )
            {
                if(BoundCheck(xTopLeft + i, yTopLeft))
                    if(Occupied(xTopLeft + i, yTopLeft))
                    mobs.Add(GetMobile(xTopLeft + i, yTopLeft));
            }

            //right bar T2B
            for (int j = 1; j < sideLength; j++)
            {
                if(BoundCheck(xTopLeft + sideLength - 1, yTopLeft + j))
                    if(Occupied(xTopLeft + sideLength - 1, yTopLeft + j))
                    mobs.Add(GetMobile(xTopLeft + sideLength - 1, yTopLeft + j));
            }
            
            //left bar T2B
            for (int j = 1; j < sideLength; j++)
            {
                if(BoundCheck(xTopLeft, yTopLeft + j))
                    if(Occupied(xTopLeft, yTopLeft + j))
                    mobs.Add(GetMobile(xTopLeft, yTopLeft + j));
            }

            //bottom bar R2L
            for (int i = 1; i < sideLength - 1; i++)
            {
                if (BoundCheck(xTopLeft + i, yTopLeft + sideLength - 1))
                    if(Occupied(xTopLeft + i, yTopLeft + sideLength - 1))
                    mobs.Add(GetMobile(xTopLeft + i, yTopLeft + sideLength - 1));
            }

            return mobs;
        }
        #endregion

        //Check if a point x;y is within the column/row bounds of map
        public bool BoundCheck(int x, int y)
        {
            if (x > _columns || x < 0)
            {
                return false;
            }
            if (y > _rows || y < 0)
            {
                return false;
            }
            return true;
        }

        public bool Occupied(int x, int y)
        {
            return _points[x, y].Occupied;
        }

        public BaseObject[] GetObjects(int x, int y)
        {
            return _points[x, y].GetObjects();
        }

        public Mobile GetMobile(int x, int y)
        {
            return _points[x, y].GetMobile();
        }


    }
}
