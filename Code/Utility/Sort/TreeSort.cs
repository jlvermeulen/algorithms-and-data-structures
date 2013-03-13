using System;

namespace Utility
{
    public static partial class Sort<T>
        where T : IComparable<T>
    {
        public static T[] TreeSort(T[] input)
        {
            AVLTree<T> tree = new AVLTree<T>(input);
            tree.CopyTo(input, 0);
            return input;
        }
    }
}