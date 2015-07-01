using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taken.Generators.MapGenerator;
using Taken.Dungeon;
using Taken.XMLParsers;

namespace Taken
{
    class Program
    {
        static void Main(string[] args)
        {
            MapGenParser.Intialize(1);
            Debug.MapGeneratorTest(1);
        }
    }
}