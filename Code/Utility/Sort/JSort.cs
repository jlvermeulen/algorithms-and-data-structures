using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] JSort(T[] input)
        {
            int l, r, s, start = (input.Length - 2) / 2, root;
            T temp;
            for (; start >= 0; start--)
            {
                root = start;
                while ((l = (root + 1) * 2 - 1) < input.Length)
                {
                    r = l + 1;
                    s = root;
                    if (input[s].CompareTo(input[l]) > 0)
                        s = l;
                    if (r < input.Length && input[s].CompareTo(input[r]) > 0)
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

            start = input.Length - (input.Length - 2) / 2 - 1;
            for (; start < input.Length; start++)
            {
                root = start;
                while ((l = input.Length - (input.Length - root - 1) * 2 - 2) >= 0)
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