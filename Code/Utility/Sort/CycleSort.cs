using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] CycleSort(T[] input)
        {
            T item, temp;
            int pos;

            for (int i = 0; i < input.Length - 1; i++)
            {
                item = input[i];
                pos = i;

                for (int j = i + 1; j < input.Length; j++)
                    if (input[j].CompareTo(item) < 0)
                        pos++;

                if (pos == i)
                    continue;

                while (item.CompareTo(input[pos]) == 0)
                    pos++;

                temp = input[pos];
                input[pos] = item;
                item = temp;

                while (pos != i)
                {
                    pos = i;
                    for (int j = i + 1; j < input.Length; j++)
                        if (input[j].CompareTo(item) < 0)
                            pos++;

                    while (item.CompareTo(input[pos]) == 0)
                        pos++;

                    temp = input[pos];
                    input[pos] = item;
                    item = temp;
                }
            }

            return input;
        }
    }
}