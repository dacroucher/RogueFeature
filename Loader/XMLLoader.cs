using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Loader
{
    public class XMLLoader
    {
        private string _XMLFileName;
        public XMLLoader(String XMLFileName, bool LoadYesNo)
        {
            _XMLFileName = XMLFileName;
            if (LoadYesNo)
                XMLLoad();
        }

        public XMLLoader()
        {
        }

        public bool setFileName(String XMLFileName, bool LoadYesNo)
        {
            _XMLFileName = XMLFileName;
            if (LoadYesNo)
            {
                try{
                    XMLLoad();
                }
                catch (Exception exception)
                {
                    Console.WriteLine("XML Import error: " + exception.ToString());
                    return false;
                }
            }
            return true;

        }
        
        public bool loadXMLFile()
        {
            try{
                    XMLLoad();
            }
            catch (Exception exception)
            {
                Console.WriteLine("XML Import error: " + exception.ToString());
                return false;
            }
            return true;
        }

        private void XMLLoad()
        {
            XDocument doc = new XDocument();
            doc = XDocument.Load(_XMLFileName);
            List <String> Contact = (from xml2 in doc.Descendants("author")
                                select xml2.Value).Distinct().ToList();
            foreach (String element in Contact)
            {
                Console.WriteLine(element);
            }

                
        }

 
    }
}
