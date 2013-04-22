using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] TreeSort(T[] input) { return TreeSort(input, 0, input.Length); }

        public static T[] TreeSort(T[] input, int start) { return TreeSort(input, start, input.Length - start); }

        public static T[] TreeSort(T[] input, int start, int length)
        {
            CheckArguments(input, start, length);

            T[] data = new T[length];
            Buffer.BlockCopy(input, start * sizeof(int), data, 0, length * sizeof(int));

            AVLTree<T> tree = new AVLTree<T>(data);
            tree.CopyTo(input, start);
            return input;
        }
    }
}