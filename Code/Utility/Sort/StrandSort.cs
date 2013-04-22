using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] StrandSort(T[] input) { return StrandSort(input, 0, input.Length); }

        public static T[] StrandSort(T[] input, int start) { return StrandSort(input, start, input.Length - start); }

        public static T[] StrandSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            List<T> sublist = new List<T>();
            int i = start;
            while (i < start + length - 1 && input[i].CompareTo(input[i + 1]) < 0)
                i++;
            int j = i + 1;
            while (i < start + length - 1)
            {
                if (j < start + length && (sublist.Count == 0 || input[j].CompareTo(sublist[sublist.Count - 1]) > 0))
                    sublist.Add(input[j++]);
                else
                {
                    int k = i, l = sublist.Count;
                    while (sublist.Count > 0)
                    {
                        if (k < 0 || input[k].CompareTo(sublist[sublist.Count - 1]) < 0)
                        {
                            input[k + sublist.Count] = sublist[sublist.Count - 1];
                            sublist.RemoveAt(sublist.Count - 1);
                        }
                        else
                        {
                            input[k + sublist.Count] = input[k];
                            k--;
                        }
                    }
                    sublist.Clear();
                    i += l;
                }
            }
            return input;
        }
    }
}