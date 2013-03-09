using System;
using System.Collections.Generic;
using Utility;
using System.IO;

class Test
{
    static void Main()
    {
        //Random random = new Random();
        //StreamWriter writer = new StreamWriter("TestSparseMedium.out");
        ////for (int i = 0; i < 10000; i++)
        ////    writer.WriteLine(random.Next(100));
        //writer.Close();

        //List<int> sort = new List<int>(LoadTestData("TestSparseMedium"));
        //sort.Sort();
        //sort.Reverse();

        //writer = new StreamWriter("TestSparseMediumReverseSorted.in");
        //for (int i = 0; i < sort.Count; i++)
        //    writer.WriteLine(sort[i]);
        //writer.Close();
        //return;

        string test = "TestSparseLargeSorted";
        int[] data = LoadTestData(test);
        DateTime start, end;

        Console.WriteLine("Using MergeSort.");
        start = DateTime.Now;
        data = Sort<int>.MergeSort(data);
        end = DateTime.Now;
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");

        data = LoadTestData(test);
        Console.WriteLine("Using HeapSort.");
        start = DateTime.Now;
        data = Sort<int>.HeapSort(data);
        end = DateTime.Now;
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");

        data = LoadTestData(test);
        Console.WriteLine("Using InsertionSort.");
        start = DateTime.Now;
        data = Sort<int>.InsertionSort(data);
        end = DateTime.Now;
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");

        //for (int i = 0; i < testList.Count; i++)
        //    Console.WriteLine(test[i]);
        Console.ReadLine();
    }

    static int[] LoadTestData(string test)
    {
        StreamReader reader = new StreamReader("TestData/" + test + ".in");
        List<int> list = new List<int>();
        string line;
        while ((line = reader.ReadLine()) != null)
            list.Add(int.Parse(line));
        reader.Close();

        return list.ToArray();
    }

    static bool CheckOutput(int[] output, string test)
    {
        StreamReader reader = new StreamReader("TestData/" + test + ".out");
        List<int> list = new List<int>();
        string line;
        while ((line = reader.ReadLine()) != null)
            list.Add(int.Parse(line));
        reader.Close();

        if (output.Length != list.Count)
            return false;

        for (int i = 0; i < output.Length; i++)
            if (output[i] != list[i])
                return false;

        return true;
    }
}