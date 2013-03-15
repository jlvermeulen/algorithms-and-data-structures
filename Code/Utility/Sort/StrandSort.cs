using System;
using System.Collections.Generic;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] StrandSort(T[] input)
        {
            List<T> sublist = new List<T>();
            int i = 0;
            while (i < input.Length - 1 && input[i].CompareTo(input[i + 1]) < 0)
                i++;
            int j = i + 1;
            while (i < input.Length - 1)
            {
                if (j < input.Length && (sublist.Count == 0 || input[j].CompareTo(sublist[sublist.Count - 1]) > 0))
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