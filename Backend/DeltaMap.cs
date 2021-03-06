﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RogueFeature.Backend.Units;

namespace RogueFeature.Backend
{
    public enum Delta {ADD, REMOVE, EDIT};

    public struct DeltaUnitPair
    {
        public Unit unit;
        public Delta delta;
        public DeltaUnitPair(Delta d, Unit u)
        {
            unit = u;
            delta = d;
        }
    }

    
    public class DeltaMap
    {
        private List<DeltaUnitPair> _deltaList;        
        
        private DeltaMap(List<DeltaUnitPair> toClone)
        {
            _deltaList = new List<DeltaUnitPair>(toClone);
        }

        public DeltaMap()
        {            
            _deltaList = new List<DeltaUnitPair>();
        }

        public void DeltaAdd(Unit u)
        {
            _deltaList.Add(new DeltaUnitPair(Delta.ADD, u));
        }

        public void DeltaRemove(Unit u)
        {
            _deltaList.Add(new DeltaUnitPair(Delta.REMOVE, u));
        }

        public void DeltaEdit(Unit u)
        {
            _deltaList.Add(new DeltaUnitPair(Delta.EDIT, u));
        }

        public DeltaUnitPair[] GetArray()
        {
            DeltaUnitPair[] arr = _deltaList.ToArray();
            _deltaList.Clear();
            return arr;
        }

        public DeltaMap Clone()
        {
            return new DeltaMap(_deltaList);
        }

        public void Clear()
        {
            _deltaList.Clear();            
        }
    }


    
}
