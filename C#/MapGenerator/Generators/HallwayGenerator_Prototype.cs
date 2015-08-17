using System;
using System.Collections.Generic;
using CodeSample.Utility;
using CodeSample.Dungeon;


/* Things I have to utilize
******************************************************************************************************************
IMPLEMENTATION
******************************************************************************************************************
Straight Hall
1. Choose a random coordinate from between the two rooms x/y (startingXY) overlap.
2. Get the length of the hallway by getting the difference between them
2. Randomly choose a width from 1 to clamped x/y overlap.
	2.1. If that randomly chosen width is odd, derive hallway amplitude by calculating width -1/2
    2.2. If Even, randomly select if the extra 1 is added to starting x/y + amplitude or is subtracted from start - amplitude
    
if(secondVertex != 0, 0)
	endingXY += amplitude * first vertexXY
for (i = startingXY; i <= endingX/y; i+= 1 * current direction vertex)
	set i to traversible space
	for(j = i; j <= i + amplitude; j++ )
    	set to traversible space
    for(k = i; k>= i - amplitude; k--)
    	set k to traversible space
	set i + amplitude + 1 to wall
    set i - amplitude - 1 to wall
if(secondVertex != 0, 0)
	set endingXY + 1 plus one tile in the opposite of second vertex to wall (this handles the corners)
	process second vertex
    
Right Angles
*/


public static class Class1 {
    private static Random RNG = new Random();
    private static List<List<int>> DungeonLayout;

    public static void MakeHallway(Room oldRoom, Room newRoom, Range possibleHallwayWidths, List<List<int>> dungeonLayout) {
        DungeonLayout = dungeonLayout;
        string straightHallwayDirection = IsStraightHallwayOnlyPossible(oldRoom, newRoom);
        int hallwayWidth = RNG.Next((int)possibleHallwayWidths.min, (int)possibleHallwayWidths.max + 1);

        // This vertex indicates the position of the old room as it relates to the old room.
        var relativeRoomPositions = new Vertex(oldRoom.center.x - newRoom.center.x, oldRoom.center.y - newRoom.center.y);

        // If so, randomly choose between right angle and straight.
        if ((straightHallwayDirection != null) && (RNG.Next(1, 5) <= 3)) {
            if (straightHallwayDirection == "x") {
                MakeHorizontalHallway(relativeRoomPositions, oldRoom, newRoom, hallwayWidth,true);
            }
            else {
                MakeVerticalHallway(relativeRoomPositions, oldRoom, newRoom, hallwayWidth, true);
            }
        }
        else {
            var initialVertex = new Vertex(relativeRoomPositions.x, 0);
            var secondVertex = new Vertex(0, relativeRoomPositions.y);
            // Choose which direction the right angle goes first.
            if (RNG.Next(0, 2) == 0)
                MakeRightAngleHallway(initialVertex, secondVertex, oldRoom, newRoom);
            else
                MakeRightAngleHallway(secondVertex, initialVertex, oldRoom, newRoom);
        }
    }

    private static Coord GetHorizontallyConstrainedCoord(Vertex relativeRoomPositions, Room oldRoom, Room newRoom) {
        Coord newRoomReferenceCorner;
        int yCoord = 0;
        if (relativeRoomPositions.x < 1)
            newRoomReferenceCorner = newRoom.topLeft;
        else
            newRoomReferenceCorner = newRoom.bottomRight;
        if (Range.IsInRange(oldRoom.topLeft.y+1, newRoom.topLeft.y+1, newRoom.bottomRight.y-1))
            yCoord = RNG.Next(oldRoom.topLeft.y+1, newRoom.bottomRight.y);
        else if (Range.IsInRange(oldRoom.bottomRight.y-1, newRoom.topLeft.y+1, newRoom.bottomRight.y-1))
            yCoord = RNG.Next(oldRoom.bottomRight.y-1, newRoom.topLeft.y+2);
        else
            yCoord = RNG.Next(newRoom.topLeft.y + 1, newRoom.bottomRight.y);
        return new Coord(newRoomReferenceCorner.x, yCoord);
    }

    private static Coord GetVerticallyConstrainedCoord(Vertex relativeRoomPositions, Room oldRoom, Room newRoom) {
        Coord newRoomReferenceCorner;
        int xCoord = 0;
        if (relativeRoomPositions.y < 1)
            newRoomReferenceCorner = newRoom.bottomRight;
        else
            newRoomReferenceCorner = newRoom.topLeft;
        if (Range.IsInRange(oldRoom.topLeft.x + 1, newRoom.topLeft.x + 1, newRoom.bottomRight.x - 1))
            xCoord = RNG.Next(oldRoom.topLeft.x + 1, newRoom.bottomRight.x);
        else if (Range.IsInRange(oldRoom.bottomRight.x - 1, newRoom.topLeft.x + 1, newRoom.bottomRight.x - 1))
            xCoord = RNG.Next(oldRoom.bottomRight.x - 1, newRoom.topLeft.x + 2);
        else
            xCoord = RNG.Next(newRoom.topLeft.x + 1, newRoom.bottomRight.x);
        return new Coord(xCoord, newRoomReferenceCorner.y);
    }

    private static Coord GetHorizontalCoord(Vertex relativeRoomPosition, Room room) {
        if (relativeRoomPosition.x < 1)
            return new Coord(room.topLeft.x, RNG.Next(room.topLeft.y+1, room.bottomRight.y));
        else
            return new Coord(room.bottomRight.x, RNG.Next(room.topLeft.y+1, room.bottomRight.y));
    }

    private static Coord GetVerticalCoord(Vertex relativeRoomPosition, Room room) {
        if (relativeRoomPosition.y < 1)
            return new Coord(room.topLeft.y, RNG.Next(room.topLeft.x + 1, room.bottomRight.x));
        else
            return new Coord(room.bottomRight.y, RNG.Next(room.topLeft.x + 1, room.bottomRight.x));
    }

    private static string IsStraightHallwayOnlyPossible(Room oldRoom, Room newRoom) {
        // Check to see if the rooms are horizontally situated to allow a vertical hallway.
        if (Range.IsInRange(oldRoom.topLeft.x, newRoom.topLeft.x, newRoom.bottomRight.x) ||
           Range.IsInRange(oldRoom.bottomRight.x, newRoom.topLeft.x, newRoom.bottomRight.x))
            return "y";

        // Check to see if the rooms are vertically situated to allow a horizontal hallway.
        if (Range.IsInRange(oldRoom.topLeft.y, newRoom.topLeft.y, newRoom.bottomRight.y) ||
           Range.IsInRange(oldRoom.bottomRight.y, newRoom.topLeft.y, newRoom.bottomRight.y))
            return "x";

        // Single pipe hallway is not possible.
        return null;
    }

/* 
if(secondVertex != 0, 0)
    endingXY += amplitude * first vertexXY
for (i = startingXY; i <= endingX/y; i+= 1 * current direction vertex)
    set i to traversible space
    for(j = i; j <= i + amplitude; j++ )
        set to traversible space
    for(k = i; k>= i - amplitude; k--)
        set k to traversible space
    set i + amplitude + 1 to wall
    set i - amplitude - 1 to wall
if(secondVertex != 0, 0)
    set endingXY + 1 plus one tile in the opposite of second vertex to wall (this handles the corners)
    process second vertex */
    private static void MakeHorizontalHallway(Vertex relativePositions, Room oldRoom, Room newRoom, int hallwayWidth, bool onlyHall=false) { 
        Coord startingCoord;
        Coord stoppingCoord;
        int northAmplitude = 0;
        int southAmplitude = 0;
        int hallwayAmplitude = (hallwayWidth - 1) / 2;
        bool amplitudeRemainder;

        if (hallwayWidth > 1){
            northAmplitude += hallwayAmplitude;
            southAmplitude += hallwayAmplitude;

            amplitudeRemainder = (hallwayWidth % 2 == 0);
            if (amplitudeRemainder){
                if (RNG.Next(0, 2) == 0)
                    northAmplitude += 1;
                else
                    southAmplitude +=1;
            }
        }
        
        if (onlyHall) {
            startingCoord = GetVerticallyConstrainedCoord(relativePositions, oldRoom, newRoom);
            if (relativePositions.x < 1)
                stoppingCoord = new Coord(oldRoom.bottomRight.x, startingCoord.y);
            else
                stoppingCoord = new Coord(oldRoom.topLeft.x, startingCoord.y);
        }
        else {
            startingCoord = GetHorizontalCoord(relativePositions, newRoom);
            stoppingCoord = GetHorizontalCoord(relativePositions, oldRoom);
            stoppingCoord.x += hallwayAmplitude;
        }

        for (int x = startingCoord.x; x <= stoppingCoord.x; x += 1 * relativePositions.x) {
            for (int y = startingCoord.y - northAmplitude; y <= startingCoord.y + southAmplitude; y++) {
                DungeonLayout[x][y] = 1;
            }
            if ((DungeonLayout[x][startingCoord.y + northAmplitude + 1] != 0) && (DungeonLayout[x][startingCoord.y + northAmplitude + 1] != 3))
                DungeonLayout[x][startingCoord.y + northAmplitude + 1] = 0;
            if ((DungeonLayout[x][startingCoord.y - southAmplitude - 1] != 0) && (DungeonLayout[x][startingCoord.y - southAmplitude - 1] != 3))
                DungeonLayout[x][startingCoord.y - southAmplitude - 1] = 0;   
        }
    }

    private static void MakeVerticalHallway(Vertex relativePositions, Room oldRoom, Room newRoom, int hallwayWidth, bool onlyHall=false) { 
        Coord startingCoord;
        Coord stoppingCoord;
        int eastAmplitude = 0;
        int westAmplitude = 0;
        int hallwayAmplitude = (hallwayWidth - 1) / 2;
        bool amplitudeRemainder;

        if (hallwayWidth > 1){
            eastAmplitude += hallwayAmplitude;
            westAmplitude += hallwayAmplitude;

            amplitudeRemainder = (hallwayWidth % 2 == 0);
            if (amplitudeRemainder){
                if (RNG.Next(0, 2) == 0)
                    eastAmplitude += 1;
                else
                    westAmplitude +=1;
            }
        }
        
        if (onlyHall) {
            startingCoord = GetHorizontallyConstrainedCoord(relativePositions, oldRoom, newRoom);
            if (relativePositions.y < 1)
                stoppingCoord = new Coord(oldRoom.topLeft.y, startingCoord.x);
            else
                stoppingCoord = new Coord(oldRoom.bottomRight.y, startingCoord.x);
        }
        else {
            startingCoord = GetVerticalCoord(relativePositions, newRoom);
            stoppingCoord = GetVerticalCoord(relativePositions, oldRoom);
            stoppingCoord.y += hallwayAmplitude;
        }

        for (int y = startingCoord.y; y <= stoppingCoord.y; y += 1 * relativePositions.y) {
            for (int x = startingCoord.x - westAmplitude; x <= startingCoord.x + eastAmplitude; x++) {
                DungeonLayout[x][y] = 1;
            }
            if ((DungeonLayout[startingCoord.x + eastAmplitude + 1][y] != 0) && (DungeonLayout[startingCoord.x + eastAmplitude + 1][y] != 3))
                DungeonLayout[startingCoord.x + eastAmplitude + 1][y] = 0;
            if ((DungeonLayout[startingCoord.x - westAmplitude - 1][y] != 0) && (DungeonLayout[startingCoord.x - westAmplitude - 1][y] != 3))
                DungeonLayout[startingCoord.x - westAmplitude - 1][y] = 0;
        }
    }

    private static void MakeRightAngleHallway(Vertex initialDirection, Vertex secondDirection, Room oldRoom, Room newRoom) { }
}