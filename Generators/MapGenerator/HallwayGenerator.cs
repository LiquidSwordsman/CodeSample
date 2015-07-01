using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taken.Dungeon;
using Taken.Generators.Utility;

namespace Taken.Generators.MapGenerator
{
    class HallwayGenerator
    {
        private static List<List<int>> DungeonLayout;
        private static Random rng = new Random();

        public static void MakeHallways(List<List<int>> dungeonLayout, Room previousRoom, 
                                        Room newRoom)
        {
            DungeonLayout = dungeonLayout;
            if (CheckForAndMakePipeHallway(previousRoom, newRoom)){
                return;
            }
            MakeRightAngleHallways(previousRoom, newRoom);
        }

        private static bool CheckForAndMakePipeHallway(Room previousRoom, Room newRoom)
        {
            Dictionary<string, Room> horizontallySortedRooms = SortRoomsByAxis("x", previousRoom, 
                                                                               newRoom);
            Dictionary<string, Room> verticallySortedRooms = SortRoomsByAxis("y", previousRoom, 
                                                                             newRoom);
            // Check for and create a vertically oriented tunnel.
            if (horizontallySortedRooms["farthest"].topLeft.x <= 
                horizontallySortedRooms["closest"].bottomRight.x - 2){
                
                int x = horizontallySortedRooms["farthest"].topLeft.x + 1;
                int startY = verticallySortedRooms["closest"].bottomRight.y;
                int stopY = verticallySortedRooms["farthest"].bottomRight.y;
                CreateVerticalTunnel(startY, stopY, x);
                return true;
            }

            // Check for and create a horizontally oriented tunnel.
            if (verticallySortedRooms["farthest"].topLeft.y <= 
                verticallySortedRooms["closest"].bottomRight.y - 2){
                
                int y = verticallySortedRooms["farthest"].topLeft.y + 1;
                int startX = horizontallySortedRooms["closest"].bottomRight.x;
                int stopX = horizontallySortedRooms["farthest"].topLeft.x;
                CreateHorizontalTunnel(startX, stopX, y);
                return true;
            }
            return false;
        }

        private static void MakeRightAngleHallways(Room previousRoom, Room newRoom)
        {
            Dictionary<string, Room> horizontallySortedRooms = SortRoomsByAxis("x", previousRoom, 
                                                                               newRoom);
            Dictionary<string, Room> verticallySortedRooms = SortRoomsByAxis("y", previousRoom, 
                                                                             newRoom);
            // Flip a coin to decide hallway tunneling pattern.
            if (rng.Next(0, 2) == 0){
                //First move horizontally, then vertically
                int horizontalStartX  = horizontallySortedRooms["closest"].bottomRight.x;
                int horizontalStopX = horizontallySortedRooms["farthest"].Center().x + 1;
                int horizontalY = horizontallySortedRooms["closest"].Center().y;
                CreateHorizontalTunnel(horizontalStartX, horizontalStopX, horizontalY);

                int verticalStartY;
                int verticalStopY;
                string sideToCap;
                if(horizontallySortedRooms["closest"].Center().y <= verticallySortedRooms["closest"].Center().y){
                    verticalStartY = horizontalY + 1;
                    verticalStopY = verticallySortedRooms["farthest"].topLeft.y;
                    sideToCap = "top";
                }
                else{
                    verticalStartY = verticallySortedRooms["closest"].bottomRight.y;
                    verticalStopY = horizontalY - 1;
                    sideToCap = "bottom";
                }
                CreateVerticalTunnel(verticalStartY, verticalStopY, horizontalStopX, true, sideToCap);
            }
            else{
                // First move vertically, then horizontally
                int verticalStartY = verticallySortedRooms["closest"].bottomRight.y;
                int verticalStopY = verticallySortedRooms["farthest"].Center().y + 1;
                int verticalX = verticallySortedRooms["closest"].Center().x;
                CreateVerticalTunnel(verticalStartY, verticalStopY, verticalX);

                int horizontalStartX;
                int horizontalStopX;
                string sideToCap;
                if (verticallySortedRooms["closest"].Center().x <= horizontallySortedRooms["closest"].Center().x){
                    horizontalStartX = verticalX + 1;
                    horizontalStopX = horizontallySortedRooms["farthest"].topLeft.x;
                    sideToCap = "left";
                }
                else
                {
                    horizontalStartX = horizontallySortedRooms["closest"].bottomRight.x;
                    horizontalStopX = verticalX - 1;
                    sideToCap = "right";
                }
                CreateHorizontalTunnel(horizontalStartX, horizontalStopX, verticalStopY, true, sideToCap);
            }
        }

        /// <summary>
        /// Creates a horizontal tunnel between two points.</summary>
        /// <param name="startX">The X coordinate of the first point.</param>
        /// <param name="stopX">The X coordinate of the second point.</param>
        /// <param name="y">The Y coordinate the tunnel will be made along.</param>
        /// <param name="secondHall">Indicates if this is the second hall in a right angle.</param>
        /// <param name="sideToCap">Indicates which end of this hall needs walls placed.</param>
        private static void CreateHorizontalTunnel(int startX, int stopX, int y, 
                                                   bool secondHall=false, string sideToCap="")
        {
            //For each tile between min and max, change their int representation to 0 (empty tile).
            for (int i = startX; i <= stopX; i++){
                if ((y > 0) && (DungeonLayout[i][y - 1] != 1))
                    DungeonLayout[i][y - 1] = 0;
                DungeonLayout[i][y] = 1;
                if ((y < DungeonLayout.Count) && (DungeonLayout[i][y + 1] != 1))
                    DungeonLayout[i][y + 1] = 0;
            }
            if (secondHall){
                int x;
                if (sideToCap == "left")
                    x = startX - 2;
                else
                    x = stopX + 2;
                FixCorners(x, y, "horizontal", sideToCap);
            }
        }

        /// <summary>
        /// Creates a vertical tunnel between two points.</summary>
        /// <param name="startY">The Y coordinate of the first point.</param>
        /// <param name="stopY">The Y coordinate of the second point.</param>
        /// <param name="x">The X coordinate the tunnel will be made along.</param>
        /// <param name="secondHall">Indicates if this is the second hall in a right angle.</param>
        /// <param name="sideToCap">Indicates which end of this hall needs walls placed.</param>
        private static void CreateVerticalTunnel(int startY, int stopY, int x, 
                                                 bool secondHall=false, string sideToCap="")
        {
            //For each tile between min and max, change their int representation to 0.
            for (int i = startY; i <= stopY; i++){
                if ((x > 0) && (DungeonLayout[x - 1][i] != 1))
                    DungeonLayout[x - 1][i] = 0;
                DungeonLayout[x][i] = 1;
                if ((x < DungeonLayout[0].Count) && (DungeonLayout[x + 1][i] != 1))
                    DungeonLayout[x + 1][i] = 0;
            }
            if (secondHall){
                int y;
                if (sideToCap == "top")
                    y = startY - 2;
                else
                    y = stopY + 2;
                FixCorners(x, y, "vertical", "top");
            }
        }

        private static void FixCorners(int x, int y, string vertOrHor, string sideToFix)
        {
            List<Coord> cornerTiles = new List<Coord>();
            if (vertOrHor == "vertical") {
                if (sideToFix == "top")
                    cornerTiles.Add(new Coord(x + 1, y + 1));
                else
                    cornerTiles.Add(new Coord(x + 1, y - 1));
                cornerTiles.Add(new Coord(x - 1, y));
                cornerTiles.Add(new Coord(x, y));
                cornerTiles.Add(new Coord(x + 1, y));
            }
            else {
                if (sideToFix == "left")
                    cornerTiles.Add(new Coord(x + 1, y + 1));
                else
                    cornerTiles.Add(new Coord(x - 1, y + 1));
                cornerTiles.Add(new Coord(x, y - 1));
                cornerTiles.Add(new Coord(x, y));
                cornerTiles.Add(new Coord(x, y + 1));
            }
            foreach (Coord coord in cornerTiles)
                if (DungeonLayout[coord.x][coord.y] != 1)
                    DungeonLayout[coord.x][coord.y] = 0;
        }

        private static Dictionary<string, Room> SortRoomsByAxis(string axis, Room room1, 
                                                                Room room2)
        {
            int r1XY;
            int r2XY;
            Room closest;
            Room farthest;
            if (axis == "x"){
                r1XY = room1.Center().x;
                r2XY = room2.Center().x;
            }
            else{
                r1XY = room1.Center().y;
                r2XY = room2.Center().y;
            }

            if (r1XY < r2XY){
                closest = room1;
                farthest = room2;
            }
            else{
                closest = room2;
                farthest = room1;
            }
            return new Dictionary<string, Room>() { 
                {"closest", closest},
                {"farthest", farthest}
            };
        }
    }
}
