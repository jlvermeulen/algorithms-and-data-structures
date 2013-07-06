using System;
using System.Collections.Generic;

namespace Utility
{
    namespace DataStructures
    {
        namespace DisjointSet
        {
            /// <summary>
            /// A class for performing disjoint-set operations.
            /// </summary>
            /// <typeparam name="T">The type of values in the sets.</typeparam>
            public class UnionFind<T>
            {
                private Dictionary<T, UnionNode> nodes = new Dictionary<T, UnionNode>();

                /// <summary>
                /// Makes a new set containing only the specified value.
                /// </summary>
                /// <param name="value">The value to store in the new set.</param>
                public void Make(T value)
                {
                    if (nodes.ContainsKey(value))
                        throw new ArgumentException("That value has already been added.", "value");

                    UnionNode node = new UnionNode(value, 0);
                    nodes[value] = node;
                }

                /// <summary>
                /// Finds the representative of a value and applies path compression.
                /// </summary>
                /// <param name="value">The value of which the representative is to be found.</param>
                /// <returns>The representative of the given node.</returns>
                public T Find(T value)
                {
                    UnionNode n;
                    if (!nodes.TryGetValue(value, out n))
                        throw new ArgumentException("There is no set containing that value.", "node");

                    return this.Find(n).Value;
                }

                /// <summary>
                /// Performs a union by rank on the two sets that <paramref name="node1"/> and <paramref name="node2"/> are in.
                /// </summary>
                /// <param name="node1">A value in the set to perform a union on.</param>
                /// <param name="node2">A value in the set to perform a union on.</param>
                public void Union(T node1, T node2)
                {
                    UnionNode a, b;
                    if (!nodes.TryGetValue(node1, out a))
                        throw new ArgumentException("There is no set containing that value.", "node1");
                    if (!nodes.TryGetValue(node2, out b))
                        throw new ArgumentException("There is no set containing that value.", "node2");

                    this.Union(a, b);
                }

                private UnionNode Find(UnionNode node)
                {
                    if (node.Parent != node)
                        node.Parent = Find(node.Parent);
                    return node.Parent;
                }

                private void Union(UnionNode node1, UnionNode node2)
                {
                    UnionNode root1 = Find(node1), root2 = Find(node2);
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

                /// <summary>
                /// Stores items and their parent pointers for use in the UnionFind&lt;T> methods.
                /// </summary>
                /// <typeparam name="T">The type of elements in the sets.</typeparam>
                private class UnionNode
                {
                    public UnionNode(T value, uint rank)
                    {
                        this.Value = value;
                        this.Parent = this;
                        this.Rank = rank;
                    }

                    /// <summary>
                    /// The value stored in this UnionNode&lt;T>.
                    /// </summary>
                    public T Value { get; set; }
                    /// <summary>
                    /// The representative of this UnionNode&lt;T>.
                    /// </summary>
                    public UnionNode Parent { get; set; }
                    /// <summary>
                    /// The rank of this set. Only accurate if this UnionNode&lt;T> is the representative of its set.
                    /// </summary>
                    public uint Rank { get; set; }
                }
            }
        }
    }
}