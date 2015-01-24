using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utility.Algorithms.ComputationalGeometry;
using Utility.Algorithms.Graph;
using Utility.Algorithms.Hash;
using Utility.Algorithms.Search;
using Utility.Algorithms.Select;
using Utility.Algorithms.Sort;
using Utility.DataStructures.BinarySearchTree;
using Utility.DataStructures.DisjointSet;
using Utility.DataStructures.PriorityQueue;
using Utility.DataStructures.Probabilistic;
using Utility.DataStructures.Trees;

class Test
{
    delegate int[] SortMethod(int[] input);

    static void Main()
    {
        #region Sort

        //string testSize = "Large";
        //string testDir = "Random\\";
        //string test = testDir + testSize;
        
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
        //RunTest(input, test, false, new SortMethod(Sort), "Test");

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

        #region Trie

        //Trie<string, bool> trie2 = new Trie<string, bool>(new bool[] { false, true }, SplitIP, JoinIP, (x => x ? 1 : 0));

        //trie2.Add("192.168.20.16");
        //trie2.Add("192.168.0.0");
        //Console.WriteLine(trie2.LongestPrefixMatch("192.168.20.19"));

        //Console.ReadLine();
        //Trie<string, char> trie = new Trie<string, char>("abcdefghijklmnopqrstuvwxyz".ToCharArray(), (x => x.ToCharArray()), (x => new string(x)), (x => (int)x - (int)'a'));
        //Dictionary<string, uint> dict = new Dictionary<string, uint>();

        //Regex regex = new Regex(@"\b[a-z]+\b");
        //DirectoryInfo d = new DirectoryInfo(@"E:\Books");
        //string line;

        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        //foreach (FileInfo f in d.GetFiles())
        //{
        //    StreamReader reader = new StreamReader(f.FullName);
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.ToLower();
        //        MatchCollection matches = regex.Matches(line);
        //        foreach (Match m in matches)
        //            trie.Add(m.Value);
        //    }
        //    break;
        //}
        //sw.Stop();

        //Console.WriteLine("Added {0} words to the Trie in {1} seconds.", trie.Count, sw.Elapsed.TotalSeconds);

        //int prevCount = trie.Count;
        //sw.Start();
        //foreach (FileInfo f in d.GetFiles())
        //{
        //    StreamReader reader = new StreamReader(f.FullName);
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.ToLower();
        //        MatchCollection matches = regex.Matches(line);
        //        foreach (Match m in matches)
        //            trie.Remove(m.Value);
        //    }
        //    reader.Close();
        //}
        //sw.Stop();

        //Console.WriteLine("Removed {0} words from the Trie in {1} seconds.", prevCount - trie.Count, sw.Elapsed.TotalSeconds);
        //Console.ReadLine();

        //sw.Start();
        //foreach (FileInfo f in d.GetFiles())
        //{
        //    StreamReader reader = new StreamReader(f.FullName);
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.ToLower();
        //        MatchCollection matches = regex.Matches(line);
        //        foreach (Match m in matches)
        //        {
        //            uint c;
        //            if (dict.TryGetValue(m.Value, out c))
        //                dict[m.Value] = c + 1;
        //            else
        //                dict[m.Value] = 1;
        //        }
        //    }
        //    reader.Close();
        //}
        //sw.Stop();

        //long count = 0;
        //foreach (KeyValuePair<string, uint> kvp in dict)
        //    count += kvp.Value;
        //Console.WriteLine("Added {0} words to the Dictionary in {1} seconds.", count, sw.Elapsed.TotalSeconds);

        //sw.Start();
        //foreach (FileInfo f in d.GetFiles())
        //{
        //    StreamReader reader = new StreamReader(f.FullName);
        //    while ((line = reader.ReadLine()) != null)
        //    {
        //        line = line.ToLower();
        //        MatchCollection matches = regex.Matches(line);
        //        foreach (Match m in matches)
        //        {
        //            uint c = dict[m.Value];
        //            if (c > 1)
        //                dict[m.Value] = c - 1;
        //            else
        //                dict.Remove(m.Value);
        //        }
        //    }
        //    reader.Close();
        //}
        //sw.Stop();

        //Console.WriteLine("Removed {0} words from the Dictionary in {1} seconds.", count, sw.Elapsed.TotalSeconds);

        //Console.ReadLine();

        #endregion

        #region Hash

        //string file = @"G:\a.txt";

        //Stopwatch sw = new Stopwatch();
        //sw.Start();
        //Console.WriteLine("Adler-32: {0}", Hash.Adler32(file).ToString("X"));
        //sw.Stop();
        //Console.WriteLine("Hashed in {0} ms.", sw.ElapsedMilliseconds);
        //Console.WriteLine();

        //sw.Reset();
        //sw.Start();
        //Console.WriteLine("CRC-32: {0}", Hash.CRC32(file).ToString("X"));
        //sw.Stop();
        //Console.WriteLine("Hashed in {0} ms.", sw.ElapsedMilliseconds);
        //Console.WriteLine();

        //sw.Reset();
        //sw.Start();
        //byte[] md5 = Hash.MD5(file);
        //Console.Write("MD5: ");
        //foreach (byte b in md5)
        //    Console.Write(b.ToString("X"));
        //Console.WriteLine();
        //sw.Stop();
        //Console.WriteLine("Hashed in {0} ms.", sw.ElapsedMilliseconds);
        //Console.WriteLine();

        //Console.ReadLine();

        #endregion

        #region Select

        //int size = 10;
        //Random random = new Random();

        //for (int i = 0; i < 100; i++)
        //{
        //    Console.WriteLine("Test {0}.", i + 1);

        //    int[] data1 = new int[size], data2 = new int[size];
        //    for (int j = 0; j < size; j++)
        //        data1[j] = data2[j] = random.Next();

        //    Array.Sort(data2);
        //    for (int j = 0; j < size; j++)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Green;
        //        int val = Select<int>.MedianOf3Quickselect(data1, j);
        //        if (data2[j] != val)
        //            Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("{0}:\t\t{1}\t\t==\t\t{2}", j, data2[j], val);
        //    }
        //}

        #endregion

        Graph g = new Graph();
        g.AddEdge(0, 1, true, 4);
        g.AddEdge(0, 2, false, 3);
        g.AddEdge(0, 4, true, 7);
        g.AddEdge(1, 3, true, 1);
        g.AddEdge(2, 4, true, 4);
        g.AddEdge(3, 4, true, 3);
        g.AddEdge(3, 5, true, 1);
        g.AddEdge(4, 5, true, 1);
        g.AddEdge(4, 6, true, 5);
        g.AddEdge(4, 8, true, 3);
        g.AddEdge(5, 7, false, 2);
        g.AddEdge(5, 8, true, 4);
        g.AddEdge(7, 8, true, 3);
        g.AddEdge(8, 6, true, 5);

        Path path0;
        g.Dijkstra(0, 8, out path0);

        Dictionary<uint, Dictionary<uint, int>> lengths1;
        Dictionary<uint, Dictionary<uint, uint>> parents1;
        g.FloydWarshall(out lengths1, out parents1);
        Path path1 = Path.FromParents(0, 8, parents1, g);

        Dictionary<uint, int> lengths2;
        Dictionary<uint, uint> parents2;
        g.BellmanFord(0, out lengths2, out parents2);
        Path path2 = Path.FromParents(0, 8, parents2, g);

        Dictionary<uint, Dictionary<uint, int>> lengths3;
        Dictionary<uint, Dictionary<uint, uint>> parents3;
        g.FloydWarshall(out lengths3, out parents3);
        Path path3 = Path.FromParents(0, 8, parents3, g);

        Console.WriteLine("Length Dijkstra:\t{0}", path0.Weight);
        Console.WriteLine("Length Floyd-Warshall:\t{0}", path1.Weight);
        Console.WriteLine("Length Bellman-Ford:\t{0}", path2.Weight);
        Console.WriteLine("Length Johnshon:\t{0}", path3.Weight);

        foreach (Edge e in path0.Edges)
            Console.WriteLine("{0} -> {1}", e.From, e.To);
        Console.WriteLine();
        foreach (Edge e in path1.Edges)
            Console.WriteLine("{0} -> {1}", e.From, e.To);
        Console.WriteLine();
        foreach (Edge e in path2.Edges)
            Console.WriteLine("{0} -> {1}", e.From, e.To);
        Console.WriteLine();
        foreach (Edge e in path3.Edges)
            Console.WriteLine("{0} -> {1}", e.From, e.To);

        Console.ReadLine();
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

    static Matrix LargeInvertibleMatrix()
    {
        Matrix matrix = new Matrix(10);
        matrix[0, 0] = 1;
        matrix[0, 1] = 5;
        matrix[0, 2] = 9;
        matrix[0, 3] = 2;
        matrix[0, 4] = 3;
        matrix[0, 5] = 5;
        matrix[0, 6] = 2;
        matrix[0, 7] = 4;
        matrix[0, 8] = 3;
        matrix[0, 9] = 7;
        matrix[1, 0] = 4;
        matrix[1, 1] = 8;
        matrix[1, 2] = 1;
        matrix[1, 3] = 2;
        matrix[1, 4] = 5;
        matrix[1, 5] = 4;
        matrix[1, 6] = 3;
        matrix[1, 7] = 1;
        matrix[1, 8] = 8;
        matrix[1, 9] = 9;
        matrix[2, 0] = 1;
        matrix[2, 1] = 2;
        matrix[2, 2] = 3;
        matrix[2, 3] = 4;
        matrix[2, 4] = 5;
        matrix[2, 5] = 6;
        matrix[2, 6] = 7;
        matrix[2, 7] = 8;
        matrix[2, 8] = 9;
        matrix[2, 9] = 0;
        matrix[3, 0] = 0;
        matrix[3, 1] = 1;
        matrix[3, 2] = 5;
        matrix[3, 3] = 3;
        matrix[3, 4] = 8;
        matrix[3, 5] = 7;
        matrix[3, 6] = 1;
        matrix[3, 7] = 2;
        matrix[3, 8] = 3;
        matrix[3, 9] = 5;
        matrix[4, 0] = 9;
        matrix[4, 1] = 4;
        matrix[4, 2] = 3;
        matrix[4, 3] = 2;
        matrix[4, 4] = 1;
        matrix[4, 5] = 5;
        matrix[4, 6] = 1;
        matrix[4, 7] = 6;
        matrix[4, 8] = 2;
        matrix[4, 9] = 8;
        matrix[5, 0] = 1;
        matrix[5, 1] = 2;
        matrix[5, 2] = 9;
        matrix[5, 3] = 2;
        matrix[5, 4] = 3;
        matrix[5, 5] = 5;
        matrix[5, 6] = 2;
        matrix[5, 7] = 4;
        matrix[5, 8] = 3;
        matrix[5, 9] = 7;
        matrix[6, 0] = 4;
        matrix[6, 1] = 5;
        matrix[6, 2] = 1;
        matrix[6, 3] = 2;
        matrix[6, 4] = 5;
        matrix[6, 5] = 4;
        matrix[6, 6] = 3;
        matrix[6, 7] = 1;
        matrix[6, 8] = 2;
        matrix[6, 9] = 9;
        matrix[7, 0] = 3;
        matrix[7, 1] = 2;
        matrix[7, 2] = 3;
        matrix[7, 3] = 4;
        matrix[7, 4] = 5;
        matrix[7, 5] = 6;
        matrix[7, 6] = 6;
        matrix[7, 7] = 8;
        matrix[7, 8] = 9;
        matrix[7, 9] = 0;
        matrix[8, 0] = 0;
        matrix[8, 1] = 1;
        matrix[8, 2] = 5;
        matrix[8, 3] = 3;
        matrix[8, 4] = 2;
        matrix[8, 5] = 7;
        matrix[8, 6] = 1;
        matrix[8, 7] = 2;
        matrix[8, 8] = 3;
        matrix[8, 9] = 5;
        matrix[9, 0] = 9;
        matrix[9, 1] = 4;
        matrix[9, 2] = 3;
        matrix[9, 3] = 2;
        matrix[9, 4] = 1;
        matrix[9, 5] = 5;
        matrix[9, 6] = 8;
        matrix[9, 7] = 6;
        matrix[9, 8] = 2;
        matrix[9, 9] = 8;

        return matrix;
    }

    static bool[] SplitIP(string ip)
    {
        string[] ss = ip.Split('.');
        List<bool> bools = new List<bool>();
        uint value = 0;
        for (int i = 3; i >= 0; i--)
            value += uint.Parse(ss[3 - i]) << (8 * i);

        int falseCount = 0;
        for (int i = 0; i < 32; i++)
        {
            if (((value >> (31 - i)) & 1) == 1)
            {
                while (falseCount-- > 0)
                    bools.Add(false);
                bools.Add(true);
                falseCount = 0;
            }
            else
                falseCount++;
        }

        return bools.ToArray();
    }

    static string JoinIP(bool[] bits)
    {
        uint value = 0;
        for (int i = 0; i < bits.Length; i++)
            value += (uint)(bits[i] ? 1 : 0) << (31 - i);

        byte[] bytes = BitConverter.GetBytes(value);

        return bytes[3].ToString() + "." + bytes[2].ToString() + "." + bytes[1].ToString() + "." + bytes[0].ToString();
    }
}