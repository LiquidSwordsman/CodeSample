using CodeSample.Dungeon;
using CodeSample.Utility;
using CodeSample.XMLParsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeSample.Generators {
    /// <summary>
    /// A collection of all the functions that combine to create a dungeon layout.</summary>
    public static class MapGenerator {
        private static int Act;
        private static Random RNG = new Random();
        private static List<List<int>> DungeonLayout;

        /// <summary>
        /// Generates a new dungeon.</summary>
        /// <param name="act">The act of the game the floor being made belongs to.</param>
        /// <returns>Returns a Map object containing the new dungeon and its relevant data.</returns>
        public static Map GenerateNewFloor(int act, bool testing = false) {

            // Set class level variables.
            Act = act;
            DungeonLayout = GenerateNullDungeon();

            Room firstRoom = null;
            Room lastRoom = null;

            // Variables to be used in the loop.
            Range rangeOfPossibleRooms = MapGenParser.GetRangeOfNumberOfRooms();
            List<Room> generatedRooms = new List<Room>();
            int loopCounter = 0;
            int numberOfCreatedRooms = 0;
            int numberOfRoomCreationAttempts = 0;
            while (!(rangeOfPossibleRooms.IsInRange(numberOfCreatedRooms)) &&
                (numberOfRoomCreationAttempts <= rangeOfPossibleRooms.max)) {
                Room newRoom = RoomBuilder(generatedRooms);
                if (newRoom == null) {
                    numberOfRoomCreationAttempts += 1;
                    continue;
                }
                else {
                    if (firstRoom == null) {
                        firstRoom = newRoom;
                        generatedRooms.Add(newRoom);
                        numberOfCreatedRooms += 1;
                    }
                    else {
                        HallwayGenerator.MakeHallways(DungeonLayout, generatedRooms[numberOfCreatedRooms - 1], newRoom);
                        // Finally, append the new room to the list, and setup for the next room.
                        generatedRooms.Add(newRoom);
                        numberOfCreatedRooms += 1;
                    }
                }
                if (numberOfRoomCreationAttempts == rangeOfPossibleRooms.max)
                    lastRoom = newRoom;
                numberOfRoomCreationAttempts++;
                loopCounter++;
            }
            Tuple<List<List<int>>, Dictionary<int, Chunk>> chunkingInfo = CreateChunkArray();
            List<List<int>> chunkArray = chunkingInfo.Item1;
            Dictionary<int, Chunk> chunkDict = chunkingInfo.Item2;
            var newMap = new Map(Act, firstRoom, lastRoom, chunkArray, chunkDict);
            if (testing)
                Debug.PrintToDesktopFile(Debug.Output2DList(DungeonLayout), "dungeon");
            return newMap;
        }


        /// <summary>
        /// Creates the 2D array to be used for the DungeonLayout.</summary>
        /// <returns>Returns an empty 2D int array.</returns>
        private static List<List<int>> GenerateNullDungeon() {
            // Fetches the map dimensions from the Parser and creates an appropriately sized 
            // jagged array.
            Tuple<Range, Range> mapDimensions = MapGenParser.GetMapDimensions();

            //Set mapWidth and mapHeight to a random number between their min and max.
            int mapWidth = -1;
            int mapHeight = -1;
            while (mapWidth % GlobalData.chunkWidth != 0)
                mapWidth = RNG.Next((int)mapDimensions.Item1.min,
                                    (int)mapDimensions.Item1.max);
            while (mapHeight % GlobalData.chunkHeight != 0)
                mapHeight = RNG.Next((int)mapDimensions.Item2.min,
                                     (int)mapDimensions.Item2.max);

            // Generate the appropriately sized 2d List.
            var dungeon = new List<List<int>>();
            for (int dungeonWidth = 0; dungeonWidth < mapWidth; dungeonWidth++) {
                List<int> column = new List<int>();
                for (int dungeonHeight = 0; dungeonHeight < mapHeight; dungeonHeight++)
                    column.Add(3);
                dungeon.Add(column);
            }
            return dungeon;
        }


        /// <summary>
        /// Creates and furnishes a randomly generated room.</summary>
        /// <returns>Returns a finished room.</returns>
        private static Room RoomBuilder(List<Room> generatedRooms, bool allowIntersections = false) {
            // Choose a random roomWidth and height based on values from the parser.
            Range potentialRoomWidth = MapParser.GetPotentialRoomDimension(Act, "width");
            Range potentialRoomHeight = MapParser.GetPotentialRoomDimension(Act, "height");

            int roomWidth = RNG.Next((int)potentialRoomWidth.min, (int)potentialRoomWidth.max + 1);
            int roomHeight = RNG.Next((int)potentialRoomHeight.min, (int)potentialRoomHeight.max + 1);

            // Choose a random position inside the maps boundaries and create the room there.
            var mapDimensions = new Range(DungeonLayout.Count - 1, DungeonLayout[0].Count - 1);
            int roomPosX = RNG.Next(0, (int)mapDimensions.min - roomWidth + 1);
            int roomPosY = RNG.Next(0, (int)mapDimensions.max - roomHeight + 1);
            var newRoom = new Room(roomPosX, roomPosY, roomWidth, roomHeight);

            // Run through the other generatedRooms and see if they Intersect with this one
            if (!allowIntersections) {
                foreach (Room otherRoom in generatedRooms)
                    // If this room intersects, discard it.
                    if (newRoom.Intersect(otherRoom))
                        return null;
            }
            RoomStyler(newRoom);
            RoomTiler(newRoom);
            RoomFurnisher(newRoom);
            return newRoom;
        }

        /// <summary>
        /// Selects a style for a room.
        /// </summary>
        /// <param name="room">The room to be styled.</param>
        private static void RoomStyler(Room room) {
            //Chooses a room style by comparing a random float to a styles dict.
            int styleSelection = RNG.Next(0, 100);
            Dictionary<Range, string> roomStyles = MapParser.GetPotentialRoomStyles(Act);

            foreach (KeyValuePair<Range, string> kvp in roomStyles)
                if (kvp.Key.IsInRange(styleSelection)) {
                    room.style = kvp.Value;
                    return;
                }
        }


        /// <summary>
        /// Updates all tiles in a room to match it's style.</summary>
        /// <param name="room">The room to be retiled.</param>
        private static void RoomTiler(Room room) {
            // For each tile in the room randomly select a tile from the list for the room style 
            // and apply it.
            Dictionary<Range, int> tileDict = MapParser.GetTilerDict(Act, room.style);
            Coord topLeft = room.topLeft;
            Coord bottomRight = room.bottomRight;

            for (int x = topLeft.x + 1; x <= bottomRight.x - 1; x++)
                for (int y = topLeft.y + 1; y <= bottomRight.y - 1; y++) {
                    int tileSelection = RNG.Next(1, 100);
                    foreach (KeyValuePair<Range, int> kvp in tileDict)
                        if (kvp.Key.IsInRange(tileSelection))
                            DungeonLayout[x][y] = kvp.Value;
                }
            // Place the north and south walls of the room.
            for (int x = topLeft.x; x <= bottomRight.x; x++) {
                DungeonLayout[x][topLeft.y] = 0;
                DungeonLayout[x][bottomRight.y] = 0;
            }

            // Place the eastAmplitude and west walls of the room.
            for (int y = topLeft.y; y <= Math.Abs(bottomRight.y); y++) {
                DungeonLayout[topLeft.x][y] = 0;
                DungeonLayout[bottomRight.x][y] = 0;
            }
        }


        /// <summary>
        /// Places appropriate furniture into a room based on its style.</summary>
        /// <param name="room">The room to be furnished.</param>
        private static void RoomFurnisher(Room room) {
            /* TODO: Implement RoomFurnisher.
               Based on the generatedRooms style and that styles rule set creates and decorates a 
               room with objects. Calls parser to generate the room styles decoration dict and
               based on the rule set for the room style add objects and update tiles 
            */
        }


        /// <summary>
        /// Compacts the (chunkWidth * n) * (chunkHeight * n) DungeonLayout into an n*n array and 
        /// creates the associated data structures.</summary>
        /// <returns>Returns a tuple containing 2D list populated by ints, and a dictionary 
        ///          containing KVPs of ints from the 2D list and Chunk objects.</returns>
        private static Tuple<List<List<int>>, Dictionary<int, Chunk>> CreateChunkArray() {
            //Return values.
            var chunkDict = new Dictionary<int, Chunk>();
            var chunkArray = new List<List<int>>();

            // Setting up loop variables.
            int chunkKey = 0;
            int columnIndex = 0;
            int chunkWidth = GlobalData.chunkWidth;
            int chunkHeight = GlobalData.chunkHeight;

            // Set the size of the chunk array (also the loop controllers).
            int chunkArrayWidth = DungeonLayout.Count() / GlobalData.chunkWidth;
            int chunkArrayHeight = DungeonLayout[0].Count() / GlobalData.chunkHeight;

            //This loop controls the columns.
            while (columnIndex < chunkArrayWidth) {
                var chunkArrayColumn = new List<int>();
                int rowIndex = 0;

                // This loop builds each row index in each column.
                while (rowIndex < chunkArrayWidth) {
                    var tilesInChunk = new List<List<int>>();

                    // These loops put the values from the DungeonLayout[outerWhile][innerWhile]
                    // into a chunkWidth*chunkHeight 2D List to be stored in a chunk.
                    for (int chunkColumn = columnIndex * chunkWidth; chunkColumn <
                        (columnIndex * chunkWidth) + chunkWidth; chunkColumn++) {
                        List<int> chunkColumnData = new List<int>();
                        for (int chunkRow = rowIndex * chunkHeight; chunkRow <
                            (rowIndex * chunkHeight) + chunkHeight; chunkRow++) {
                            chunkColumnData.Add(DungeonLayout[chunkColumn][chunkRow]);
                        }
                        tilesInChunk.Add(chunkColumnData);
                    }

                    // Create the chunk, store it in the chunk dict, and update loop controls.
                    var chunk = new Chunk(chunkWidth, chunkHeight, tilesInChunk);
                    chunkDict.Add(chunkKey, chunk);
                    chunkArrayColumn.Add(chunkKey);
                    rowIndex++;
                    chunkKey++;
                }

                // Add the column to the chunk array and update column iterator.\
                chunkArray.Add(chunkArrayColumn);
                columnIndex++;
            }
            var output = new Tuple<List<List<int>>, Dictionary<int, Chunk>>(chunkArray, chunkDict);
            return output;
        }
    }
}