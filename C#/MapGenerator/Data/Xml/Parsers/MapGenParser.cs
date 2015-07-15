using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using CodeSample.Dungeon;
using CodeSample.Utility;

namespace CodeSample.XMLParsers
{
    /// <summary>
    /// MapGenParser reads the XML file containing the values for the current act and provides the
    /// values to the MapGenerator.</summary>
    public static class MapGenParser
    {
        private static XmlDocument doc = new XmlDocument();
        private static XmlSchema schema;
        private static string filePath = Path.Combine(Environment.CurrentDirectory, @"Data\\Xml\");
        
        /// <summary>
        /// Loads the XmlDocument from the filepath into doc and validates it.</summary>
        public static void Intialize(int act)
        {
            using (var fs = File.OpenRead(Path.Combine(filePath, "Schemas\\MapGenerator.xsd")))
            {
                schema = XmlSchema.Read(fs, null);
            }
            doc.Schemas.Add(schema);
            doc.Load(filePath + "Data\\Act" + act.ToString() + "_MapGenValues.xml");
            doc.Validate(null);
        }

        /// <summary>
        /// Retrieves the min-max map dimensions from the XML file.</summary>
        /// <returns>A tuple containing the domain and range of the maps possible 
        ///         dimensions.</returns>
        public static Tuple<Range, Range> GetMapDimensions()
        {
            XmlNode dungeonValues = doc.SelectSingleNode("//mapGenValues/dungeonValues");
            int minimumMapWidth = XmlConvert.ToInt32(dungeonValues["minMapWidth"].InnerText);
            int maximumMapWidth = XmlConvert.ToInt32(dungeonValues["maxMapWidth"].InnerText);
            int minimumMapHeight = XmlConvert.ToInt32(dungeonValues["minMapHeight"].InnerText);
            int maximumMapHeight = XmlConvert.ToInt32(dungeonValues["maxMapHeight"].InnerText);
            Range width = new Range(minimumMapWidth, maximumMapWidth);
            Range height = new Range(minimumMapHeight, maximumMapHeight);
            Tuple<Range, Range> mapDimensions = new Tuple<Range, Range>(width, height);
            return mapDimensions;
        }

        /// <summary>
        /// Gets the range for the number of rooms.</summary>
        /// <returns>Returns the possible range of the number of rooms.</returns>
        public static Range GetRangeOfNumberOfRooms()
        {
            XmlNode dungeonValues = doc.SelectSingleNode("//mapGenValues/dungeonValues");
            int minimumRooms = XmlConvert.ToInt32(dungeonValues["minNumberOfRooms"].InnerText);
            int maximumRooms = XmlConvert.ToInt32(dungeonValues["maxNumberOfRooms"].InnerText);
            Range roomRange = new Range(minimumRooms, maximumRooms);
            return roomRange;
        }

        /// <summary>
        /// Gets the range for room sizes.</summary>
        /// <returns>A tuple containing the domain and range of a rooms possible 
        ///          dimensions.</returns>
        public static Tuple<Range, Range> GetRoomSizeRange()
        {
            XmlNode dungeonValues = doc.SelectSingleNode("//mapGenValues/dungeonValues");
            int minimumRoomWidth = XmlConvert.ToInt32(dungeonValues["minRoomWidth"].InnerText);
            int maximumRoomWidth = XmlConvert.ToInt32(dungeonValues["maxRoomWidth"].InnerText);
            int minimumRoomHeight = XmlConvert.ToInt32(dungeonValues["minRoomHeight"].InnerText);
            int maximumRoomHeight = XmlConvert.ToInt32(dungeonValues["maxRoomHeight"].InnerText);
            Range width = new Range(minimumRoomWidth, maximumRoomWidth);
            Range height = new Range(minimumRoomHeight, maximumRoomHeight);
            Tuple<Range, Range> roomDimensions = new Tuple<Range, Range>(width, height);
            return roomDimensions;
        }

        /// <summary>
        /// Gets a dictionary of room styles and their spawn chance.</summary>
        /// <returns>Style Dictionary {Range; string}</returns>
        public static Dictionary<Range, string> GetStylesDict()
        {
            XmlNode styles = doc.SelectSingleNode("//mapGenValues/RoomStyles");
            Dictionary<Range, string> styleSpawnDict = new Dictionary<Range, string>();

            foreach (XmlNode node in styles.ChildNodes)
            {
                // Get values and add the style to the styleSpawnDict.
                int styleMinChance = XmlConvert.ToInt32(node["minChance"].InnerText);
                int styleMaxChance = XmlConvert.ToInt32(node["maxChance"].InnerText);
                Range styleSpawnChance = new Range(styleMinChance, styleMaxChance);
                string styleName = node.Attributes["name"].Value;
                styleSpawnDict.Add(styleSpawnChance, styleName);
            }
            return styleSpawnDict;
        }

        /// <summary>
        /// Returns a dict of the given RoomStyles possible tile types.</summary>
        /// <param name="roomStyle">The style of the room whose tiles we are getting.</param>
        /// <returns>The dictionary of information about the contained tiles.</returns>
        public static Dictionary<Range, int> GetTilerDict(string roomStyle)
        {
            XmlNode tilesNode = doc.SelectSingleNode(
                String.Format("//mapGenValues/RoomStyles/style[@name='{0}']/tileTypes", 
                roomStyle));
            Dictionary<Range, int> tilerDict = new Dictionary<Range, int>();
            foreach (XmlNode node in tilesNode.ChildNodes)
	        {
                int tileMinChance = XmlConvert.ToInt32(node.Attributes["mapInt"].Value);
                int tileMaxChance = XmlConvert.ToInt32(node["maxChance"].InnerText);
                int mapInt = XmlConvert.ToInt32(node["minChance"].InnerText);
                Range tileSpawnChance = new Range(tileMinChance, tileMaxChance);
                tilerDict.Add(tileSpawnChance, mapInt);
    	    }
            return tilerDict;
        }
        
        /// <summary>
        /// Gets a dictionary of tiles.</summary>
        /// <returns>A dictionary containing every tile object in the act {mapInt, Tile}</returns>
        public static Dictionary<int, Tile> GetTileObjectDict()
        {
            XmlNodeList tileTypes = doc.SelectSingleNode("//mapGenValues/tileTypes").ChildNodes;
            Dictionary<int, Tile> tileDict = new Dictionary<int, Tile>();
            foreach (XmlNode tileType in tileTypes)
            {
                bool blocksSight = XmlConvert.ToBoolean(tileType["blocksSight"].InnerText);
                bool blocksMovement = XmlConvert.ToBoolean(tileType["blocksMovement"].InnerText);
                string filePath = Path.Combine(Environment.CurrentDirectory, @tileType["filePath"].InnerText);
                int mapInt = XmlConvert.ToInt32(tileType.Attributes["mapInt"].Value);
                Tile tile = new Tile(filePath, blocksSight, blocksMovement);
                tileDict.Add(mapInt, tile);
            }
            return tileDict;
        }
    }
}