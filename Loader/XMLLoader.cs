using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using RogueFeature.Backend;
using RogueFeature.DebugLogging;

namespace Loader
{
    public class XMLLoader
    {
        public class UnitObject
        {
            public string ID { set; get; }
            public int MaxHits { set; get; }
            public int Attack { set; get; }
            public int Defense { set; get; }
            public string Name { set; get; }
            public bool IsPassable { set; get; }
            public string ImagePath { set; get; }
            public Direction Direction { set; get; }
            public bool IsInteractable { set; get; }
        }

        // Path to XML data file.
        private const string XMLFileName = "Loader//map.xml";
        // Data structure where XML data is stored.
        private List<Map> _map = new List<Map>();
        public List<Map> lstMaps { get { return _map; } }
        /// <summary>
        /// Constructor. Loads from map.xml into a list of map objects.
        /// </summary>
        /// <remarks>
        /// Automatically loads data into data structure when constructed.
        /// </remarks>
        public XMLLoader()
        {
            try
            {
                XMLLoad();
            }
            catch (Exception exception)
            {
                DebugLogger.LogIt("$E XML Import error: " + exception.ToString());
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
        private static Direction intToDirection(int dir)
        {
            switch (dir)  //convert number in xml to direction enum
            {
                case 0:
                    return Direction.UP;
                case 1:
                    return Direction.RIGHT;
                case 2:
                    return Direction.DOWN;
                case 3:
                    return Direction.LEFT;
                default:
                    return Direction.UP;
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
            doc = XDocument.Load(XMLLoader.XMLFileName);
            uint mapID = 0;

            List<XElement> lstObjects = doc.Descendants("objects").ToList();

            List<UnitObject> lstUnitObjects = new List<UnitObject>();
            XElement objParent = doc.Descendants("objects").FirstOrDefault();
            foreach (XElement obj in objParent.Descendants("object"))
            {
                UnitObject unitObj = new UnitObject();
                unitObj.ID = obj.Attribute("id").Value;
                unitObj.ImagePath = obj.Descendants("imagePath").FirstOrDefault().Value;
                if (obj.Descendants("passable").FirstOrDefault() != null)
                {
                    unitObj.IsPassable = Convert.ToBoolean(obj.Descendants("passable").FirstOrDefault().Value);
                }
                else unitObj.IsPassable = false;

                if (obj.Descendants("direction").FirstOrDefault() != null)
                {
                    unitObj.Direction = intToDirection(Convert.ToInt32(obj.Descendants("direction").FirstOrDefault().Value));
                }
                else unitObj.Direction = intToDirection(0);

                if (obj.Descendants("interactable").FirstOrDefault() != null)
                {
                    unitObj.IsInteractable = Convert.ToBoolean(obj.Descendants("interactable").FirstOrDefault().Value);
                }
                else unitObj.IsInteractable = false;

                if (obj.Descendants("name").FirstOrDefault() != null)
                {
                    unitObj.Name = obj.Descendants("name").FirstOrDefault().Value;
                }
                else unitObj.Name = "";

                if (obj.Descendants("maxHits").FirstOrDefault() != null)
                {
                    unitObj.MaxHits = int.Parse(obj.Descendants("maxHits").FirstOrDefault().Value.ToString());
                }
                else unitObj.MaxHits = 0;

                if (obj.Descendants("attack").FirstOrDefault() != null)
                {
                    unitObj.Attack = int.Parse(obj.Descendants("attack").FirstOrDefault().Value.ToString());
                }
                else unitObj.Attack = 0;

                if (obj.Descendants("defense").FirstOrDefault() != null)
                {
                    unitObj.Defense = int.Parse(obj.Descendants("defense").FirstOrDefault().Value.ToString());
                }
                else unitObj.Defense = 0;


                lstUnitObjects.Add(unitObj);
            }

            // GET: Each level in the game.
            foreach (XElement map in doc.Descendants("map"))
            {
                uint rows = Convert.ToUInt32(map.Descendants("rows").FirstOrDefault().Value);
                uint columns = Convert.ToUInt32(map.Descendants("columns").FirstOrDefault().Value);
                string mapName = map.Descendants("name").FirstOrDefault().Value;
                _map.Add(new Map(rows, columns, mapID++, mapName));
                // Reference to the last added map.
                Map latestMap = _map[_map.Count - 1];
                // GET: Each point in the map.
                foreach (XElement point in map.Descendants("point").ToList())
                {
                    int xPosition = Convert.ToInt32(point.Descendants("xposition").FirstOrDefault().Value);
                    int yPosition = Convert.ToInt32(point.Descendants("yposition").FirstOrDefault().Value);
                    int direction = Convert.ToInt32(point.Descendants("direction").FirstOrDefault().Value);
                    string imagePath = point.Descendants("imagePath").FirstOrDefault().Value;
                    bool passable = Convert.ToBoolean(point.Descendants("passable").FirstOrDefault().Value);

                    // Initialize point.
                    latestMap.InitPoint(xPosition, yPosition, imagePath, intToDirection(direction), passable);

                    foreach (XElement child in point.Descendants("object").ToList())
                    {
                        foreach (UnitObject obj in lstUnitObjects)
                        {
                            if (obj.ID.ToUpper().Equals(child.Attribute("id").Value.ToString().ToUpper()))
                            {
                                //bbb all of below needs constants..
                                switch (obj.ID.ToUpper())
                                {
                                    case "UNIT":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Unit(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name, obj.IsPassable));
                                        break;
                                    case "BASEOBJECT":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.BaseObject(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name, obj.IsPassable, obj.IsInteractable));
                                        break;
                                    case "MOBILE":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Mobile(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name, obj.MaxHits, obj.Attack, obj.Defense));
                                        break;
                                    case "PLAYERCHAR":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.PlayerChar(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name, obj.MaxHits, obj.Attack, obj.Defense));
                                        break;
                                    case "CONTAINER":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Container(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name));
                                        break;
                                    case "ITEM":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Item(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name));
                                        break;
                                    case "CORPSE":
                                        latestMap.AddUnitToPoint(xPosition, yPosition, new RogueFeature.Backend.Units.Corpse(xPosition, yPosition, obj.ImagePath, obj.Direction, obj.Name));
                                        break;

                                }
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
