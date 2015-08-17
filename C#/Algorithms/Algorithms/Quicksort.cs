using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Quicksort
    {
        Random rng = new Random();

        public void ProcessQuicksort(List<int> data, int start, int end)
        {
            if (end - start < 1) return;
            int lowerIndex = start;
            int upperIndex = end;
            int pivotIndex = rng.Next(end-start);
            int pivotData = data[rng.Next(start, end)];
            while (lowerIndex < upperIndex)
            {       
                while (data[lowerIndex] <= pivotData && lowerIndex <= upperIndex)
                {
                    lowerIndex += 1;
                }
                while (data[upperIndex] >= pivotData && lowerIndex <= upperIndex)
                {
                    upperIndex -= 1;
                }
            }
            Utilities.Swap(data, lowerIndex, upperIndex);
            ProcessQuicksort(data, start, pivotIndex - 1);
            ProcessQuicksort(data, pivotIndex + 1, end);
        }
    }
}