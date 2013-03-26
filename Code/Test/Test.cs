using System;
using System.Collections.Generic;
using Utility;
using System.IO;
using System.Diagnostics;

class Test
{
    delegate int[] SortMethod(int[] input);

    static void Main()
    {
        //StreamWriter writer;
        //Random random = new Random();
        //List<int> list = new List<int>();
        //writer = new StreamWriter("VeryLarge.in");
        //int j = 100000000;
        //for (int i = 0; i < 100000000; i++)
        //{
        //    if (random.NextDouble() <= 0.8)
        //        j--;
        //    else
        //        j++;
        //    list.Add(j);
        //}

        //for(int i = 0; i < list.Count; i++)
        //    writer.WriteLine(list[i]);
        //writer.Close();

        //list.Sort();

        //writer = new StreamWriter("VeryLarge.out");
        //for (int i = 0; i < list.Count; i++)
        //    writer.WriteLine(list[i]);
        //writer.Close();
        //return;
        //Console.BufferHeight = 11000;
        //Console.BufferWidth = 100;

        string testSize = "Large";
        string testDir = "Random\\";
        string test = testDir + testSize;

        UnzipTestData(test, testDir);
        int[] input = LoadTestData(test);

        ReferenceTest(input, test, true);

        //RunTest(input, test, true, new SortMethod(Sort<int>.BinaryInsertionSort), "Binary Insertion Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.BubbleSort), "Bubble Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CocktailSort), "Cocktail Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CombSort), "Comb Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CombInsertionSort), "Comb-Insertion Sort");
        //RunTest(input, test, false, new SortMethod(Sort<int>.CycleSort), "Cycle Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.GnomeSort), "Gnome Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.Heapsort), "Heapsort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.InsertionSort), "Insertion Sort");
        //RunTest(input, test, false, new SortMethod(Sort<int>.JSort), "JSort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.MergeSort), "Merge Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.OddEvenSort), "Odd-Even Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.PatienceSort), "Patience Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.Quicksort), "Quicksort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.SelectionSort), "Selection Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.StrandSort), "Strand Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.ShellSort), "Shell Sort");
        //RunTest(input, test, false, new SortMethod(Sort<int>.Timsort), "Timsort");
        //RunTest(input, test, false, new SortMethod(Sort<int>.TreeSort), "Tree Sort");

        File.Delete("TestData\\" + test + ".in");
        File.Delete("TestData\\" + test + ".out");

        Console.ReadLine();
    }

    static void RunTest(int[] input, string test, bool benchmark, SortMethod Sort, string sortName)
    {
        int[] data = new int[input.Length];
        input.CopyTo(data, 0);
        Console.WriteLine("Using {0}.", sortName);

        DateTime start = DateTime.Now;
        data = Sort(data);
        DateTime end = DateTime.Now;

        Console.WriteLine("Sorted {0} numbers in {1} seconds.", data.Length, (end - start).TotalSeconds);
        if (!benchmark)
            Console.WriteLine("Output is {0}.", CheckOutput(data, test) ? "correct" : "incorrect");

        //for (int i = 0; i < data.Length; i++)
        //    Console.WriteLine(i + ": " + data[i]);

        Console.WriteLine();
    }

    static void ReferenceTest(int[] input, string test, bool benchmark)
    {
        Console.WriteLine("Using C# Sort.");
        List<int> list = new List<int>(input);

        DateTime start = DateTime.Now;
        list.Sort();
        DateTime end = DateTime.Now;

        input = list.ToArray();
        Console.WriteLine("Sorted {0} numbers in {1} seconds.", input.Length, (end - start).TotalSeconds);
        if (!benchmark)
            Console.WriteLine("Output is {0}.", !benchmark && CheckOutput(input, test) ? "correct" : "incorrect");
        Console.WriteLine();
    }

    static int[] LoadTestData(string test)
    {
        StreamReader reader = new StreamReader("TestData\\" + test + ".in");
        List<int> list = new List<int>();
        string line;
        while ((line = reader.ReadLine()) != null)
            list.Add(int.Parse(line));
        reader.Close();

        return list.ToArray();
    }

    static void UnzipTestData(string test, string testDir)
    {
        Process p = new Process();
        p.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\7za.exe";
        p.StartInfo.Arguments = "x " + "TestData\\" + test + ".7z -aoa -o" + "TestData\\" + testDir;
        p.StartInfo.CreateNoWindow = true;
        p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
        p.Start();
        p.WaitForExit();
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