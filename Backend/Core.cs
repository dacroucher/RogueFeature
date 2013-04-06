using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Backend.Units;

namespace Backend
{
    public class Core
    {
        private Map _grid;
        private PlayerChar _pc;
        public delegate void ComputeComplete(object sender, EventArgs e);
        public event ComputeComplete Computed;

        public Core(int Rows, int Columns)
        {
            _grid = new Map(Rows, Columns);
        }

        public void Compute()
        {
                            
        }

        public void PlayerMove(Direction d)
        {

            ComputeAI();
            
        }


        public void ComputeAI()
        {

        }
        

        

        
    }
}
