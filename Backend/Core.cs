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
        public Map Map { get { return _map; } }        
        public static DeltaMap delta = new DeltaMap();

        public static PlayerChar pc; //registered when new PlayerChar() is called

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
            foreach (Mobile m in _map.GetMobilesInRange(pc.x, pc.y, 7))
            {
                ((Actor)m).Act(_map, pc);
            }
        }

        //Performs the player action in direction d
        private void Action(Direction d)
        {
            //Check for move
            int xCheck = pc.x;
            int yCheck = pc.y;
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

            if (_map.Passable(xCheck, yCheck)) //Check if target area is passable
            {                
                //move if passable                
                pc.Move(xCheck, yCheck);
                
                //interact with items on the square
                foreach (BaseObject bo in _map.GetObjects(xCheck,yCheck))
                {
                    bo.Interact(pc);
                }
            }
            else //Otherwise interact with whatever is in the unpassable block
            {                               
                if (_map.Occupied(xCheck,yCheck))
                {
                    _map.GetMobile(xCheck, yCheck).TakeHit(pc);
                }
                else
                {
                    foreach (BaseObject bo in _map.GetObjects(xCheck, yCheck))
                    {
                        bo.Interact(pc);
                    }
                }
            }
            
        }

        

        
    }
}
