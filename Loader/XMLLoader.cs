using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Backend;

namespace Loader
{
    public class XMLLoader
    {
        private string _XMLFileName;
        private string _levelID;
        private List<Map> _map;
        List<XElement> _Level;

        public XMLLoader(String XMLFileName, String levelID)
        {
            _XMLFileName = XMLFileName;
            _levelID = levelID;
            try
            {
                XMLLoad();
            }
            catch (Exception exception)
            {
                Console.WriteLine("XML Import error: " + exception.ToString());
            }
        }

        private void XMLLoad()
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(_XMLFileName);
            int rows, columns;

            foreach (XElement map in doc.Descendants("map"))
            {
                rows = Convert.ToInt32(map.Descendants("rows").FirstOrDefault().Value);
                columns = Convert.ToInt32(map.Descendants("columns").FirstOrDefault().Value);
                _map.Add(new Map(rows, columns));
                foreach (XElement point in map.Descendants("point").ToList())
                {
                    Console.WriteLine("X: " + point.Descendants("xposition").FirstOrDefault().Value);
                    Console.WriteLine("Y: " + point.Descendants("yposition").FirstOrDefault().Value);
                }
            }

        }


    }
}
