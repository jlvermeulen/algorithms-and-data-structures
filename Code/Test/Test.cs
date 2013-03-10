using System;
using System.Collections.Generic;
using Utility;
using System.IO;

class Test
{
    delegate int[] SortMethod(int[] input);

    static void Main()
    {
        ////Random random = new Random();
        //StreamWriter writer = new StreamWriter("TestUniformSmall.in");
        ////for (int i = 0; i < 100; i++)
        ////    writer.WriteLine(random.Next());
        //writer.Close();
        ////return;

        //List<int> sort = new List<int>(LoadTestData("TestSparseSmall"));
        //sort.Sort();
        //sort.Reverse();

        //writer = new StreamWriter("TestSparseSmallReverseSorted.in");
        //for (int i = 0; i < sort.Count; i++)
        //    writer.WriteLine(sort[i]);
        //writer.Close();
        //return;

        string test = "Sparse/Random/Large";

        ReferenceTest(test);

        RunTest(test, new SortMethod(Sort<int>.MergeSort), "MergeSort");
        RunTest(test, new SortMethod(Sort<int>.HeapSort), "HeapSort");
        //RunTest(test, new SortMethod(Sort<int>.InsertionSort), "InsertionSort");
        //RunTest(test, new SortMethod(Sort<int>.BubbleSort), "BubbleSort");
        //RunTest(test, new SortMethod(Sort<int>.SelectionSort), "SelectionSort");
        //RunTest(test, new SortMethod(Sort<int>.CocktailSort), "CocktailSort");
        RunTest(test, new SortMethod(Sort<int>.CombSort), "CombSort");
        RunTest(test, new SortMethod(Sort<int>.ShellSort), "ShellSort");

        //for (int i = 0; i < testList.Count; i++)
        //    Console.WriteLine(test[i]);
        Console.ReadLine();
    }

    static void RunTest(string test, SortMethod Sort, string sortName)
    {
        int[] data = LoadTestData(test);
        Console.WriteLine("Using {0}.", sortName);

        DateTime start = DateTime.Now;
        data = Sort(data);
        DateTime end = DateTime.Now;

        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");
        Console.WriteLine();
    }

    static void ReferenceTest(string test)
    {
        int[] data = LoadTestData(test);
        Console.WriteLine("Using C# Sort.");
        List<int> list = new List<int>(data);

        DateTime start = DateTime.Now;
        list.Sort();
        DateTime end = DateTime.Now;

        data = list.ToArray();
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");
        Console.WriteLine();
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