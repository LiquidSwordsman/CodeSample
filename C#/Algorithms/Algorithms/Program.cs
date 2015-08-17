using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithms
{
    class Program {
        static void Main(string[] args) {
            int dataSetSize = 10000;
            List<int> data = Utilities.GenerateIntDataSet(dataSetSize);
//            Console.WriteLine("The unsorted dataset is:\n{0}", Utilities.PrintList(data));
//            Console.ReadKey();
            var sw = new Stopwatch();
            sw.Start();
            var qs = new Quicksort();
            qs.ProcessQuicksort(data, 0, data.Count-1);
            sw.Stop();
            Console.WriteLine("Quicksort took {0} milliseconds to process a dataset of {1} items.", sw.ElapsedMilliseconds.ToString(), dataSetSize);
            Console.ReadKey();
//            Console.WriteLine("The sorted dataset is:\n{0}", Utilities.PrintList(data));
//            Console.ReadKey();
        }

        static void TestMergesort(List<int> data) {
            var sw = new Stopwatch();
            sw.Start();
            MergeSort.StartMergeSort(data);
            sw.Stop();
            Console.WriteLine("Merge-Sort took {0} milliseconds to process a dataset of {1} items.", sw.ElapsedMilliseconds.ToString(), data.Count.ToString());
        }
    }
}
