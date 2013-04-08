using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend
{
    class ComputedEventArgs : EventArgs
    {

        DeltaUnitPair [] deltaMap;

        public ComputedEventArgs(DeltaUnitPair [] dm)
            : base()
        {
            deltaMap = dm;
        }


    }
}
