using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] Quicksort(T[] input) { return Quicksort(input, 0, input.Length); }

        public static T[] Quicksort(T[] input, int start) { return Quicksort(input, start, input.Length - start); }

        public static T[] Quicksort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            Quicksort(input, start, start + length - 1, new Random());
            return input;
        }

        private static void Quicksort(T[] input, int start, int end, Random random)
        {
            if (end - start < 20)
            {
                InsertionSort(input, start, end - start + 1);
                return;
            }

            int l = start + 1, g = end - 1;
            T temp, p1, p2;

            int _p1 = 0, _p2 = 0;
            while (_p1 == _p2)
            {
                _p1 = random.Next(start, end + 1);
                _p2 = random.Next(start, end + 1);
            }

            p1 = input[_p1];
            p2 = input[_p2];

            if (p1.CompareTo(p2) > 0)
            {
                temp = p1;
                p1 = p2;
                p2 = temp;
            }

            input[_p1] = input[start];
            input[start] = p1;

            if (start == _p2)
                _p2 = _p1;

            input[_p2] = input[end];
            input[end] = p2;

            bool pivotValueEqual = input[start].CompareTo(input[end]) == 0;

            for (int i = l; i <= g; i++)
            {
                temp = input[i];
                if (temp.CompareTo(p1) < 0)
                {
                    input[i] = input[l];
                    input[l++] = temp;
                }
                else if (temp.CompareTo(p2) > 0)
                {
                    while (input[g].CompareTo(p2) > 0 && i < g)
                        g--;
                    input[i] = input[g];
                    input[g--] = temp;
                    i--;
                }
            }

            input[start] = input[l - 1];
            input[l - 1] = p1;

            input[end] = input[g + 1];
            input[g + 1] = p2;

            Quicksort(input, start, l - 2, random);
            Quicksort(input, g + 2, end, random);
            if (!pivotValueEqual)
                Quicksort(input, l, g, random);
        }
    }
}