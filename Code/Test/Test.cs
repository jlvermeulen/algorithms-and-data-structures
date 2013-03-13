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
        //writer = new StreamWriter("VeryLarge.in");
        //for (int i = 0; i < 100000000; i++)
        //    writer.WriteLine(random.Next(100));
        //writer.Close();
        //return;

        //List<int> sort = new List<int>(LoadTestData("Sparse/Random/VeryLarge"));
        //sort.Sort();
        //sort.Reverse();

        //writer = new StreamWriter("VeryLarge.in");
        //for (int i = 0; i < sort.Count; i++)
        //    writer.WriteLine(sort[i]);
        //writer.Close();
        //return;

        string testSize = "Large";
        string testDir = "Uniform\\Random\\";
        string test = testDir + testSize;

        UnzipTestData(test, testDir);

        ReferenceTest(test);

        //RunTest(test, new SortMethod(Sort<int>.BubbleSort), "Bubble Sort");
        //RunTest(test, new SortMethod(Sort<int>.CocktailSort), "Cocktail Sort");
        //RunTest(test, new SortMethod(Sort<int>.CombSort), "Comb Sort");
        //RunTest(test, new SortMethod(Sort<int>.CombInsertionSort), "Comb-Insertion Sort");
        //RunTest(test, new SortMethod(Sort<int>.GnomeSort), "Gnome Sort");
        RunTest(test, new SortMethod(Sort<int>.Heapsort), "Heapsort");
        //RunTest(test, new SortMethod(Sort<int>.InsertionSort), "Insertion Sort");
        RunTest(test, new SortMethod(Sort<int>.MergeSort), "Merge Sort");
        //RunTest(test, new SortMethod(Sort<int>.OddEvenSort), "Odd-Even Sort");
        RunTest(test, new SortMethod(Sort<int>.Quicksort), "Quicksort");
        //RunTest(test, new SortMethod(Sort<int>.SelectionSort), "Selection Sort");
        //RunTest(test, new SortMethod(Sort<int>.ShellSort), "Shell Sort");
        RunTest(test, new SortMethod(Sort<int>.TreeSort), "Tree Sort");

        File.Delete("TestData\\" + test + ".in");
        File.Delete("TestData\\" + test + ".out");

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