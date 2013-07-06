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
                /// Performs an interpolation search for <paramref name="item"/> in <paramref name="array"/>.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <param name="interpolator">The class that defines how to interpolate between values of type <typeparamref name="T"/>.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int InterpolationSearch(T[] array, T item, IInterpolator<T> interpolator)
                {
                    return InterpolationSearch(array, item, 0, array.Length - 1, interpolator.Interpolate);
                }

                /// <summary>
                /// Performs an interpolation search for <paramref name="item"/> in <paramref name="array"/>.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <param name="interpolate">A function that defines how to interpolate between values of type <typeparamref name="T"/>.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int InterpolationSearch(T[] array, T item, Interpolation<T> interpolate)
                {
                    return InterpolationSearch(array, item, 0, array.Length - 1, interpolate);
                }

                /// <summary>
                /// Performs an interpolation search for <paramref name="item"/> in <paramref name="array"/>.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <param name="start">The lowest index to look at.</param>
                /// <param name="end">The highest index to look at.</param>
                /// <param name="interpolator">The class that defines how to interpolate between values of type <typeparamref name="T"/>.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int InterpolationSearch(T[] array, T item, int start, int end, IInterpolator<T> interpolator)
                {
                    return InterpolationSearch(array, item, 0, array.Length - 1, interpolator.Interpolate);
                }

                /// <summary>
                /// Performs an interpolation search for <paramref name="item"/> in <paramref name="array"/>.
                /// </summary>
                /// <param name="array">The array to search.</param>
                /// <param name="item">The item to search for.</param>
                /// <param name="start">The lowest index to look at.</param>
                /// <param name="end">The highest index to look at.</param>
                /// <param name="interpolate">A function that defines how to interpolate between values of type <typeparamref name="T"/>.</param>
                /// <returns>An integer indicating the index of the largest element that is smaller than or equal to <paramref name="item"/>.</returns>
                public static int InterpolationSearch(T[] array, T item, int start, int end, Interpolation<T> interpolate)
                {
                    int guess;
                    T low;
                    while (end > start + 1)
                    {
                        low = array[start];
                        guess = (int)((start + 1) + (end - start - 1) * interpolate(item, low, array[end]));

                        if (array[guess].CompareTo(item) <= 0)
                            start = guess;
                        else
                            end = guess;
                    }

                    if (start == 0 && item.CompareTo(array[start]) < 0)
                        return -1;
                    if (start == array.Length - 1 && item.CompareTo(array[start]) > 0)
                        return array.Length;

                    return start;
                }
            }
        }
    }
}