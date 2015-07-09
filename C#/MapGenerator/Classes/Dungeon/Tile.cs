using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSample.Entities;

namespace CodeSample.Dungeon
{
    /// <summary>
    /// A class that represents a tile.</summary>
    public class Tile: ImpedingFactor
    {
        /// <summary>
        /// Class constructor.</summary>
        /// <param name="imageSource">The filepath to the tiles graphical representation.</param>
        /// <param name="blocksSight">Indicates if the tile blocks sight.</param>
        /// <param name="blocksMovement">Indicates if the tile can be occupied.</param>
        public Tile(string imageSource, bool blocksSight, bool blocksMovement): 
            base(imageSource, blocksSight, blocksMovement)
        { }
    }
}