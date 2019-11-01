using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace MFXmlParser {
    class Program {
        static void Main(string[] args) {
            string unparsedFolder = Path.Combine(Directory.GetCurrentDirectory(),args[1]);
            string parsedFolder = Path.Combine(Directory.GetCurrentDirectory(),args[2]);
            float spotDetectionThreshold = float.Parse(args[3]);

            // create our lot
            ParkingLot lot = new ParkingLot(spotDetectionThreshold);

            // check if our supplied paths exist
            if (!Directory.Exists(unparsedFolder)) {
                Console.WriteLine("Error: unparsed path {0} is not a directory. exitting...", unparsedFolder);
                return;
            }
            if (!Directory.Exists(parsedFolder)) {
                Console.WriteLine("Error: parsed path {0} is not a directory. exitting...", parsedFolder);
                return;
            }

            // find every file in our supplied path
            string[] UnparsedFiles = GetUnparsedXMLFilenames(unparsedFolder);
            
            // for each unparsed file we found...
            foreach(string unparsedFilePath in UnparsedFiles) {

                // parse the XML to get a list of parking objects
                ParkingObject[] currentParkingObjects = ParseXML(unparsedFilePath);

                // and add each member to our parking lot
                foreach(ParkingObject p in currentParkingObjects) {
                    lot.AddParkingEvent(p);
                }

                // move the file to our parsed directory
                string unparsedFileName = Path.GetFileName(unparsedFilePath);
                Directory.Move(unparsedFilePath, Path.Combine(parsedFolder, unparsedFileName));
            }
        }
        
        /// <summary>
        /// gets all files in specified path
        /// </summary>
        /// <param name="startPath"></param>
        /// <returns></returns>
        static string[] GetUnparsedXMLFilenames(string startPath) {
            return Directory.GetFiles(startPath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static ParkingObject[] ParseXML(string filename) {

            // make our list to store parking objects in
            List<ParkingObject> parkings = new List<ParkingObject>();

            // open supplied xml document
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // get every node of the proper type
            XmlNodeList parkingEventsNode = doc.GetElementsByTagName("ParkingEvent");

            // for each individual node...
            foreach(XmlNode node in parkingEventsNode) {
                
                // get each attribute
                XmlAttributeCollection attributes = node.Attributes;

                // and parse it
                string type = attributes["type"].InnerText;
                int top = int.Parse(attributes["top"].InnerText);
                int left = int.Parse(attributes["left"].InnerText);
                int width = int.Parse(attributes["width"].InnerText);
                int height = int.Parse(attributes["height"].InnerText);

                // build a bounding box
                BoundingBox bb = new BoundingBox(top, left, width, height);

                // build the parking object
                ParkingObject obj = new ParkingObject(type,bb);
                
                // add it to our list
                parkings.Add(obj);
            }

            // return our final array
            return parkings.ToArray();
        }
    }
}
