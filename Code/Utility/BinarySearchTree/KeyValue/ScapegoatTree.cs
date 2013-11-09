using System;
using System.Collections.Generic;

namespace Utility
{
    namespace DataStructures
    {
        namespace BinarySearchTree
        {
            /// <summary>
            /// Represents a dictionary implemented by a balanced binary search tree.
            /// </summary>
            /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
            /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
            public class ScapegoatTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
                where TKey : IComparable<TKey>
            {
                private float alpha;
                private int size, maxSize;

                /// <summary>
                /// Initializes a new instance of the ScapeGoatTree&lt;TKey, TValue> class that is empty.
                /// </summary>
                /// <param name="alpha">The weight factor that is used when balancing the tree.</param>
                public ScapegoatTree(float alpha = 0.75f)
                {
                    this.alpha = alpha;
                    this.size = 0;
                    this.maxSize = 0;
                }

                /// <summary>
                /// Initializes a new instance of the ScapeGoatTree&lt;TKey, TValue> class that contains elements copied from the specified IDictionary&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose elements are copied to the new ScapeGoatTree&lt;TKey, TValue>.</param>
                /// <param name="alpha">The weight factor that is used when balancing the tree.</param>
                public ScapegoatTree(IDictionary<TKey, TValue> collection, float alpha = 0.75f)
                    : base()
                {
                    this.alpha = alpha;
                    this.size = 0;
                    this.maxSize = 0;
                    foreach (KeyValuePair<TKey, TValue> t in collection)
                        this.Add(t.Key, t.Value);
                }

                protected override KeyValueTreeNode Add(KeyValueTreeNode node)
                {
                    int depth = 0;
                    this.Count++;
                    if (this.root == null)
                    {
                        this.root = node;
                        this.size++;
                        return node;
                    }
                    KeyValueTreeNode current = (KeyValueTreeNode)this.root;
                    while (current != null)
                    {
                        depth++;
                        int c = node.Key.CompareTo(current.Key);
                        if (c < 0)
                        {
                            if (current.Left != null)
                                current = current.Left;
                            else
                            {
                                current.Left = node;
                                break;
                            }
                        }
                        else if (c > 0)
                        {
                            if (current.Right != null)
                                current = current.Right;
                            else
                            {
                                current.Right = node;
                                break;
                            }
                        }
                        else
                            return null;
                    }
                    this.size++;
                    this.maxSize = Math.Max(size, maxSize);
                    node.Parent = current;

                    if (depth > Math.Log(this.size, 1 / alpha))
                        this.Rebalance(node);

                    return node;
                }

                /// <summary>
                /// Removes the value with the specified key from the ScapeGoatTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to remove.</param>
                /// <returns>true if the element is successfully found and removed; otherwise, false.</returns>
                public override bool Remove(TKey key)
                {
                    KeyValueTreeNode current = this.Find(key);

                    if (current == null)
                        return false;

                    if (current.Count > 1)
                    {
                        current.Count--;
                        this.Count--;
                        return true;
                    }

                    if (current.Left == null || current.Right == null)
                        this.Delete(current);
                    else
                    {
                        KeyValueTreeNode replace = (KeyValueTreeNode)this.Predecessor(current);
                        if (replace == null)
                            replace = (KeyValueTreeNode)this.Successor(current);

                        current.Value = replace.Value;
                        current.Count = replace.Count;

                        this.Delete(replace);
                    }

                    this.Count--;
                    this.size--;

                    if (this.size <= this.maxSize / 2)
                    {
                        this.Flatten((KeyValueTreeNode)this.root, this.size);
                        this.maxSize = this.size;
                    }

                    return true;
                }

                private void Rebalance(KeyValueTreeNode node)
                {
                    int size = 1, leftSize, rightSize;
                    bool leftC;
                    while (node.Parent != null)
                    {
                        leftC = node.Parent.Left == node;
                        node = node.Parent;
                        if (leftC)
                        {
                            rightSize = this.Size(node.Right);
                            leftSize = size;
                        }
                        else
                        {
                            leftSize = this.Size(node.Left);
                            rightSize = size;
                        }
                        size = leftSize + rightSize + 1;

                        if (leftSize > alpha * size || rightSize > alpha * size)
                        {
                            this.Flatten(node, size);
                            return;
                        }
                    }
                }

                private void Flatten(KeyValueTreeNode node, int size)
                {
                    if (node == null)
                        return;

                    KeyValueTreeNode[] nodes = new KeyValueTreeNode[size];
                    int index = 0;
                    this.Flatten(node, nodes, ref index);
                    this.InsertMedians(nodes, node.Parent);
                }

                private void Flatten(KeyValueTreeNode node, KeyValueTreeNode[] array, ref int index)
                {
                    if (node == null)
                        return;
                    this.Flatten(node.Left, array, ref index);
                    array[index++] = node;
                    this.Flatten(node.Right, array, ref index);
                }

                private void InsertMedians(KeyValueTreeNode[] array, KeyValueTreeNode root)
                {
                    int median = array.Length / 2;
                    KeyValueTreeNode medianNode = new KeyValueTreeNode(array[median].Key, array[median].Value);
                    medianNode.Parent = root;
                    if (root == null)
                        this.root = medianNode;
                    else if (medianNode.Key.CompareTo(root.Key) < 0)
                        root.Left = medianNode;
                    else
                        root.Right = medianNode;
                    this.InsertMedians(array, medianNode, 0, median, true);
                    this.InsertMedians(array, medianNode, median + 1, array.Length, false);
                }

                private void InsertMedians(KeyValueTreeNode[] array, KeyValueTreeNode root, int start, int end, bool left)
                {
                    if (end == start)
                        return;
                    int median = (end - start) / 2 + start;
                    KeyValueTreeNode node = new KeyValueTreeNode(array[median].Key, array[median].Value);
                    this.AddAt(node, root, left);
                    this.InsertMedians(array, node, start, median, true);
                    this.InsertMedians(array, node, median + 1, end, false);
                }

                private void AddAt(KeyValueTreeNode node, KeyValueTreeNode root, bool left)
                {
                    if (left)
                        root.Left = node;
                    else
                        root.Right = node;
                    node.Parent = root;
                }

                private int Size(KeyValueTreeNode node)
                {
                    if (node == null)
                        return 0;
                    return this.Size(node.Left) + this.Size(node.Right) + 1;
                }

                public override void Clear() { base.Clear(); this.size = 0; }
            }
        }
    }
}