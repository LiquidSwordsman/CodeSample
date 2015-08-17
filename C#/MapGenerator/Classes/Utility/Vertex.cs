using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeSample.Utility
{
    struct Vertex
    {
        public int x, y;

        public Vertex(int xDir, int yDir)
        {
            x = UtilityMethods.Clamp<int>(xDir, -1, 1);
            y = UtilityMethods.Clamp<int>(xDir, -1, 1);
        }

    }
}
