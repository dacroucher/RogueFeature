﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;

namespace RogueFeature.Backend
{
    
    public class Core
    {
        private Map _map;
        private PlayerChar _pc;
        public delegate void ComputeComplete(object sender, EventArgs e);
        public event ComputeComplete Computed;
        public static DeltaMap delta = new DeltaMap();               

        public Core(Map map)
        {
            _map = map;            
        }

        public void SetMap(Map map)
        {
            _map = map;
        }
        
        public void PlayerMove(Direction d)
        {
            Action(d);
            ComputeAI();
            if (Computed != null)
                Computed(this, new ComputedEventArgs(delta));
        }


        private void ComputeAI()
        {
            foreach (Mobile m in _map.GetSurroundingMobiles(_pc.x, _pc.y, 15))
            {
                m.Act(_map,_pc);
            }
        }

        private void Action(Direction d)
        {
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

            Unit[] units = _map.GetUnits(xCheck, yCheck);

            if (_map.Passable(xCheck, yCheck))
            {
                _pc.x = xCheck;
                _pc.y = yCheck;
                delta.DeltaEdit(_pc);
                
                foreach (Unit u in units)
                {
                    if (u is BaseObject)
                        ((BaseObject)u).Interact(_pc);
                }
            }
            else
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
