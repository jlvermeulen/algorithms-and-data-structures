using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        const int MIN_GALLOP = 7;

        public static T[] Timsort(T[] input) { return Timsort(input, 0, input.Length); }

        public static T[] Timsort(T[] input, int start) { return Timsort(input, start, input.Length - start); }

        public static T[] Timsort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            List<Tuple<int, int>> runs = new List<Tuple<int, int>>();
            int minrun, r = 0, mingallop = MIN_GALLOP;
            minrun = length;
            while (minrun >= 64)
            {
                r |= minrun & 1;
                minrun >>= 1;
            }
            minrun += r;

            int runStart = start, runLength = 0, i = start, maxMinRun;
            while (i < start + length - 1)
            {
                runLength++;
                if (input[i].CompareTo(input[i + 1]) <= 0)
                {
                    while (input[i].CompareTo(input[++i]) <= 0)
                    {
                        runLength++;
                        if (i == input.Length - 1)
                            break;
                    }
                }
                else
                {
                    while (input[i].CompareTo(input[++i]) > 0)
                    {
                        runLength++;
                        if (i == input.Length - 1)
                            break;
                    }
                    Array.Reverse(input, runStart, runLength);
                }

                maxMinRun = Math.Min(start + length - runStart, minrun);
                if (runLength < minrun)
                {
                    BinaryInsertionSort(input, runStart, maxMinRun);
                    runLength = maxMinRun;
                }
                runs.Add(new Tuple<int, int>(runStart, runLength));
                if (runs.Count > 1)
                    CollapseRuns(input, runs, ref mingallop);
                i = runStart = runStart + runLength;
                runLength = 0;
            }

            while (runs.Count > 1)
            {
                int n = runs.Count - 2;
                if (n > 0 && runs[n - 1].Item2 < runs[n + 1].Item2)
                    n--;
                Merge(input, runs[n], runs[n + 1], ref mingallop);
                runs[n] = new Tuple<int, int>(runs[n].Item1, runs[n].Item2 + runs[n + 1].Item2);
                runs.RemoveAt(n + 1);
            }

            return input;
        }

        private static void CollapseRuns(T[] input, List<Tuple<int, int>> runs, ref int mingallop)
        {
            int n;
            while (runs.Count > 1)
            {
                n = runs.Count - 2;
                if (n > 0 && runs[n - 1].Item2 <= runs[n].Item2 + runs[n + 1].Item2)
                {
                    if (runs[n - 1].Item2 < runs[n + 1].Item2)
                        n--;
                    Merge(input, runs[n], runs[n + 1], ref mingallop);
                }
                else if (runs[n].Item2 <= runs[n + 1].Item2)
                    Merge(input, runs[n], runs[n + 1], ref mingallop);
                else
                    break;
                runs[n] = new Tuple<int, int>(runs[n].Item1, runs[n].Item2 + runs[n + 1].Item2);
                runs.RemoveAt(n + 1);
            }
        }

        private static void Merge(T[] input, Tuple<int, int> run1, Tuple<int, int> run2, ref int mingallop)
        {
            int a = Search<T>.GallopSearchRight(input, input[run2.Item1], run1.Item1, run1.Item1 + run1.Item2);
            int b = Search<T>.GallopSearchLeft(input, input[run1.Item1 + run1.Item2 - 1], run2.Item1, run2.Item1 + run2.Item2);
            run1 = new Tuple<int, int>(a, run1.Item1 + run1.Item2 - a);
            run2 = new Tuple<int, int>(run2.Item1, b - run2.Item1 + 1);

            if (run1.Item2 <= run2.Item2)
                MergeLeftToRight(input, run1, run2, ref mingallop);
            else
                MergeRightToLeft(input, run1, run2, ref mingallop);
        }

        private static void MergeLeftToRight(T[] input, Tuple<int, int> run1, Tuple<int, int> run2, ref int mingallop)
        {
            T[] temp = new T[run1.Item2];
            Buffer.BlockCopy(input, run1.Item1 * sizeof(int), temp, 0, run1.Item2 * sizeof(int));
            int acount = 0, bcount = 0, currgallop = mingallop;

            int i = run1.Item1, j = 0, k = run2.Item1, end = run2.Item1 + run2.Item2, n;
            while (true)
            {
                while (true)
                {
                    if (temp[j].CompareTo(input[k]) > 0)
                    {
                        input[i++] = input[k++];
                        if (k == end)
                        {
                            Buffer.BlockCopy(temp, j * sizeof(int), input, i * sizeof(int), (temp.Length - j) * sizeof(int));
                            return;
                        }
                        acount++;
                        bcount = 0;
                        if (acount >= currgallop)
                            break;
                    }
                    else
                    {
                        input[i++] = temp[j++];
                        if (j == temp.Length)
                            return;
                        bcount++;
                        acount = 0;
                        if (bcount >= currgallop)
                            break;
                    }
                }
                currgallop++;
                do
                {
                    currgallop -= currgallop > 1 ? 1 : 0;
                    mingallop = currgallop;
                    n = Search<T>.GallopSearchRight(temp, input[k], j - 1, temp.Length);
                    acount = n - j + 1;
                    if (acount > 0)
                    {
                        Buffer.BlockCopy(temp, j * sizeof(int), input, i * sizeof(int), acount * sizeof(int));
                        i += acount;
                        j += acount;
                        if (j == temp.Length)
                            return;
                    }
                    input[i++] = input[k++];
                    if (k == end)
                    {
                        Buffer.BlockCopy(temp, j * sizeof(int), input, i * sizeof(int), (temp.Length - j) * sizeof(int));
                        return;
                    }

                    n = Search<T>.GallopSearchRight(input, temp[j], k - 1, end);
                    bcount = n - k + 1;
                    if (bcount > 0)
                    {
                        Buffer.BlockCopy(input, k * sizeof(int), input, i * sizeof(int), bcount * sizeof(int));
                        i += bcount;
                        k += bcount;
                        if (k == end)
                        {
                            Buffer.BlockCopy(temp, j * sizeof(int), input, i * sizeof(int), (temp.Length - j) * sizeof(int));
                            return;
                        }
                    }
                    input[i++] = temp[j++];
                    if (j == input.Length)
                        return;
                }
                while (acount >= MIN_GALLOP || bcount >= MIN_GALLOP);
                currgallop++;
                mingallop = currgallop;
            }
        }

        private static void MergeRightToLeft(T[] input, Tuple<int, int> run1, Tuple<int, int> run2, ref int mingallop)
        {
            T[] temp = new T[run2.Item2];
            Buffer.BlockCopy(input, run2.Item1 * sizeof(int), temp, 0, run2.Item2 * sizeof(int));
            int acount = 0, bcount = 0, currgallop = mingallop;

            int i = run2.Item1 + run2.Item2 - 1, j = run2.Item2 - 1, k = run1.Item1 + run1.Item2 - 1, n;
            while (true)
            {
                while (true)
                {
                    if (input[k].CompareTo(temp[j]) > 0)
                    {
                        input[i--] = input[k--];
                        if (k == run1.Item1 - 1)
                        {
                            Buffer.BlockCopy(temp, 0, input, run1.Item1 * sizeof(int), (j + 1) * sizeof(int));
                            return;
                        }
                        acount++;
                        bcount = 0;
                        if (acount >= currgallop)
                            break;
                    }
                    else
                    {
                        input[i--] = temp[j--];
                        if (j == -1)
                            return;
                        bcount++;
                        acount = 0;
                        if (bcount >= currgallop)
                            break;
                    }
                }
                currgallop++;
                do
                {
                    currgallop -= currgallop > 1 ? 1 : 0;
                    mingallop = currgallop;
                    n = Search<T>.GallopSearchLeft(temp, input[k], -1, j + 1);
                    acount = j - n;
                    if (acount > 0)
                    {
                        i -= acount;
                        j -= acount;
                        Buffer.BlockCopy(temp, (j + 1) * sizeof(int), input, (i + 1) * sizeof(int), acount * sizeof(int));
                        if (j == -1)
                            return;
                    }
                    input[i--] = input[k--];
                    if (k == run1.Item1 - 1)
                    {
                        Buffer.BlockCopy(temp, 0, input, run1.Item1 * sizeof(int), (j + 1) * sizeof(int));
                        return;
                    }

                    n = Search<T>.GallopSearchLeft(input, temp[j], run1.Item1 - 1, k + 1);
                    bcount = k - n;
                    if (bcount > 0)
                    {
                        i -= bcount;
                        k -= bcount;
                        Buffer.BlockCopy(input, (k + 1) * sizeof(int), input, (i + 1) * sizeof(int), bcount * sizeof(int));
                        if (k == run1.Item1 - 1)
                        {
                            Buffer.BlockCopy(temp, 0, input, run1.Item1 * sizeof(int), (j + 1) * sizeof(int));
                            return;
                        }
                    }
                    input[i--] = temp[j--];
                    if (j == -1)
                        return;
                }
                while (acount >= MIN_GALLOP || bcount >= MIN_GALLOP);
                currgallop++;
                mingallop = currgallop;
            }
        }
    }
}