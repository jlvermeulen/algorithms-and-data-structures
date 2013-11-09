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
            public class SplayTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initializes a new instance of the SplayTree&lt;TKey, TValue> class that is empty.
                /// </summary>
                public SplayTree() { }

                /// <summary>
                /// Initializes a new instance of the SplayTree&lt;TKey, TValue> class that contains elements copied from the specified IDictionary&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose elements are copied to the new SplayTree&lt;TKey, TValue>.</param>
                public SplayTree(IDictionary<TKey, TValue> collection) : base(collection) { }

                /// <summary>
                /// Adds the specified key and value to the SplayTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to add.</param>
                /// <param name="value">The value of the element to add.</param>
                public override void Add(TKey key, TValue value)
                {
                    KeyValueTreeNode node = new KeyValueTreeNode(key, value);
                    node = base.Add(node);
                    this.Splay(node);
                }

                /// <summary>
                /// Removes the value with the specified key from the SplayTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to remove.</param>
                /// <returns>true if the element is successfully found and removed; otherwise, false.</returns>
                public override bool Remove(TKey key)
                {
                    KeyValueTreeNode current = (KeyValueTreeNode)this.root, previous = null;
                    while (current != null)
                    {
                        previous = current;
                        if (key.CompareTo(current.Key) < 0)
                            current = current.Left;
                        else if (key.CompareTo(current.Key) > 0)
                            current = current.Right;
                        else
                            break;
                    }

                    if (current == null)
                    {
                        this.Splay(previous);
                        return false;
                    }

                    if (current.Left == null || current.Right == null)
                        this.Delete(current);
                    else
                    {
                        KeyValueTreeNode replace = (KeyValueTreeNode)this.Predecessor(current);
                        if (replace == null)
                            replace = (KeyValueTreeNode)this.Successor(current);

                        current.Value = replace.Value;
                        this.Delete(replace);
                    }

                    this.Count--;
                    return true;
                }

                protected override void Delete(TreeNode current)
                {
                    base.Delete(current);
                    this.Splay((KeyValueTreeNode)current.Parent);
                }

                protected override KeyValueTreeNode Find(TKey key)
                {
                    KeyValueTreeNode current = (KeyValueTreeNode)this.root, previous = null;
                    while (current != null)
                    {
                        int c = key.CompareTo(current.Key);
                        previous = current;
                        if (c < 0)
                            current = current.Left;
                        else if (c > 0)
                            current = current.Right;
                        else
                            break;
                    }

                    this.Splay(previous);
                    return current;
                }

                private void Splay(KeyValueTreeNode start)
                {
                    bool leftC, leftPC;

                    if (this.root == start || start == null)
                        return;

                    leftC = start == start.Parent.Left;

                    if (start.Parent.Parent == null)
                    {
                        if (leftC)
                            this.RotateRight(start.Parent);
                        else
                            this.RotateLeft(start.Parent);
                        return;
                    }

                    leftPC = start.Parent == start.Parent.Parent.Left;

                    if (leftC && leftPC)
                    {
                        this.RotateRight(start.Parent.Parent);
                        this.RotateRight(start.Parent);
                    }
                    else if (!leftC && !leftPC)
                    {
                        this.RotateLeft(start.Parent.Parent);
                        this.RotateLeft(start.Parent);
                    }
                    else if (!leftC && leftPC)
                    {
                        this.RotateLeft(start.Parent);
                        this.RotateRight(start.Parent);
                    }
                    else
                    {
                        this.RotateRight(start.Parent);
                        this.RotateLeft(start.Parent);
                    }

                    this.Splay(start);
                }
            }
        }
    }
}