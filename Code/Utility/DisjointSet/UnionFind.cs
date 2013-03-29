namespace Utility
{
    /// <summary>
    /// A class for performing disjoint-set operations. Does not store the nodes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sets.</typeparam>
    public static class UnionFind<T>
    {
        /// <summary>
        /// Makes a new set containing a single node with a specified value.
        /// </summary>
        /// <param name="value">The value to store in the new set.</param>
        /// <returns>A UnionNode&lt;T> containing the specified value.</returns>
        public static UnionNode<T> Make(T value)
        {
            UnionNode<T> node = new UnionNode<T>();
            node.Value = value;
            node.Parent = node;
            node.Rank = 0;
            return node;
        }

        /// <summary>
        /// Finds the representative of a UnionNode&lt;T> and applies path compression.
        /// </summary>
        /// <param name="node">The node of which the representative is to be found.</param>
        /// <returns>The representative of the given UnionNode&lt;T>.</returns>
        public static UnionNode<T> Find(UnionNode<T> node)
        {
            if (node.Parent != node)
                node.Parent = Find(node.Parent);
            return node.Parent;
        }

        /// <summary>
        /// Performs a union by rank on the two sets that <paramref name="node1"/> and <paramref name="node2"/> are in.
        /// </summary>
        /// <param name="node1">A node in the set to perform a union on.</param>
        /// <param name="node2">A node in the set to perform a union on.</param>
        public static void Union(UnionNode<T> node1, UnionNode<T> node2)
        {
            UnionNode<T> root1 = Find(node1), root2 = Find(node2);
            if (root1 == root2)
                return;

            if (root1.Rank > root2.Rank)
                root2.Parent = root1;
            else if (root2.Rank > root1.Rank)
                root1.Parent = root2;
            else
            {
                root2.Parent = root1;
                root1.Rank++;
            }
        }
    }

    /// <summary>
    /// Stores items and their parent pointers for use in the UnionFind&lt;T> methods.
    /// </summary>
    /// <typeparam name="T">The type of elements in the sets.</typeparam>
    public class UnionNode<T>
    {
        /// <summary>
        /// The value stored in this UnionNode&lt;T>.
        /// </summary>
        public T Value { get; set; }
        /// <summary>
        /// The representative of this UnionNode&lt;T>.
        /// </summary>
        public UnionNode<T> Parent { get; set; }
        /// <summary>
        /// The rank of this set. Only accurate if this UnionNode&lt;T> is the representative of its set.
        /// </summary>
        public int Rank { get; set; }
    }
}