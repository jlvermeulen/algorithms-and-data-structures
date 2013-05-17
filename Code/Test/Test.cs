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
        #region MakeTest

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
        DateTime start, end;

        #endregion

        #region Sort

        string testSize = "Large";
        string testDir = "Random\\";
        string test = testDir + testSize;

        UnzipTestData(test, testDir);
        int[] input = LoadTestData(test);
        
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
        //RunTest(input, test, true, new SortMethod(Sort<int>.JSort), "JSort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.MergeSort), "Merge Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.OddEvenSort), "Odd-Even Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.PatienceSort), "Patience Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.Quicksort), "Quicksort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.SelectionSort), "Selection Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.ShellSort), "Shell Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.StrandSort), "Strand Sort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.Timsort), "Timsort");
        //RunTest(input, test, true, new SortMethod(Sort<int>.TreeSort), "Tree Sort");
        RunTest(input, test, false, new SortMethod(Sort), "Test");

        #endregion

        #region Graph

        //WeightGraph wGraph = new WeightGraph();
        //FlowGraph fGraph = new FlowGraph();
        //List<IFlowGraphEdge> cut;
        //List<IWeightedGraphEdge> mst;

        //start = DateTime.Now;
        //Console.WriteLine(Graph.MaxFlow(fGraph, 0, 15));
        //end = DateTime.Now;
        //Console.WriteLine("Calculated max flow in {0} seconds.", (end - start).TotalSeconds);

        //start = DateTime.Now;
        //cut = Graph.MinCut(fGraph, 0, 15);
        //end = DateTime.Now;
        //foreach (IFlowGraphEdge e in cut)
        //    Console.WriteLine(e.From + " " + e.To);
        //Console.WriteLine("Calculated min cut in {0} seconds.", (end - start).TotalSeconds);

        //Console.WriteLine();
        //start = DateTime.Now;
        //mst = Graph.MinimumSpanningTree(wGraph);
        //end = DateTime.Now;
        //foreach (IWeightedGraphEdge e in mst)
        //    Console.WriteLine(((char)e.From).ToString() + ((char)e.To).ToString());
        //Console.WriteLine("Calculated MST in {0} seconds.", (end - start).TotalSeconds);

        #endregion

        #region Geometry

        //List<Vector2D> list = new List<Vector2D>();
        //list.Add(new Vector2D(0, 0));
        //list.Add(new Vector2D(1, 2));
        //list.Add(new Vector2D(-2, 1));
        //list.Add(new Vector2D(-1, -1));
        //list.Add(new Vector2D(3, 4));
        //list.Add(new Vector2D(4, 3));
        //list.Add(new Vector2D(-5, 4));
        //list.Add(new Vector2D(6, 5));
        //list.Add(new Vector2D(7, 7));
        //list.Add(new Vector2D(7, -7));
        //list.Add(new Vector2D(-7, -7));
        //list.Add(new Vector2D(-7, 7));
        //list.Add(new Vector2D(9, 0));
        //list.Add(new Vector2D(-9, 0));
        //list.Add(new Vector2D(0, 9));
        //list.Add(new Vector2D(0, -9));
        //list.Add(new Vector2D(-8, 0));
        //list.Add(new Vector2D(8, 0));
        //list.Add(new Vector2D(-7, 0));
        //list.Add(new Vector2D(7, 0));
        //list.Add(new Vector2D(-6, 0));
        //list.Add(new Vector2D(6, 0));
        //list.Add(new Vector2D(-5, 0));
        //list.Add(new Vector2D(5, 0));
        //list.Add(new Vector2D(-4, 0));
        //list.Add(new Vector2D(4, 0));
        //list.Add(new Vector2D(-3, 0));
        //list.Add(new Vector2D(3, 0));
        //list.Add(new Vector2D(-2, 0));
        //list.Add(new Vector2D(2, 0));
        //list.Add(new Vector2D(-1, 0));
        //list.Add(new Vector2D(1, 0));
        //list.Add(new Vector2D(0, -8));
        //list.Add(new Vector2D(0, 8));
        //list.Add(new Vector2D(0, -7));
        //list.Add(new Vector2D(0, 7));
        //list.Add(new Vector2D(0, -6));
        //list.Add(new Vector2D(0, 6));
        //list.Add(new Vector2D(0, -5));
        //list.Add(new Vector2D(0, 5));
        //list.Add(new Vector2D(0, -4));
        //list.Add(new Vector2D(0, 4));
        //list.Add(new Vector2D(0, -3));
        //list.Add(new Vector2D(0, 3));
        //list.Add(new Vector2D(0, -2));
        //list.Add(new Vector2D(0, 2));
        //list.Add(new Vector2D(0, -1));
        //list.Add(new Vector2D(0, 1));
        //list.Add(new Vector2D(1, 1));
        //list.Add(new Vector2D(2, 2));
        //list.Add(new Vector2D(3, 3));
        //list.Add(new Vector2D(4, 4));
        //list.Add(new Vector2D(5,5));
        //list.Add(new Vector2D(6, 6));
        //list.Add(new Vector2D(1, -1));
        //list.Add(new Vector2D(2, -2));
        //list.Add(new Vector2D(3, -3));
        //list.Add(new Vector2D(4, -4));
        //list.Add(new Vector2D(5, -5));
        //list.Add(new Vector2D(6, -6));
        //list.Add(new Vector2D(-1, 1));
        //list.Add(new Vector2D(-2, 2));
        //list.Add(new Vector2D(-3, 3));
        //list.Add(new Vector2D(-4, 4));
        //list.Add(new Vector2D(-5, 5));
        //list.Add(new Vector2D(-6, 6));
        //list.Add(new Vector2D(-1, -1));
        //list.Add(new Vector2D(-2, -2));
        //list.Add(new Vector2D(-3, -3));
        //list.Add(new Vector2D(-4, -4));
        //list.Add(new Vector2D(-5, -5));
        //list.Add(new Vector2D(-6, -6));

        //list = Geometry.ConvexHull(list);
        //foreach (Vector2D v in list)
        //    Console.WriteLine(v);

        #endregion

        #region Search

        //Interpolation<int> interpolate = new Interpolation<int>((x, y, z) => { return (double)(x - y) / (z - y); });
        //Interpolator<int> interpolator = Interpolator<int>.Create(interpolate);

        //Console.WriteLine("Using C# Search.");
        //int pos;
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Array.BinarySearch<int>(input, input[i]);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        //Console.WriteLine("Using Binary Search.");
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Search<int>.BinarySearch(input, input[i]);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        //Console.WriteLine("Using Gallop Search.");
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Search<int>.GallopSearch(input, input[i]);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        //Console.WriteLine("Using Interpolation Search.");
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Search<int>.InterpolationSearch(input, input[i], interpolate);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        //Console.WriteLine("Using Random Search.");
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Search<int>.RandomSearch(input, input[i]);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        //Console.WriteLine("Using Binary Interpolation Search.");
        //start = DateTime.Now;
        //for (int i = 0; i < input.Length; i++)
        //{
        //    pos = Search<int>.BinaryInterpolationSearch(input, input[i], interpolate);
        //    if (input[pos] != input[i])
        //        Console.WriteLine("Wrong position for {0}: returned {1}, should be {2}.", input[i], pos, i);
        //}
        //end = DateTime.Now;
        //Console.WriteLine("Searched and found {0} items in {1} seconds.", input.Length, (end - start).TotalSeconds);
        //Console.WriteLine();

        #endregion

        #region Probabilistic

        //BloomFilter<string> filter = new BloomFilter<string>(1000, 0.001, PrimaryHash, SecondaryHash);
        //HashSet<string> elems = new HashSet<string>();
        //Random random = new Random();
        //byte[] buffer = new byte[10];
        //string item;
        //int size = 1000, falsePositives = 0, testSize = 1000000;

        //for (int i = 0; i < size; i++)
        //{
        //    random.NextBytes(buffer);
        //    item = System.Text.Encoding.ASCII.GetString(buffer);
        //    filter.Add(item);
        //    elems.Add(item);
        //}
        //Console.WriteLine("Confidence after adding {0} elements is {1}.", size, filter.Confidence);
        //Console.WriteLine("Expected false positives: {0}/{1}.", (int)((1 - filter.Confidence) * testSize), testSize);

        //for (int i = 0; i < testSize; i++)
        //{
        //    random.NextBytes(buffer);
        //    item = System.Text.Encoding.ASCII.GetString(buffer);
        //    if (filter.Contains(item) != elems.Contains(item))
        //    {
        //        falsePositives++;
        //        filter.Contains(item);
        //    }
        //}
        
        //Console.WriteLine("Actual false positives: {0}/{1}.", falsePositives, testSize);
        //Console.WriteLine("Performance factor: {0}.", (1 - filter.Confidence) * testSize / falsePositives);

        #endregion

        File.Delete("TestData\\" + test + ".in");
        File.Delete("TestData\\" + test + ".out");

        Console.ReadLine();
    }

    static int[] Sort(int[] input)
    {
        PairingMinHeap<int> heap = new PairingMinHeap<int>(input);

        int i = 0;
        while (heap.Count > 0)
            input[i++] = heap.Extract();

        return input;
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

    static uint PrimaryHash(string item)
    {
        uint hash = 0;
        for (int i = 0; i < item.Length; i++)
        {
            hash += item[i];
            hash += (hash << 10);
            hash ^= (hash >> 6);
        }
        hash += (hash << 3);
        hash ^= (hash >> 11);
        hash += (hash << 15);
        return hash;
    }

    static uint SecondaryHash(string item)
    {
        uint hash = 5381;
        for (int i = 0; i < item.Length; i++)
            hash = ((hash << 5) + hash) + item[i];
        return hash;
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

            this.Nodes = new Dictionary<uint, IGraphNode<IFlowGraphEdge>>() { { 0, n0 }, { 1, n1 }, { 2, n2 }, { 3, n3 }, { 4, n4 }, { 5, n5 }, { 6, n6 }, { 7, n7 }, { 8, n8 }, { 9, n9 }, { 10, n10 }, { 11, n11 }, { 12, n12 }, { 13, n13 }, { 14, n14 }, { 15, n15 } };
            this.Edges = new Dictionary<uint, Dictionary<uint, IFlowGraphEdge>>()
            {
                { 0, new Dictionary<uint, IFlowGraphEdge>() { { 1, e01 }, { 2, e02 }, { 3, e03 }, { 4, e04 } } },
                { 1, new Dictionary<uint, IFlowGraphEdge>() { { 5, e15 } } },
                { 2, new Dictionary<uint, IFlowGraphEdge>() { { 5, e25 }, { 6, e26 } } },
                { 3, new Dictionary<uint, IFlowGraphEdge>() { { 8, e38 }, { 13, e313 } } },
                { 4, new Dictionary<uint, IFlowGraphEdge>() { { 8, e48 }, { 10, e410 } } },
                { 5, new Dictionary<uint, IFlowGraphEdge>() { { 7, e57 } } },
                { 6, new Dictionary<uint, IFlowGraphEdge>() { { 7, e67 } } },
                { 7, new Dictionary<uint, IFlowGraphEdge>() { { 9, e79 } } },
                { 8, new Dictionary<uint, IFlowGraphEdge>() { { 10, e810 }, { 13, e813 } } },
                { 9, new Dictionary<uint, IFlowGraphEdge>() { { 11, e911 }, { 12, e912 }, { 13, e913 } } },
                { 10, new Dictionary<uint, IFlowGraphEdge>() { { 15, e1015 } } },
                { 11, new Dictionary<uint, IFlowGraphEdge>() { { 14, e1114 } } },
                { 12, new Dictionary<uint, IFlowGraphEdge>() { { 14, e1214 }, { 15, e1215 } } },
                { 13, new Dictionary<uint, IFlowGraphEdge>() { { 15, e1315 } } }
            };
        }

        public Dictionary<uint, IGraphNode<IFlowGraphEdge>> Nodes { get; private set; }
        public Dictionary<uint, Dictionary<uint, IFlowGraphEdge>> Edges { get; private set; }
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

            ab = new WeightEdge((uint)'A', (uint)'B', 4);
            ah = new WeightEdge((uint)'A', (uint)'H', 8);
            bc = new WeightEdge((uint)'B', (uint)'C', 8);
            bh = new WeightEdge((uint)'B', (uint)'H', 11);
            cd = new WeightEdge((uint)'C', (uint)'D', 7);
            cf = new WeightEdge((uint)'C', (uint)'F', 4);
            ci = new WeightEdge((uint)'C', (uint)'I', 2);
            de = new WeightEdge((uint)'D', (uint)'E', 9);
            df = new WeightEdge((uint)'D', (uint)'F', 14);
            ef = new WeightEdge((uint)'E', (uint)'F', 10);
            fg = new WeightEdge((uint)'F', (uint)'G', 2);
            gh = new WeightEdge((uint)'G', (uint)'H', 1);
            gi = new WeightEdge((uint)'G', (uint)'I', 6);
            hi = new WeightEdge((uint)'H', (uint)'I', 7);

            a = new WeightNode((uint)'A', new WeightEdge[] { ab, ah });
            b = new WeightNode((uint)'B', new WeightEdge[] { bc, bh });
            c = new WeightNode((uint)'C', new WeightEdge[] { cd, cf, ci });
            d = new WeightNode((uint)'D', new WeightEdge[] { de, df });
            e = new WeightNode((uint)'E', new WeightEdge[] { ef });
            f = new WeightNode((uint)'F', new WeightEdge[] { fg });
            g = new WeightNode((uint)'G', new WeightEdge[] { gh, gi });
            h = new WeightNode((uint)'H', new WeightEdge[] { hi });
            i = new WeightNode((uint)'I', new WeightEdge[] { });

            this.Nodes = new Dictionary<uint, IGraphNode<IWeightedGraphEdge>>() { { a.ID, a }, { b.ID, b }, { c.ID, c }, { d.ID, d }, { e.ID, e }, { f.ID, f }, { g.ID, g }, { h.ID, h }, { i.ID, i } };
            this.Edges = new Dictionary<uint, Dictionary<uint, IWeightedGraphEdge>>()
            {
                { a.ID, new Dictionary<uint, IWeightedGraphEdge>() { { b.ID, ab }, { h.ID, ah } } },
                { b.ID, new Dictionary<uint, IWeightedGraphEdge>() { { c.ID, bc }, { h.ID, bh } } },
                { c.ID, new Dictionary<uint, IWeightedGraphEdge>() { { d.ID, cd }, { f.ID, cf }, { i.ID, ci } } },
                { d.ID, new Dictionary<uint, IWeightedGraphEdge>() { { e.ID, de }, { f.ID, df } } },
                { e.ID, new Dictionary<uint, IWeightedGraphEdge>() { { f.ID, ef } } },
                { f.ID, new Dictionary<uint, IWeightedGraphEdge>() { { g.ID, fg } } },
                { g.ID, new Dictionary<uint, IWeightedGraphEdge>() { { h.ID, gh }, { i.ID, gi } } },
                { h.ID, new Dictionary<uint, IWeightedGraphEdge>() { { i.ID, hi } } }
            };
        }

        public Dictionary<uint, IGraphNode<IWeightedGraphEdge>> Nodes { get; private set; }
        public Dictionary<uint, Dictionary<uint, IWeightedGraphEdge>> Edges { get; private set; }
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