using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Loader;

namespace XMLTest
{
    [TestClass]
    public class XMLTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            XMLLoader lvl1 = new XMLLoader("map.xml", "lvl1");
            Assert.AreEqual("", lvl1.getData());
        }
    }
}
