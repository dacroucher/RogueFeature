using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;

namespace RogueFeature.Backend
{
    
    public class Core
    {
        //Event for a completely computed turn. ComputedEventArgs.deltaMap is an array of DeltaMapUnits containing all information!
        public delegate void ComputeComplete(object sender, ComputedEventArgs e);
        public event ComputeComplete Computed;

        private Map _map;
        private PlayerChar _pc;        
        public static DeltaMap delta = new DeltaMap();
        

        //Initialise a core with a loaded map
        public Core(Map map)
        {
            _map = map;            
        }

        //Set the current play map
        public void SetMap(Map map)
        {
            _map = map;
        }
        
        //Signal a player has input a move in a Direction d
        public void PlayerMove(Direction d)
        {
            Action(d);
            ComputeAI();
            if (Computed != null)
            {
                Computed(this, new ComputedEventArgs(delta.GetArray()));
                delta.Clear();
            }
        }

        //Computes the AI
        private void ComputeAI()
        {
            foreach (Mobile m in _map.GetMobilesInRange(_pc.x, _pc.y, 7))
            {
                m.Act(_map,_pc);
            }
        }

        //Performs the player action in direction d
        private void Action(Direction d)
        {
            //Check for move
            int xCheck = _pc.x;
            int yCheck = _pc.y;
            switch(d)
            {
                case Direction.UP:
                    yCheck--;
                    break;
                case Direction.DOWN:
                    yCheck++;
                    break;
                case Direction.LEFT:
                    xCheck--;
                    break;
                case Direction.RIGHT:
                    xCheck++;
                    break;
                default: 
                    break;
            }

            if (!_map.BoundCheck(xCheck, yCheck))
                return;
            Unit[] units = _map.GetUnits(xCheck, yCheck);

            if (_map.Passable(xCheck, yCheck)) //Check if target area is passable
            {                
                //move if passable                
                _pc.Move(xCheck, yCheck);
                
                //interact with items on the square
                foreach (Unit u in units)
                {
                    if (u is BaseObject)
                        ((BaseObject)u).Interact(_pc);
                }
            }
            else //Otherwise interact with whatever is in the unpassable block
            {                               
                foreach(Unit u in units)
                {
                    if(u is Mobile)
                    {
                        ((Mobile)u).TakeHit(_pc);
                    }
                    else if (u is BaseObject)
                    {                        
                        ((BaseObject)u).Interact(_pc);
                    }
                }
            }
            
        }

        

        
    }
}
