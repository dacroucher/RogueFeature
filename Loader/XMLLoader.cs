using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using RogueFeature.Backend;

namespace Loader
{
    public class XMLLoader
    {
        private string _XMLFileName;
        private string _levelID;
        private List<Map> _map;
        List<XElement> _Level;

        public XMLLoader()
        {
            _XMLFileName = "map.xml";
            try
            {
                XMLLoad();
            }
            catch (Exception exception)
            {
                Console.WriteLine("XML Import error: " + exception.ToString());
            }
        }

        private Direction intToDirection(int dir)
        {
            switch (dir)  //convert number in xml to direction enum
            {
                case 0:
                    return Direction.UP;
                    break;
                case 1:
                    return Direction.RIGHT;
                    break;
                case 2:
                    return Direction.DOWN;
                    break;
                case 3:
                    return Direction.LEFT;
                    break;
                default:
                    return Direction.UP;
                    break;
            }
        }

        private void XMLLoad()
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(_XMLFileName);
            uint rows, columns;

            //GET: Each level
            foreach (XElement map in doc.Descendants("map"))    
            {
                rows = Convert.ToUInt32(map.Descendants("rows").FirstOrDefault().Value);
                columns = Convert.ToUInt32(map.Descendants("columns").FirstOrDefault().Value);
                _map.Add(new Map(rows, columns));       //Create a new map, size rows x columns
                Map latestMap = _map[_map.Count - 1];   //last added map reference
                //GET: Each point
                foreach (XElement point in map.Descendants("point").ToList())   
                {
                    int xPosition = Convert.ToInt32(point.Descendants("xposition").FirstOrDefault().Value);
                    int yPosition = Convert.ToInt32(point.Descendants("yposition").FirstOrDefault().Value);
                    string imagePath = point.Descendants("imagePath").FirstOrDefault().Value;
                    int direction = Convert.ToInt32(point.Descendants("direction").FirstOrDefault().Value);
                    bool passable = Convert.ToBoolean(point.Descendants("passable").FirstOrDefault().Value);
                    latestMap.InitPoint(xPosition, yPosition, imagePath, intToDirection(direction), passable);  //Initialize point
                    //GET: All units at point
                    foreach (XElement unit in point.Descendants("object").ToList())   
                    {
                        string unitid = unit.Attribute("id").Value;
                        XElement theunit = (from el in doc.Elements("object")
                         where (string)el.Attribute("id") == unitid
                         select el).FirstOrDefault();
                        string type = theunit.Descendants("type").FirstOrDefault().Value;
                        switch (type)
                        {
                            case "Unit":

                                break;
                        }

                    }
                }
            }

        }


    }
}
