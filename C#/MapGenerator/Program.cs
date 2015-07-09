using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSample.Generators.MapGenerator;
using CodeSample.Dungeon;
using CodeSample.XMLParsers;

namespace CodeSample
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