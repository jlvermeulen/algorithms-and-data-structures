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
                public static T[] SelectionSort(T[] input) { return SelectionSort(input, 0, input.Length); }

                public static T[] SelectionSort(T[] input, int start) { return SelectionSort(input, start, input.Length - start); }

                public static T[] SelectionSort(T[] input, int start, int length)
                {
                    CheckArguments(input, start, length);

                    int p, q;
                    T temp1, temp2;
                    bool odd = length % 2 == 1;

                    for (int i = start; i < start + length - 1; i += 2)
                    {
                        p = i;
                        q = i + 1;
                        if (input[q].CompareTo(input[p]) < 0)
                        {
                            p = i + 1;
                            q = i;
                        }
                        for (int j = i + 2; j < start + length - 1; j += 2)
                        {
                            if (input[j].CompareTo(input[j + 1]) > 0)
                            {
                                temp1 = input[j];
                                input[j] = input[j + 1];
                                input[j + 1] = temp1;
                            }
                            if (input[j].CompareTo(input[p]) < 0)
                            {
                                if (input[j + 1].CompareTo(input[p]) < 0)
                                    q = j + 1;
                                else
                                    q = p;
                                p = j;
                            }
                            else if (input[j].CompareTo(input[q]) < 0)
                                q = j;
                        }

                        if (odd)
                        {
                            if (input[start + length - 1].CompareTo(input[p]) < 0)
                            {
                                q = p;
                                p = input.Length - 1;
                            }
                            else if (input[start + length - 1].CompareTo(input[q]) < 0)
                                q = input.Length - 1;
                        }

                        temp1 = input[p];
                        temp2 = input[q];

                        if (i == q)
                            q = p;

                        input[p] = input[i];
                        input[i] = temp1;

                        input[q] = input[i + 1];
                        input[i + 1] = temp2;
                    }

                    return input;
                }
            }
        }
    }
}