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
        // Path to XML data file.
        private string _XMLFileName;
        // Data structure where XML data is stored.
        private List<Map> _map = new List<Map>();

        /// <summary>
        /// Constructor. Loads from map.xml into a list of map objects.
        /// </summary>
        /// <remarks>
        /// Automatically loads data into data structure when constructed.
        /// </remarks>
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

        /// <summary>
        /// Takes an integer and maps it to the direction enum with the 
        /// following mapping:
        /// 0 -> UP
        /// 1 -> RIGHT
        /// 2 -> DOWN
        /// 3 -> LEFT
        /// </summary>
        /// <param name="dir">The integer representing direction</param>
        /// <returns>
        /// Returns the direction enum represented by the dir parameter.
        /// </returns>
        /// <remarks>
        /// This is a private function used internally.
        /// </remarks>
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

        /// <summary>
        /// Returns the data structure generated during construction.
        /// </summary>
        /// <returns>
        /// A list of Map structures, each representing a level in the game.
        /// </returns>
        public List<Map> getMapList()
        {
            return _map;
        }

        /// <summary>
        /// This is the main function which does all the processing. It reads in 
        /// from the XML file and processes the data into the memory data structure
        /// </summary>
        /// <remarks>
        /// This function is run when the class is constructed. It is private.
        /// </remarks>
        private void XMLLoad()
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(_XMLFileName);
            uint rows, columns;

            // GET: Each level in the game.
            foreach (XElement map in doc.Descendants("map"))    
            {
                rows = Convert.ToUInt32(map.Descendants("rows").FirstOrDefault().Value);
                columns = Convert.ToUInt32(map.Descendants("columns").FirstOrDefault().Value);
                _map.Add(new Map(rows, columns));    
                // Reference to the last added map.
                Map latestMap = _map[_map.Count - 1];  
                // GET: Each point in the map.
                foreach (XElement point in map.Descendants("point").ToList())   
                {
                    int xPosition = Convert.ToInt32(point.Descendants("xposition").FirstOrDefault().Value);
                    int yPosition = Convert.ToInt32(point.Descendants("yposition").FirstOrDefault().Value);
                    string imagePath = point.Descendants("imagePath").FirstOrDefault().Value;
                    int direction = Convert.ToInt32(point.Descendants("direction").FirstOrDefault().Value);
                    bool passable = Convert.ToBoolean(point.Descendants("passable").FirstOrDefault().Value);
                    // Initialize point.
                    latestMap.InitPoint(xPosition, yPosition, imagePath, intToDirection(direction), passable);  
                    // GET: All units at the point.
                    foreach (XElement unit in point.Descendants("object").ToList())   
                    {
                        string unitid = unit.Attribute("id").Value;
                        XElement theunit = (from el in doc.Elements("object")
                         where (string)el.Attribute("id") == unitid
                         select el).FirstOrDefault();
                        string type = theunit.Descendants("type").FirstOrDefault().Value;
                        // Variables for the switch case.
                        String name, imgPath;
                        bool pass, inter;
                        int dir, hitsMax, attack, defense;
                        // Determine type of unit and add to the point.
                        switch (type)
                        {
                            case "Unit":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                pass = Convert.ToBoolean(theunit.Descendants("passable").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Unit(xPosition, yPosition, imgPath, intToDirection(dir), name, pass));
                                break;

                            case "BaseObject":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                pass = Convert.ToBoolean(theunit.Descendants("passable").FirstOrDefault().Value);
                                inter = Convert.ToBoolean(theunit.Descendants("interactable").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.BaseObject(xPosition, yPosition, imgPath, intToDirection(dir), name, pass, inter));
                                break;

                            case "Mobile":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                hitsMax = Convert.ToInt32(theunit.Descendants("hitsMax").FirstOrDefault().Value);
                                attack = Convert.ToInt32(theunit.Descendants("attack").FirstOrDefault().Value);
                                defense = Convert.ToInt32(theunit.Descendants("defense").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Mobile(xPosition, yPosition, imgPath, intToDirection(dir), name, hitsMax, attack, defense));
                                break;

                            case "PlayerChar":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                hitsMax = Convert.ToInt32(theunit.Descendants("hitsMax").FirstOrDefault().Value);
                                attack = Convert.ToInt32(theunit.Descendants("attack").FirstOrDefault().Value);
                                defense = Convert.ToInt32(theunit.Descendants("defense").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.PlayerChar(xPosition, yPosition, imgPath, intToDirection(dir), name, hitsMax, attack, defense));
                                break;

                            case "Container":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Container(xPosition, yPosition, imgPath, intToDirection(dir), name));
                                break;

                            case "Item":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Item(xPosition, yPosition, imgPath, intToDirection(dir), name));
                                break;

                            case "Corpse":
                                name = theunit.Descendants("name").FirstOrDefault().Value;
                                imgPath = theunit.Descendants("imagePath").FirstOrDefault().Value;
                                dir = Convert.ToInt32(theunit.Descendants("direction").FirstOrDefault().Value);
                                latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Corpse(xPosition, yPosition, imgPath, intToDirection(dir), name));
                                break;

                            default:
                                break;
                        }

                    }
                }
            }

        }


    }
}
