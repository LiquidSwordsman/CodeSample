using CodeSample.Utility;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeSample.Generators {
    class MapParser {
        private static int LastInitializedIn;
        private static string FilePath;
        private static JObject Data;

        private static void Initialize(int act) {
            if (act != LastInitializedIn) {
                FilePath = Path.Combine(Environment.CurrentDirectory, @"Data\\JSON\\");
                LastInitializedIn = act;
            }
        }

        private static JObject ReadDataFile(string data_file) {
            using (StreamReader file = File.OpenText(Path.Combine(FilePath, data_file)))
            using (JsonTextReader reader = new JsonTextReader(file)) {
                return (JObject)JToken.ReadFrom(reader);
            }
        }

        private static void PrepareToParse(int act, string data_file) {
            Initialize(act);
            Data = ReadDataFile(data_file);
        }

        public static Range GetPotentialRoomDimension(int act, string dimension) {
            PrepareToParse(act, "dungeon_values.json");
            return new Range(Data.SelectToken("rooms.dimensions." + dimension + "_range").ToObject<List<int>>());
        }

        public static Dictionary<Range, string> GetPotentialRoomStyles(int act) {
            var output = new Dictionary<Range, string>();
            PrepareToParse(act, "room_styles.json");
            foreach (var x in (JObject)Data) {
                var spawnChance = x.Value["spawn_chance"].ToObject<List<int>>();
                output.Add(new Range(spawnChance), x.Key);
            }
            return output;
        }

        public static Dictionary<Range, int> GetTilerDict(int act, string roomStyle) {
            var output = new Dictionary<Range, int>();
            PrepareToParse(act, "room_styles.json");
            var tileTypes = (JObject)Data[roomStyle]["tile_types"];
            JObject tilesData = ReadDataFile("tiles.json");

            foreach (var tileType in tileTypes) {
                var spawnChance = tileType.Value["spawn_chance"].ToObject<List<int>>();
                int mapInt = (int)tilesData[tileType.Key]["map_int"];
                output.Add(new Range(spawnChance), mapInt);
            }

            return output;
        }
    }
}
