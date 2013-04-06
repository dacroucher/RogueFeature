using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Units;

namespace Backend
{
    public class Grid
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

        public Grid(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            Init();
        }

        private void Init()
        {
            for (int x = 0; x < _rows; x++)
            {
                for (int y = 0; y < _columns; y++)
                {
                    _points[x][y] = new Point(this);
                }
            }
        }

        public void AddUnitToPoint(int x, int y, Unit u)
        {
            _points[x][y].AddUnit(u);
        }
    }
}
