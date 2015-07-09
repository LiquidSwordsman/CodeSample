using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeSample.Generators.Utility;

namespace CodeSample.Data
{
    class MapParser
    {
        private static int LastInitializedIn;
        private static string FilePath;
        private static JObject Data;

        private static void Initialize(int act){
            if (act != LastInitializedIn)
            {
                FilePath = Path.Combine(Environment.CurrentDirectory, @"Data\\Act" + act.ToString() + "\\Dungeon\\");
                LastInitializedIn = act;
            }
        }

        private static JObject ReadDataFile(string data_file)
        {
            using (StreamReader file = File.OpenText(Path.Combine(FilePath, data_file)))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                return (JObject)JToken.ReadFrom(reader);
            }
        }

        private static void PrepareToParse(int act, string data_file)
        {
            Initialize(act);
            Data = ReadDataFile(data_file);
        }

        public static Dictionary<Range, string> GetPotentialRoomStyles(int act)
        {
            var output = new Dictionary<Range, string>();
            PrepareToParse(act, "room_styles.json");
            foreach (var x in Data)
            {
                output.Add(new Range((int)x.Value["spawn_chance"]["min"], (int)x.Value["spawn_chance"]["max"]), x.Key);
            }
            return output;
        }

        public static Dictionary<Range, int> GetTilerDict(int act, string roomStyle)
        {
            var output = new Dictionary<Range, int>();
            PrepareToParse(act, "room_styles.json");
            JObject style = (JObject)Data.SelectToken(roomStyle + ".tile_types");
            foreach(var tileType in style.Properties())
            {       
                var spawnChance = new Range((int)tileType.Value["spawn_chance"]["min"], (int)tileType.Value["spawn_chance"]["max"]);
                int mapInt = (int)ReadDataFile("tiles.json").SelectToken(tileType.Name + ".map_int");
                output.Add(spawnChance, mapInt);
            }
            return output;
        }
    }
}
