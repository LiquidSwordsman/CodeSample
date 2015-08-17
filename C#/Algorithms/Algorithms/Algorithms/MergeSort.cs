using System;
using System.Collections.Generic;
using Algorithms;

class MergeSort {
    // Domain
    // Write a recursive Merge sort algorithm to arrange a random list of integers in ascending order.
    // The merge sort works by breaking a list in half repeatedly until each list contains only 1 item(and is therefore sorted).
    // Then the lists are compared via a < b comparison to create an output. This is done recursively until the list is returned as a single sorted entity.
    // 1. I need to recursively break lists in half if their length is > 1.
    // 2. I need a function to put these lists in the right order.
    // 3. I need a main entry function

    public static void CompareListMembers(List<int> data, int start, int mid, int end) {
        var SortedList = new List<int>();
        // Three loops
        //Loop 1 compare both halves of the list simultaneously

        int i = start;
        int j = mid + 1;
        while (i <= mid && j <= end) {
            if (data[i] < data[j]) {
                SortedList.Add(data[i]);
                i += 1;
            }
            else {
                SortedList.Add(data[j]);
                j += 1;
            }
        }
        while (i <= mid) {
            SortedList.Add(data[i]);
            i += 1;
            }
        while (j <= end) {
            SortedList.Add(data[j]);
            j += 1;
        }

        int k = 0;
        i = start;
        while (k < SortedList.Count && i <= end) {
            data[i] = SortedList[k];
            i += 1;
            k += 1;
        }
    }

    public static void ProcessMergeSort(List<int> data, int start, int end) {
        if (start < end) {
            int mid = (start + end) / 2;
            ProcessMergeSort(data, start, mid);
            ProcessMergeSort(data, mid+1, end);
            CompareListMembers(data, start, mid, end);
        }
    }

    public static void StartMergeSort() {
        var data = new List<int>() { 6, 2, 4, 3, 1, 5, 9, 11, 8, 7 };
        Console.WriteLine("The randomized list is: " + Utilities.PrintList(data));
        ProcessMergeSort(data, 0, data.Count - 1);
        Console.WriteLine("The ordered list is: " + Utilities.PrintList(data));
        Console.ReadKey();
    }

    public static void StartMergeSort(List<int> data, bool output=false) {
        if (output) {
            Console.WriteLine("The randomized list is: " + Utilities.PrintList(data));
        }
        ProcessMergeSort(data, 0, data.Count - 1);
        if (output) {
            Console.WriteLine("The ordered list is: " + Utilities.PrintList(data));
            Console.ReadKey();
        }
    }
}