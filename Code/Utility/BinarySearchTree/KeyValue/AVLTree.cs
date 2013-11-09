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
            public class AVLTree<TKey, TValue> : BinarySearchTree<TKey, TValue>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initializes a new instance of the AVLTree&lt;TKey, TValue> class that is empty.
                /// </summary>
                public AVLTree() { }

                /// <summary>
                /// Initializes a new instance of the AVLTree&lt;TKey, TValue> class that contains elements copied from the specified IDictionary&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose elements are copied to the new AVLTree&lt;TKey, TValue>.</param>
                public AVLTree(IDictionary<TKey, TValue> collection) : base(collection) { }

                /// <summary>
                /// Adds the specified key and value to the AVLTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to add.</param>
                /// <param name="value">The value of the element to add.</param>
                public override void Add(TKey key, TValue value)
                {
                    AVLNode node = new AVLNode(key, value);
                    base.Add(node);
                    this.Rebalance(node.Parent);
                }

                protected override void Delete(TreeNode current)
                {
                    base.Delete(current);
                    this.Rebalance((AVLNode)current.Parent);
                }

                private void Rebalance(AVLNode start)
                {
                    int height, left, right, diff, leftC, rightC, diffC;
                    while (start != null)
                    {
                        height = start.Height;
                        left = start.Left != null ? start.Left.Height : -1;
                        right = start.Right != null ? start.Right.Height : -1;
                        diff = left - right;

                        if (diff == -2)
                        {
                            leftC = start.Right.Left != null ? start.Right.Left.Height : -1;
                            rightC = start.Right.Right != null ? start.Right.Right.Height : -1;
                            diffC = leftC - rightC;

                            if (diffC == 1)
                                this.RotateRight(start.Right);
                            this.RotateLeft(start);

                            start = start.Parent.Parent;
                            continue;
                        }
                        else if (diff == 2)
                        {
                            leftC = start.Left.Left != null ? start.Left.Left.Height : -1;
                            rightC = start.Left.Right != null ? start.Left.Right.Height : -1;
                            diffC = leftC - rightC;

                            if (diffC == -1)
                                this.RotateLeft(start.Left);
                            this.RotateRight(start);

                            start = start.Parent.Parent;
                            continue;
                        }

                        start.Height = Math.Max(left, right) + 1;
                        if (height == start.Height)
                            break;
                        else
                            start = start.Parent;
                    }
                }

                protected void RotateLeft(AVLNode start)
                {
                    base.RotateLeft(start);

                    int left, right;
                    left = start.Left != null ? start.Left.Height : -1;
                    right = start.Right != null ? start.Right.Height : -1;
                    start.Height = Math.Max(left, right) + 1;

                    left = start.Height;
                    right = start.Parent.Right != null ? start.Parent.Right.Height : -1;
                    start.Parent.Height = Math.Max(left, right) + 1;
                }

                protected void RotateRight(AVLNode start)
                {
                    base.RotateRight(start);

                    int left, right;
                    left = start.Left != null ? start.Left.Height : -1;
                    right = start.Right != null ? start.Right.Height : -1;
                    start.Height = Math.Max(left, right) + 1;

                    left = start.Parent.Left != null ? start.Parent.Left.Height : -1;
                    right = start.Height;
                    start.Parent.Height = Math.Max(left, right) + 1;
                }

                protected class AVLNode : KeyValueTreeNode
                {
                    public AVLNode(TKey key, TValue value) : base(key, value) { this.Height = 0; }

                    public int Height { get; set; }
                    public new AVLNode Parent { get { return (AVLNode)base.Parent; } set { base.Parent = value; } }
                    public new AVLNode Left { get { return (AVLNode)base.Left; } set { base.Left = value; } }
                    public new AVLNode Right { get { return (AVLNode)base.Right; } set { base.Right = value; } }
                }
            }
        }
    }
}