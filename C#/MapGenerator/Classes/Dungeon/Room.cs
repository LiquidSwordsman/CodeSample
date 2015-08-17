using System;
namespace CodeSample.Dungeon {
    /// <summary>
    /// A room is a convenience class used in the MapGenerator.</summary>
    public sealed class Room {
        public Coord topLeft, bottomRight, center;

        /// <summary>
        /// The room's style, that will be used to pick tiles.</summary>
        public string style;

        /// <summary>
        /// Class constructor.</summary>
        /// <param name="x">The X coordinate of the rooms top-left corner.</param>
        /// <param name="y">The Y coordinate of the rooms top-left corner.</param>
        /// <param name="width">The rooms width.</param>
        /// <param name="height">The rooms height.</param>
        public Room(int x, int y, int width, int height) {
            this.topLeft = new Coord(x, y);
            this.bottomRight = new Coord(x + width, y + height);
            this.center = new Coord((this.topLeft.x + this.bottomRight.x) / 2, (this.topLeft.y + this.bottomRight.y) / 2);
        }

        /// <summary>
        /// Determines if this room intersects with another.</summary>
        /// <param name="other">The room you are testing for intersection with.</param>
        /// <returns>True or false.</returns>
        public bool Intersect(Room other) {
            return ((this.topLeft.x <= other.bottomRight.x) && (this.bottomRight.x >= other.topLeft.x) &&
                    (this.topLeft.y <= other.bottomRight.y) && (this.bottomRight.y >= other.topLeft.y));
        }

        public override string ToString() {
            return "Room:\n" + Debug.Indent() + "Top Left Corner: " + topLeft.ToString() + "\n" +
                   Debug.Indent() + "Bottom Right Corner: " + bottomRight.ToString() + "\n" +
                   Debug.Indent() + "Center: " + center.ToString();
        }
    }
}