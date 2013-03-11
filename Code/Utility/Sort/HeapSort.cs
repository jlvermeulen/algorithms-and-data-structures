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
            int start = (heap.Length - 2) / 2;

            for (; start >= 0; start--)
                PushDown(heap, heap.Length, start);
        }

        private static void PushDown(T[] heap, int length, int root)
        {
            int l, r, s;
            while ((l = Left(heap, length, root)) != -1)
            {
                r = l + 1;
                s = root;
                if (heap[s].CompareTo(heap[l]) < 0)
                    s = l;
                if (r < length && heap[s].CompareTo(heap[r]) < 0)
                    s = r;
                if (s != root)
                {
                    Switch(heap, s, root);
                    root = s;
                }
                else
                    return;
            }
        }

        private static int Parent(T[] heap, int length, int node)
        {
            int p = (node + 1) / 2 - 1;
            if (p >= 0 && p < length)
                return p;
            return -1;
        }

        private static int Left(T[] heap, int length, int node)
        {
            int l = (node + 1) * 2 - 1;
            if (l < length)
                return l;
            return -1;
        }

        private static int Right(T[] heap, int length, int node)
        {
            int r = (node + 1) * 2;
            if (r < heap.Length)
                return r;
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