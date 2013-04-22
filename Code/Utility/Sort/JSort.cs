using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] JSort(T[] input) { return JSort(input, 0, input.Length); }

        public static T[] JSort(T[] input, int start) { return JSort(input, start, input.Length - start); }

        public static T[] JSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            int l, r, s, startHeapify = (input.Length - 2) / 2, root, end = start + length;
            T temp;
            for (; startHeapify >= start; startHeapify--)
            {
                root = startHeapify;
                while ((l = (root + 1) * 2 - 1) < end)
                {
                    r = l + 1;
                    s = root;
                    if (input[s].CompareTo(input[l]) > 0)
                        s = l;
                    if (r < end && input[s].CompareTo(input[r]) > 0)
                        s = r;
                    if (s != root)
                    {
                        temp = input[s];
                        input[s] = input[root];
                        input[root] = temp;
                        root = s;
                    }
                    else
                        break;
                }
            }

            startHeapify = end - (end - 2) / 2 - 1;
            for (; startHeapify < end; startHeapify++)
            {
                root = startHeapify;
                while ((l = end - (end - root - 1) * 2 - 2) >= 0)
                {
                    r = l - 1;
                    s = root;
                    if (input[s].CompareTo(input[l]) < 0)
                        s = l;
                    if (r >= 0 && input[s].CompareTo(input[r]) < 0)
                        s = r;
                    if (s != root)
                    {
                        temp = input[s];
                        input[s] = input[root];
                        input[root] = temp;
                        root = s;
                    }
                    else
                        break;
                }
            }

            return BinaryInsertionSort(input);
        }
    }
}