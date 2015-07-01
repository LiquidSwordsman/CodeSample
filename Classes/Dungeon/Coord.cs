using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taken.Dungeon
{
    /// <summary>
    /// Represents an X, Y pair.</summary>
    public class Coord
    {
        public int x;
        public int y;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Determines if a coordinate refers to the same location as this one.</summary>
        /// <param name="other">The location being checked.</param>
        /// <returns>True or false.</returns>
        public bool Equals(Coord other)
        {
            if ((other.x == x) && (other.y == y))
                return true;
            return false;
        }

        /// <summary>
        /// Checks to see if this coordinate falls within a given area.</summary>
        /// <param name="area">The area being checked.</param>
        /// <returns>True or false.</returns>
        public bool IsInArea(List<Coord> area)
        {
            foreach (Coord coordinate in area)
                if ((coordinate.x == x) && (coordinate.y == y))
                    return true;
            return false;
        }
    }
}
