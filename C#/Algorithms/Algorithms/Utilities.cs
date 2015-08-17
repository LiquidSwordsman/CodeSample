using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public static class Utilities
    {
        public static List<int> GenerateIntDataSet(int dataSetSize)
        {
            var dataSet = new List<int>();
            var rng = new Random();
            for (int i = 0; i < dataSetSize; i++)
                dataSet.Add(i);
            dataSet.Shuffle();
            return dataSet;
        }

        public static string PrintList(List<int> data)
        {
            var output = new StringBuilder("[");
            for (int i = 0; i < data.Count - 1; i++)
                output.Append(data[i].ToString() + ", ");
            return output.ToString() + data[data.Count - 1].ToString() + "]";
        }

        // Use like so: ExecuteAndMeasureTimeTaken(() => FunctionToTime(), "Merge-Sort");
        public static string ExecuteAndMeasureTimeTaken(Func<Type> action, string timedProcessName)
        {
            if (action == null) throw new ArgumentNullException();
            else
            {
                var sw = new Stopwatch();
                sw.Start();

                action.DynamicInvoke();

                sw.Stop();
                return timedProcessName + " took " + sw.ElapsedMilliseconds.ToString() + " milliseconds to complete.";
            }
        }

        public static void Swap<T>(List<T> data, int a, int b)
        {
            T temp = data[a];
            data[a] = data[b];
            data[b] = temp;
        }

        private static Random rng = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}