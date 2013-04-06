using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Units;

namespace Backend
{
    public class Map
    {
        private int _rows;
        private int _columns;
        public int rows
        {
            get
            {
                return _rows;
            }
        }
        public int columns
        {
            get
            {
                return _columns;
            }
        }

        private Point[][] _points;

        public Map(int rows, int columns)
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
    }
}
