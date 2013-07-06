using System;

namespace Utility
{
    namespace Algorithms
    {
        namespace Sort
        {
            public static partial class Sort<T>
                where T : IComparable<T>
            {
                private static void CheckArguments(T[] input, int start, int length)
                {
                    if (input == null)
                        throw new ArgumentNullException("input");
                    if (start < 0 || start >= input.Length)
                        throw new ArgumentOutOfRangeException("start", start, "The start argument must be non-negative and smaller than the length of the array.");
                    if (length < 0 || start + length > input.Length)
                        throw new ArgumentOutOfRangeException("length", length, "The length argument must be non-negative and the number of elements in the input array from start must be at least equal to length.");
                }
            }
        }
    }
}