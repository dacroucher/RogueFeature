using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Loader;

namespace RogueFeature
{
    public class GameHandle
    {
        public XMLLoader xmlLoader;
        public GameHandle()
        {
            xmlLoader = new XMLLoader();
            xmlLoader.loadXMLFile();
        }
    }
}
