using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend
{
    class ComputedEventArgs : EventArgs
    {

        DeltaMap deltaMap;

        public ComputedEventArgs(DeltaMap dm)
            : base()
        {
            deltaMap = dm;
        }


    }
}
