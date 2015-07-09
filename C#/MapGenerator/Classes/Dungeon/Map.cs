using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.Dungeon
{
    /// <summary>
    /// A map object stores all of the relevant information about the player's current dungeon 
    /// floor.</summary>
    public sealed class Map
    {
        /// <summary>
        /// A two dimensional list of sequentially numbered integers representing the chunks 
        /// holding the dungeon layout.</summary>
        public List<List<int>> chunkArray;
        
        /// <summary>
        /// The act this map was generated for. Useful for spawning the appropriate enemies should 
        /// the player backtrack between acts.</summary>
        public int act;

        /// <summary>
        /// The first room that was generated on this floor. Used for the intial placement of the 
        /// player on the floor, and the placement of the stairs to the previous level.</summary>
        public Room firstRoom;

        /// <summary>
        /// The last room generated in this map. Used to place the stairs downward to the next 
        /// floor, and if necessary the boss.</summary>
        public Room lastRoom;

        /// <summary>
        /// A dictionary of the chunk objects used to store the dungeon layout.</summary>
        public Dictionary<int, Chunk> chunkDictionary;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="chunkArray">A two-dimensional list of the ints that serve as lookup keys 
        ///                          for the chunks that store the map.</param>
        /// <param name="act">The act of the game this map was generated in.</param>
        /// <param name="firstRoom">The first room generated in this map. Used for the players 
        ///                         initial X/Y placement on this floor.</param>
        /// <param name="lastRoom">The last room generated in this map. Used to place the stairs 
        ///                        down to a new floor.</param>
        /// <param name="chunkDictionary">A dictionary that stores the chunks that make up the map. 
        ///                               Keys are the ints found in the map</param>
        public Map(int act, Room firstRoom, Room lastRoom, List<List<int>> chunkArray, 
                   Dictionary<int, Chunk> chunkDictionary)
        {
            this.chunkArray = chunkArray;
            this.act = act;
            this.firstRoom = firstRoom;
            this.lastRoom = lastRoom;
            this.chunkDictionary = chunkDictionary;
        }
    }
}