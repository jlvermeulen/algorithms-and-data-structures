using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace Search
        {
            public static partial class Search<T>
                where T : IComparable<T>
            {
                /// <summary>
                /// Performs a search for <paramref name="item"/> in <paramref name="array"/> by repeatedly looking at a random element between the upper and lower bound.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int RandomSearch(T[] array, T item)
                {
                    return RandomSearch(array, item, -1, array.Length);
                }

                /// <summary>
                /// Performs a search for <paramref name="item"/> in <paramref name="array"/> between <paramref name="start"/> and <paramref name="end"/> by repeatedly looking at a random element between the upper and lower bound.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <param name="start">The lowest index to look at.</param>
                /// <param name="end">The highest index to look at.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int RandomSearch(T[] array, T item, int start, int end)
                {
                    Random r = new Random();
                    int random;
                    while (end > start + 1)
                    {
                        random = start + 1 + r.Next(end - start - 1);
                        if (array[random].CompareTo(item) <= 0)
                            start = random;
                        else
                            end = random;
                    }
                    return start;
                }
            }
        }
    }
}