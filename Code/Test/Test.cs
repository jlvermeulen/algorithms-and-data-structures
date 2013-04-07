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

        //string testSize = "Medium";
        //string testDir = "Random\\";
        //string test = testDir + testSize;

        //UnzipTestData(test, testDir);
        //int[] input = LoadTestData(test);
        
        //ReferenceTest(input, test, true);

        //RunTest(input, test, true, new SortMethod(Sort<int>.BinaryInsertionSort), "Binary Insertion Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.BubbleSort), "Bubble Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CocktailSort), "Cocktail Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CombSort), "Comb Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CombInsertionSort), "Comb-Insertion Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.CycleSort), "Cycle Sort");
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
        //RunTest(input, test, true, new SortMethod(Sort<int>.Timsort), "Timsort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.TreeSort), "Tree Sort");

        //File.Delete("TestData\\" + test + ".in");
        //File.Delete("TestData\\" + test + ".out");

        DateTime start, end;
        WeightGraph wGraph = new WeightGraph();
        FlowGraph fGraph = new FlowGraph();
        List<IFlowGraphEdge> cut;
        List<IWeightedGraphEdge> mst;

        start = DateTime.Now;
        Console.WriteLine(Graph.MaxFlow(fGraph, 0, 15));
        end = DateTime.Now;
        Console.WriteLine("Calculated max flow in {0} seconds.", (end - start).TotalSeconds);

        start = DateTime.Now;
        cut = Graph.MinCut(fGraph, 0, 15);
        end = DateTime.Now;
        foreach (IFlowGraphEdge e in cut)
            Console.WriteLine(e.From + " " + e.To);
        Console.WriteLine("Calculated min cut in {0} seconds.", (end - start).TotalSeconds);

        Console.WriteLine();
        start = DateTime.Now;
        mst = Graph.MinimumSpanningTree(wGraph);
        end = DateTime.Now;
        foreach (IWeightedGraphEdge e in mst)
            Console.WriteLine(((char)e.From).ToString() + ((char)e.To).ToString());
        Console.WriteLine("Calculated MST in {0} seconds.", (end - start).TotalSeconds);

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

    class FlowGraph : IGraph<IFlowGraphEdge>
    {
        public FlowGraph()
        {
            FlowNode n0, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15;
            FlowEdge e01, e02, e03, e04, e15, e25, e26, e38, e313, e48, e410, e57, e67, e79, e810, e813, e911, e912, e913, e1015, e1114, e1214, e1215, e1315;

            e01 = new FlowEdge(0, 1, 90);
            e02 = new FlowEdge(0, 2, 160);
            e03 = new FlowEdge(0, 3, 320);
            e04 = new FlowEdge(0, 4, 45);
            e15 = new FlowEdge(1, 5, 30);
            e25 = new FlowEdge(2, 5, 20);
            e26 = new FlowEdge(2, 6, 260);
            e38 = new FlowEdge(3, 8, 100);
            e313 = new FlowEdge(3, 13, 240);
            e48 = new FlowEdge(4, 8, 50);
            e410 = new FlowEdge(4, 10, 180);
            e57 = new FlowEdge(5, 7, 135);
            e67 = new FlowEdge(6, 7, 55);
            e79 = new FlowEdge(7, 9, 250);
            e810 = new FlowEdge(8, 10, 20);
            e813 = new FlowEdge(8, 13, 360);
            e911 = new FlowEdge(9, 11, 5);
            e912 = new FlowEdge(9, 12, 30);
            e913 = new FlowEdge(9, 13, 110);
            e1015 = new FlowEdge(10, 15, 175);
            e1114 = new FlowEdge(11, 14, 10);
            e1214 = new FlowEdge(12, 14, 25);
            e1215 = new FlowEdge(12, 15, 140);
            e1315 = new FlowEdge(13, 15, 60);

            n0 = new FlowNode(0, new FlowEdge[] { e01, e02, e03, e04 });
            n1 = new FlowNode(1, new FlowEdge[] { e15 });
            n2 = new FlowNode(2, new FlowEdge[] { e25, e26 });
            n3 = new FlowNode(3, new FlowEdge[] { e38, e313 });
            n4 = new FlowNode(4, new FlowEdge[] { e48, e410 });
            n5 = new FlowNode(5, new FlowEdge[] { e57 });
            n6 = new FlowNode(6, new FlowEdge[] { e67 });
            n7 = new FlowNode(7, new FlowEdge[] { e79 });
            n8 = new FlowNode(8, new FlowEdge[] { e810, e813 });
            n9 = new FlowNode(9, new FlowEdge[] { e911, e912, e913 });
            n10 = new FlowNode(10, new FlowEdge[] { e1015 });
            n11 = new FlowNode(11, new FlowEdge[] { e1114 });
            n12 = new FlowNode(12, new FlowEdge[] { e1214, e1215 });
            n13 = new FlowNode(13, new FlowEdge[] { e1315 });
            n14 = new FlowNode(14, new FlowEdge[] { });
            n15 = new FlowNode(15, new FlowEdge[] { });

            this.Nodes = new FlowNode[] { n0, n1, n2, n3, n4, n5, n6, n7, n8, n9, n10, n11, n12, n13, n14, n15 };
            this.Edges = new FlowEdge[] { e01, e02, e03, e04, e15, e25, e26, e38, e313, e48, e410, e57, e67, e79, e810, e813, e911, e912, e913, e1015, e1114, e1214, e1215, e1315 };
        }

        public IEnumerable<IGraphNode<IFlowGraphEdge>> Nodes { get; private set; }
        public IEnumerable<IFlowGraphEdge> Edges { get; private set; }
    }

    class FlowNode : IGraphNode<IFlowGraphEdge>
    {
        public FlowNode(uint id, IEnumerable<IFlowGraphEdge> neighbours)
        {
            this.ID = id;
            this.Neighbours = neighbours;
        }

        public uint ID { get; set; }
        public IEnumerable<IFlowGraphEdge> Neighbours { get; set; }
    }

    class FlowEdge : IFlowGraphEdge
    {
        public FlowEdge(uint from, uint to, uint capacity)
        {
            this.From = from;
            this.To = to;
            this.Capacity = capacity;
        }

        public uint From { get; private set; }
        public uint To { get; private set; }
        public uint Capacity { get; private set; }
    }

    class WeightGraph : IGraph<IWeightedGraphEdge>
    {
        public WeightGraph()
        {
            WeightNode a, b, c, d, e, f, g, h, i;
            WeightEdge ab, ah, bc, bh, cd, cf, ci, de, df, ef, fg, gh, gi, hi;

            ab = new WeightEdge((int)'A', (int)'B', 4);
            ah = new WeightEdge((int)'A', (int)'H', 8);
            bc = new WeightEdge((int)'B', (int)'C', 8);
            bh = new WeightEdge((int)'B', (int)'H', 11);
            cd = new WeightEdge((int)'C', (int)'D', 7);
            cf = new WeightEdge((int)'C', (int)'F', 4);
            ci = new WeightEdge((int)'C', (int)'I', 2);
            de = new WeightEdge((int)'D', (int)'E', 9);
            df = new WeightEdge((int)'D', (int)'F', 14);
            ef = new WeightEdge((int)'E', (int)'F', 10);
            fg = new WeightEdge((int)'F', (int)'G', 2);
            gh = new WeightEdge((int)'G', (int)'H', 1);
            gi = new WeightEdge((int)'G', (int)'I', 6);
            hi = new WeightEdge((int)'H', (int)'I', 7);

            a = new WeightNode((int)'A', new WeightEdge[] { ab, ah });
            b = new WeightNode((int)'B', new WeightEdge[] { bc, bh });
            c = new WeightNode((int)'C', new WeightEdge[] { cd, cf, ci });
            d = new WeightNode((int)'D', new WeightEdge[] { de, df });
            e = new WeightNode((int)'E', new WeightEdge[] { ef });
            f = new WeightNode((int)'F', new WeightEdge[] { fg });
            g = new WeightNode((int)'G', new WeightEdge[] { gh, gi });
            h = new WeightNode((int)'H', new WeightEdge[] { hi });
            i = new WeightNode((int)'I', new WeightEdge[] { });

            this.Nodes = new WeightNode[] { a, b, c, d, e, f, g, h, i };
            this.Edges = new WeightEdge[] { ab, ah, bc, bh, cd, cf, ci, de, df, ef, fg, gh, gi, hi };
        }

        public IEnumerable<IGraphNode<IWeightedGraphEdge>> Nodes { get; private set; }
        public IEnumerable<IWeightedGraphEdge> Edges { get; private set; }
    }

    class WeightNode : IGraphNode<IWeightedGraphEdge>
    {
        public WeightNode(uint id, IEnumerable<IWeightedGraphEdge> neighbours)
        {
            this.ID = id;
            this.Neighbours = neighbours;
        }

        public uint ID { get; set; }
        public IEnumerable<IWeightedGraphEdge> Neighbours { get; set; }
    }

    class WeightEdge : IWeightedGraphEdge
    {
        public WeightEdge(uint from, uint to, uint weight)
        {
            this.From = from;
            this.To = to;
            this.Weight = weight;
        }

        public uint From { get; private set; }
        public uint To { get; private set; }
        public uint Weight { get; private set; }
    }
}