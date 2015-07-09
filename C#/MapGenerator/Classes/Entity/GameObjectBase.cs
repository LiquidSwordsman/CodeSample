using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSample.Entities
{
    /// <summary>
    /// Every single thing in the game is derived from GameObjectBase.</summary>
    public abstract class GameObjectBase
    {
        /// <summary>
        /// A string indicating the filepath for an objects graphical representation.</summary>
        public string imageSource;

        /// <summary>
        /// Class Constructor.</summary>
        /// <param name="imageSource">A string indicating the filepath for an objects graphical 
        ///                           representation.</param>
        public GameObjectBase(string imageSource)
        {
            this.imageSource = imageSource;
        }
    }
}
