using System;
using System.Collections.Generic;

namespace Utility
{
    namespace DataStructures
    {
        namespace BinarySearchTree
        {
            /// <summary>
            /// Represents a balanced binary search tree.
            /// </summary>
            /// <typeparam name="T">The type of the values in the tree.</typeparam>
            public class AVLTree<T> : BinarySearchTree<T>
                where T : IComparable<T>
            {
                /// <summary>
                /// Initializes a new instance of the AVLTree&lt;T> class that is empty.
                /// </summary>
                public AVLTree() { }

                /// <summary>
                /// Initializes a new instance of the AVLTree&lt;T> class that contains elements copied from the specified IEnumerable&lt;T>.
                /// </summary>
                /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new AVLTree&lt;T>.</param>
                public AVLTree(IEnumerable<T> collection) : base(collection) { }

                /// <summary>
                /// Adds the specified value to the AVLTree&lt;T>.
                /// </summary>
                /// <param name="item">The value to add.</param>
                public override void Add(T item)
                {
                    AVLNode node = new AVLNode(item);
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

                protected class AVLNode : ValueTreeNode
                {
                    public AVLNode(T value) : base(value) { this.Height = 0; }

                    public int Height { get; set; }
                    public new AVLNode Parent { get { return (AVLNode)base.Parent; } set { base.Parent = value; } }
                    public new AVLNode Left { get { return (AVLNode)base.Left; } set { base.Left = value; } }
                    public new AVLNode Right { get { return (AVLNode)base.Right; } set { base.Right = value; } }
                }
            }
        }
    }
}