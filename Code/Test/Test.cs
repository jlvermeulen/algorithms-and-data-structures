using System;
using System.Collections.Generic;
using Utility;

class Test
{
    static void Main()
    {
        Random random = new Random();
        List<int> testList = new List<int>();
        for (int i = 0; i < 10000; i++)
            testList.Add(random.Next());
        int[] test = testList.ToArray();
        int[] test2 = testList.ToArray();
        DateTime start, end;

        start = DateTime.Now;
        Sort<int>.InsertionSort(test);
        end = DateTime.Now;
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", testList.Count, (end - start).TotalSeconds);

        start = DateTime.Now;
        Sort<int>.MergeSort(test2);
        end = DateTime.Now;
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", testList.Count, (end - start).TotalSeconds);

        //for (int i = 0; i < testList.Count; i++)
        //    Console.WriteLine(test[i]);
        Console.ReadLine();
    }
}