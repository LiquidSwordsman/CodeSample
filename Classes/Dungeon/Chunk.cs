using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taken.Dungeon
{
    /// <summary>
    /// A chunk represents a 3x3 portion of the dungeon map.</summary>
    public sealed class Chunk
    {
        /// <summary>
        /// A 2D list of ints indicating what tiletypes are stored here.</summary>
        public List<List<int>> tiles;

        /// <summary>
        /// A 2D list of ints indicating what groundObjects (if any) are stored here.</summary>
        public List<List<int>> groundObjects;
        
        /// <summary>
        /// A 2D list of ints indicating what evironmental decorations are stored here.</summary>
        public List<List<int>> decorations;

        /// <summary>
        /// An int indicating the horizontal number of tiles this chunk represents.</summary>
        public int width;

        /// <summary>
        /// An int indicating the vertical number of tiles this chunk represents.</summary>
        public int height;

        /// <summary>
        /// A bool indicating if the player has seen this chunk yet.</summary>
        public bool explored;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="width">An int indicating the horizontal number of tiles this chunk 
        ///                     represents.</param>
        /// <param name="height">An int indicating the vertical number of tiles this chunk 
        ///                      represents.</param>
        /// <param name="tiles">A 2D list of ints that indicate the tiles in the chunk.</param>
        /// <param name="explored">A boolean indicating if the  player has seen this chunk.</param>
        /// <param name="groundObjects">A 2D list of ints indicating what groundObjects (if any) 
        ///                             are stored here.</param>
        /// <param name="decorations">A 2D list of ints indicating what evironmental decorations 
        ///                           are stored here.</param>
        public Chunk(int width, int height, List<List<int>> tiles, bool explored = false, 
                     List<List<int>> groundObjects=null, List<List<int>> decorations=null)
        {
            this.tiles = tiles;
            this.groundObjects = groundObjects;
            this.decorations = decorations;
            this.width = width;
            this.height = height;
        }
    }
}