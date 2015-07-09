using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeSample.Generators.MapGenerator;
using CodeSample.Dungeon;

namespace CodeSample
{
    /// <summary>
    /// This class holds various functions to aid in the testing and debugging code.</summary>
    public static class Debug

    {
        /// <summary>
        /// Used to indent ConsoleOutput. Exceptionally useful when outputing nested 
        /// collections.</summary>
        /// <param name="level">The number of tabs to indent by. Defaults to one.</param>
        /// <returns>The string containing the Indent.</returns>
        public static string Indent(int level = 1)
        {
            string indent = "";
            for (int i = 0; i <= level; i++)
                indent += "    ";
            return indent;
        }

        /// <summary>
        /// Prints a 2D list in a human-readable grid format.</summary>
        /// <param name="list">The list to be printed.</param>
        /// <returns>A string containing the human readable grid.</returns>
        public static string Output2DList(List<List<int>> list)
        {
            System.Text.StringBuilder array = new System.Text.StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                foreach (List<int> column in list)
                {
                    array.Append(column[i].ToString());
                }
                array.Append("\r\n");
            }
            return array.ToString();
        }

        /// <summary>
        /// Writes a given string to a text file on your desktop.</summary>
        /// <param name="toPrint">The string to be written.</param>
        /// <param name="docName">The desired name of your text file.</param>
        public static void PrintToDesktopFile(string toPrint, string docName)
        {
            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            using (StreamWriter outfile = new StreamWriter(mydocpath + @"\" + docName + ".txt"))
                outfile.Write(toPrint.ToString());
        }

        public static void MapGeneratorTest(int act){
            var sw = new Stopwatch();
            sw.Start();
            MapGenerator.GenerateNewFloor(act, true);
            sw.Stop();
            Console.WriteLine("Map was generated successfully and has been written to desktop.");
            Console.WriteLine("Map generation took {0} milliseconds.", sw.ElapsedMilliseconds);
            Console.ReadLine();
        }
    }
}
