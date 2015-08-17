using CodeSample.XMLParsers;
using System;
using System.Collections.Generic;

namespace CodeSample
{
    class Program
    {
        public static string PrintList(List<int> List) {
            string output = "[ ";
            foreach (int num in List) {
                output += num.ToString() + ", ";
            }
            return output += "]";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1 / 2 = " + (1 / 2).ToString());
            List<int> list1 = new List<int>() { 1, 2, 3 };
            Console.WriteLine("List1: " + PrintList(list1));
            List<int> list2 = new List<int>() { 3, 4, 5 };
            Console.WriteLine("List2: " + PrintList(list2));
            List<int> list3 = new List<int>();
            list3.AddRange(list1);
            list3.AddRange(list2);
            Console.WriteLine("List1+2:" + PrintList(list3));
            MapGenParser.Intialize(1);
            Debug.MapGeneratorTest(1);
        }
    }
}