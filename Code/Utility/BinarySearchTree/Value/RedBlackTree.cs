using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    /// <summary>
    /// Represents a balanced binary search tree.
    /// </summary>
    /// <typeparam name="T">The type of the values in the tree.</typeparam>
    public class RedBlackTree<T> : BinarySearchTree<T>, IEnumerable<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Initializes a new instance of the RedBlackTree&lt;T> class that is empty.
        /// </summary>
        public RedBlackTree() { }

        /// <summary>
        /// Initializes a new instance of the RedBlackTree&lt;T> class that contains elements copied from the specified IEnumerable&lt;T>.
        /// </summary>
        /// <param name="collection">The IEnumerable&lt;T> whose elements are copied to the new RedBlackTree&lt;T>.</param>
        public RedBlackTree(IEnumerable<T> collection) : base(collection) { }

        /// <summary>
        /// Adds the specified value to the RedBlackTree&lt;T>.
        /// </summary>
        /// <param name="item">The value to add.</param>
        public override void Add(T item)
        {
            RBNode node = new RBNode(item);
            this.Count++;
            if (this.root == null)
            {
                this.root = node;
                node.Black = true;
                return;
            }
            RBNode current = (RBNode)this.root;
            while (!current.IsLeaf)
            {
                int c = node.Value.CompareTo(current.Value);
                if (c < 0)
                {
                    if (!current.Left.IsLeaf)
                        current = current.Left;
                    else
                    {
                        current.Left = node;
                        break;
                    }
                }
                else if (c > 0)
                {
                    if (!current.Right.IsLeaf)
                        current = current.Right;
                    else
                    {
                        current.Right = node;
                        break;
                    }
                }
                else
                {
                    current.Count++;
                    return;
                }
            }
            node.Parent = current;
            this.InsertRebalance(node);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the RedBlackTree&lt;T>.
        /// </summary>
        /// <param name="item">The object to be removed.</param>
        /// <returns>true if item was successfully removed from the RedBlackTree&lt;T>; otherwise, false.</returns>
        public override bool Remove(T item)
        {
            RBNode current = (RBNode)this.root;
            while (!current.IsLeaf)
            {
                int c = item.CompareTo(current.Value);
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            if (current.IsLeaf)
                return false;

            if (current.Count > 1)
            {
                current.Count--;
                this.Count--;
                return true;
            }

            if (current.Left.IsLeaf || current.Right.IsLeaf)
                this.Delete(current);
            else
            {
                RBNode replace = (RBNode)this.Predecessor(current);
                if (replace == null)
                    replace = (RBNode)this.Successor(current);

                current.Value = replace.Value;
                current.Count = replace.Count;

                this.Delete(replace);
            }

            this.Count--;
            return true;
        }

        protected override ValueTreeNode Find(T item)
        {
            RBNode node = (RBNode)base.Find(item);
            if (node.IsLeaf)
                return null;
            return node;
        }

        protected override void Delete(TreeNode node)
        {
            RBNode n = (RBNode)node;

            if (!n.Black)
            {
                base.Delete(node);
                return;
            }

            bool isLeftChild = false;
            RBNode child = n.Right;
            if (child.IsLeaf)
            {
                child = n.Left;
                isLeftChild = true;
            }

            if (!child.Black)
            {
                base.Delete(node);
                if (isLeftChild)
                    n.Left.Black = true;
                else
                    n.Right.Black = true;
                return;
            }

            if (n.Parent.Left == n)
                n.Parent.Left = child;
            else
                n.Parent.Right = child;
            if (child != null)
                child.Parent = n.Parent;

            this.DeleteRebalance(n);
        }

        private void Delete(RBNode current)
        {
            RBNode move = current.Right;
            if (move.IsLeaf)
                move = current.Left;

            if (current.Parent == null)
                root = move;
            else if (current.Parent.Left == current)
                current.Parent.Left = move;
            else
                current.Parent.Right = move;
            move.Parent = current.Parent;
        }

        private void InsertRebalance(RBNode start)
        {
            if (start.Parent == null && !start.Black)
            {
                start.Black = true;
                return;
            }

            if (start.Parent.Black)
                return;

            if (!start.Parent.Black && start.Uncle != null && !start.Uncle.Black)
            {
                start.Parent.Black = true;
                start.Uncle.Black = true;
                start.GrandParent.Black = false;
                this.InsertRebalance(start.GrandParent);
                return;
            }

            if (!start.Parent.Black && (start.Uncle == null || start.Uncle.Black))
            {
                if (start == start.Parent.Right && start.Parent == start.GrandParent.Left)
                {
                    this.RotateLeft(start.Parent);
                    start = start.Left;
                }
                else if (start == start.Parent.Left && start.Parent == start.GrandParent.Right)
                {
                    this.RotateRight(start.Parent);
                    start = start.Right;
                }
            }

            if (!start.Parent.Black && (start.Uncle == null || start.Uncle.Black))
            {
                if (start == start.Parent.Left && start.Parent == start.GrandParent.Left)
                {
                    this.RotateRight(start.GrandParent);
                    start.Parent.Black = true;
                    start.Parent.Right.Black = false;
                }
                else if (start == start.Parent.Right && start.Parent == start.GrandParent.Right)
                {
                    this.RotateLeft(start.GrandParent);
                    start.Parent.Black = true;
                    start.Parent.Left.Black = false;
                }
            }
        }

        private void DeleteRebalance(RBNode start)
        {
            if (start.Parent == null)
                return;

            bool isLeftChild = start.Parent.Left == start;
            RBNode s = start.Sibling;

            if (!s.Black)
            {
                start.Parent.Black = false;
                s.Black = true;
                if (isLeftChild)
                    this.RotateLeft(start.Parent);
                else
                    this.RotateRight(start.Parent);
            }
            else if ((s.Left == null || s.Left.Black) && (s.Right == null || s.Right.Black))
            {
                s.Black = false;
                this.DeleteRebalance(start.Parent);
                return;
            }

            s = start.Sibling;

            if (!start.Parent.Black && s.Black && (s.Left == null || s.Left.Black) && (s.Right == null || s.Right.Black))
            {
                start.Parent.Black = true;
                s.Black = false;
                return;
            }

            if (s.Black)
            {
                if (isLeftChild && s.Left != null && !s.Left.Black && (s.Right == null || s.Right.Black))
                {
                    this.RotateRight(s);
                    s.Parent.Black = true;
                    s.Black = false;
                }
                else if (!isLeftChild && s.Right != null && !s.Right.Black && (s.Left == null || s.Left.Black))
                {
                    this.RotateLeft(s);
                    s.Parent.Black = true;
                    s.Black = false;
                }
            }

            s = start.Sibling;

            s.Black = s.Parent.Black;
            start.Parent.Black = true;

            if (isLeftChild)
            {
                s.Right.Black = true;
                this.RotateLeft(start.Parent);
            }
            else
            {
                s.Left.Black = true;
                this.RotateRight(start.Parent);
            }
        }

        protected override TreeNode Successor(TreeNode item)
        {
            RBNode node = (RBNode)item;
            if (node.IsLeaf || node.Right.IsLeaf)
            {
                RBNode previous;
                while (node.Parent != null)
                {
                    previous = node;
                    node = node.Parent;
                    if (node.Left == previous)
                        return node;
                }
                return null;
            }

            node = node.Right;
            while (!node.Left.IsLeaf)
                node = node.Left;

            return node;
        }

        protected override TreeNode Predecessor(TreeNode item)
        {
            RBNode node = (RBNode)item;
            if (node.IsLeaf || node.Left.IsLeaf)
            {
                RBNode previous;
                while (node.Parent != null)
                {
                    previous = node;
                    node = node.Parent;
                    if (node.Right == previous)
                        return item;
                }
                return null;
            }

            node = node.Left;
            while (!node.Right.IsLeaf)
                node = node.Right;

            return node;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator&lt;T> that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new RBEnumerator(this); }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() { return new RBEnumerator(this); }

        protected class RBNode : ValueTreeNode
        {
            public RBNode()
                : base(default(T))
            {
                this.Black = true;
            }

            public RBNode(T value)
                : base(value)
            {
                this.Black = false;
                this.Left = new RBNode();
                this.Left.Parent = this;
                this.Right = new RBNode();
                this.Right.Parent = this;
            }

            public bool Black { get; set; }
            public new RBNode Parent { get { return (RBNode)base.Parent; } set { base.Parent = value; } }
            public new RBNode Left { get { return (RBNode)base.Left; } set { base.Left = value; } }
            public new RBNode Right { get { return (RBNode)base.Right; } set { base.Right = value; } }

            public RBNode GrandParent
            {
                get
                {
                    if (this.Parent == null)
                        return null;
                    return this.Parent.Parent;
                }
            }

            public RBNode Uncle
            {
                get
                {
                    RBNode gp = this.GrandParent;
                    if (gp == null)
                        return null;
                    if (this.Parent == gp.Left)
                        return gp.Right;
                    return gp.Left;
                }
            }

            public RBNode Sibling
            {
                get
                {
                    if (this.Parent == null)
                        return null;
                    if (this.Parent.Left == this)
                        return this.Parent.Right;
                    return this.Parent.Left;
                }
            }

            public bool IsLeaf { get { return this.Left == null; } }
        }

        private class RBEnumerator : IEnumerator<T>
        {
            private RedBlackTree<T> tree;
            private RBNode currentNode;
            private int currentCount;

            public RBEnumerator(RedBlackTree<T> tree)
            {
                this.tree = tree;
                this.currentNode = null;
                this.currentCount = 1;
            }

            public bool MoveNext()
            {
                RBNode successor;
                if (tree.root == null)
                    return false;
                if (currentNode == null)
                {
                    currentNode = (RBNode)tree.root;
                    while (!currentNode.Left.IsLeaf)
                        currentNode = currentNode.Left;
                    return true;
                }

                if (currentNode.Count > currentCount)
                {
                    currentCount++;
                    return true;
                }

                if ((successor = (RBNode)tree.Successor(currentNode)) != null)
                {
                    currentNode = successor;
                    currentCount = 1;
                    return true;
                }

                return false;
            }

            public void Reset()
            {
                this.currentNode = null;
                this.currentCount = 1;
            }

            void IDisposable.Dispose() { }

            public T Current { get { return currentNode.Value; } }

            object IEnumerator.Current { get { return currentNode.Value; } }
        }
    }
}