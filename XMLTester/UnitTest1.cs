using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loader;

namespace XMLTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestXMLImport()
        {
            XMLLoader load = new XMLLoader("map.xml", "lvl1");
        }
    }
}
