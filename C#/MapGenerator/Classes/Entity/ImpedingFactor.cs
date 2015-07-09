using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.Entities
{
    /// <summary>
    /// Gives it's children the ability to block sight and movement. EntityBase and terrain are 
    /// children of ImpedingFactor.</summary>
    public abstract class ImpedingFactor: GameObjectBase
    {
        /// <summary>
        /// Indicates whether or not a ImpedingFactor blocks an entity's line of sight.</summary>
        public bool blocksSight;

        /// <summary>
        /// Indicates whether or not a DungeonObject can be passed through.</summary>
        public bool blocksMovement;


        /// <summary>
        /// Class Constructor</summary>
        /// <param name="imageSource">A string indicating the filepath for an objects graphical 
        ///                           representation.</param>
        /// <param name="blocksSight">A bool indicating whethter or not a ImpedingFactor blocks an 
        ///                           entity's line of sight.</param>
        /// <param name="blocksMovement">A bool indicating whether or not a DungeonObject can be 
        ///                              passed through.</param>
        public ImpedingFactor(string imageSource, bool blocksSight, bool blocksMovement): 
               base(imageSource)
        {
            this.blocksSight = blocksSight;
            this.blocksMovement = blocksMovement;
        }
    }
}