using CodeSample.XMLParsers;
using System;

namespace CodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 / 2 = " + (1 / 2).ToString());
            MapGenParser.Intialize(1);
            Debug.MapGeneratorTest(1);
        }
    }
}