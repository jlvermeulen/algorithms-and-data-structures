using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static void MergeSort(T[] input)
        {
            if (input.Length > 1)
            {
                int half = input.Length / 2;
                T[] left = new T[half];
                T[] right = new T[input.Length - half];
                for (int i = 0; i < half; i++)
                    left[i] = input[i];
                for (int i = 0; i < input.Length - half; i++)
                    right[i] = input[i + half];
                MergeSort(left);
                MergeSort(right);

                int p1 = 0, p2 = 0;
                for (int i = 0; i < input.Length; i++)
                {
                    if (p1 == left.Length)
                        input[i] = right[p2++];
                    else if (p2 == right.Length)
                        input[i] = left[p1++];
                    else if (left[p1].CompareTo(right[p2]) < 0)
                        input[i] = left[p1++];
                    else
                        input[i] = right[p2++];
                }
            }
            
            return;
        }
    }
}