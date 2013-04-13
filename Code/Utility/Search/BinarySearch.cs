using System;

namespace Utility
{
    /// <summary>
    /// Static class containing methods that search for items in sorted arrays.
    /// </summary>
    /// <typeparam name="T">The type of value to search for.</typeparam>
    public static partial class Search<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Performs a binary search for <paramref name="item"/> in <paramref name="array"/>.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
        public static int BinarySearch(T[] array, T item)
        {
            return BinarySearch(array, item, -1, array.Length);
        }

        /// <summary>
        /// Performs a binary search for <paramref name="item"/> in <paramref name="array"/> between <paramref name="start"/> and <paramref name="end"/>.
        /// </summary>
        /// <param name="array">The array to search.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="start">The lowest index to look at.</param>
        /// <param name="end">The highest index to look at.</param>
        /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
        public static int BinarySearch(T[] array, T item, int start, int end)
        {
            int half;
            while (end > start + 1)
            {
                half = (end + start) / 2;
                if (array[half].CompareTo(item) <= 0)
                    start = half;
                else
                    end = half;
            }
            return start;
        }
    }
}