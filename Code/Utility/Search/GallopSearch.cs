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
                public static int GallopSearch(T[] array, T item) { return GallopSearchRight(array, item, 0, array.Length); }

                public static int GallopSearchRight(T[] array, T item, int start, int end)
                {
                    int previous, off = 0;
                    do
                    {
                        previous = start;
                        off = 2 * off + 1;
                        start += off;
                        if (start >= end)
                        {
                            start = end;
                            break;
                        }
                    }
                    while (array[start].CompareTo(item) <= 0);

                    return BinarySearch(array, item, previous, start);
                }

                public static int GallopSearchLeft(T[] array, T item, int start, int end)
                {
                    int previous, off = 0;
                    do
                    {
                        previous = end;
                        off = 2 * off + 1;
                        end -= off;
                        if (end <= start)
                        {
                            end = start;
                            break;
                        }
                    }
                    while (array[end].CompareTo(item) > 0);

                    return BinarySearch(array, item, end, previous);
                }
            }
        }
    }
}