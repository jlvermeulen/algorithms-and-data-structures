using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] Heapsort(T[] input)
        {
            Heapify(input);
            int length = input.Length;
            while (length > 0)
            {
                Switch(input, 0, --length);
                PushDown(input, length, 0);
            }
            return input;
        }

        private static void Heapify(T[] heap)
        {
            int start = (heap.Length - 2) / 5;

            for (; start >= 0; start--)
                PushDown(heap, heap.Length, start);
        }

        private static void PushDown(T[] heap, int length, int root)
        {
            int l, c1, c2, c3, c4, s;
            while ((l = Left(root, length)) != -1)
            {
                c1 = l + 1;
                c2 = l + 2;
                c3 = l + 3;
                c4 = l + 4;
                s = root;
                if (heap[s].CompareTo(heap[l]) < 0)
                    s = l;
                if (c1 < length && heap[s].CompareTo(heap[c1]) < 0)
                    s = c1;
                if (c2 < length && heap[s].CompareTo(heap[c2]) < 0)
                    s = c2;
                if (c3 < length && heap[s].CompareTo(heap[c3]) < 0)
                    s = c3;
                if (c4 < length && heap[s].CompareTo(heap[c4]) < 0)
                    s = c4;
                if (s != root)
                {
                    Switch(heap, s, root);
                    root = s;
                }
                else
                    return;
            }
        }

        private static int Left(int node, int length)
        {
            int l = node * 5 + 1;
            if (l < length)
                return l;
            return -1;
        }

        private static void Switch(T[] heap, int i1, int i2)
        {
            T temp = heap[i1];
            heap[i1] = heap[i2];
            heap[i2] = temp;
        }
    }
}