using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taken.Dungeon;

namespace Taken
{
    /// <summary>
    /// Global Data is where we store save specific constants. At some point these will be moved 
    /// into a game default values XML and loaded in on a per game basis to allow modification of 
    /// the games initial state.</summary>
    public static class GlobalData
    {
        // Until this comment disappears, chunkHeight and chunkWidth should both be the same value.
        /// <summary>
        /// Controls the the height (in tiles) of a chunk.</summary>
        public static readonly int chunkHeight = 3;

        /// <summary>
        /// Controls the width (in tiles) of a chunk.</summary>
        public static readonly int chunkWidth = 3;
    }
}
