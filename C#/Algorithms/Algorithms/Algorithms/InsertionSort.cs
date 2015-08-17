using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    class InsertionSort {
        List<int> data = new List<int>();

        public static List<int> Sort(List<int> data) {

            for (int i = 2; i <= data.Count-1; i++) {
                int key = data[i];
                for (int k = i; k > 1 && data[k] < data[k - 1]; k--) {
                    data[k - 1] += data[k];
                    data[k] = data[k - 1] - data[k];
                    data[k - 1] -= data[k];
                }
            }
            return data;
        }
    }
}