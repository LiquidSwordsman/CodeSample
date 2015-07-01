using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taken.Dungeon
{
    /// <summary>
    /// A room is a convenience class used in the MapGenerator.</summary>
    public sealed class Room
    {
        private int x1;
        private int y1;
        private int x2;
        private int y2;
        public Coord topLeft;
        public Coord bottomRight;
        
        /// <summary>
        /// The room's style, that will be used to pick tiles.</summary>
        public string style;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="x">The X coordinate of the rooms top-left corner.</param>
        /// <param name="y">The Y coordinate of the rooms top-left corner.</param>
        /// <param name="width">The rooms width.</param>
        /// <param name="height">The rooms height.</param>
        public Room(int x, int y, int width, int height)
        {
            this.x1 = x;
            this.y1 = y;
            this.x2 = x + width;
            this.y2 = y + height;
            this.topLeft = new Coord(x1, y1);
            this.bottomRight = new Coord(x2, y2);
        }

        /// <summary>
        /// Find the coordinates of this room's Center.</summary>
        /// <returns>A Coord of the rooms Center.</returns>
        public Coord Center()
        {
            int centerX = (x1 + x2) / 2;
            int centerY = (y1 + y2) / 2;
            Coord center = new Coord(centerX, centerY);
            return center;
        }
         
        /// <summary>
        /// Determines if this room intersects with another.</summary>
        /// <param name="other">The room you are testing for intersection with.</param>
        /// <returns>True or false.</returns>
        public bool Intersect(Room other)
        {
            return ((this.x1 <= other.bottomRight.x) && (this.x2 >= other.topLeft.x) &&
                    (this.y1 <= other.bottomRight.y) && (this.y2 >= other.topLeft.y));
        }
    }
}
