using System;
using System.Collections;
using System.Collections.Generic;

namespace Utility
{
    public class BinarySearchTree<T> : ICollection<T>
        where T : IComparable<T>
    {
        protected TreeNode root;

        public BinarySearchTree() { }

        public BinarySearchTree(IEnumerable<T> data)
        {
            foreach (T t in data)
                this.Add(t);
        }

        public virtual void Add(T item)
        {
            this.Add(new TreeNode(item));
        }

        public virtual bool Remove(T item)
        {
            TreeNode current = this.root;
            while (current != null)
            {
                int c = item.CompareTo(current.Value);
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            if (current == null)
                return false;

            if (current.Count > 1)
            {
                current.Count--;
                return true;
            }

            if (current.Left == null || current.Right == null)
                this.Delete(current);
            else
            {
                TreeNode replace = this.Predecessor(current);
                if (replace == null)
                    replace = this.Successor(current);

                current.Value = replace.Value;
                current.Count = replace.Count;

                this.Delete(replace);
            }

            this.Count--;
            return true;
        }

        public virtual bool Contains(T item)
        {
            return this.Find(item) != null;
        }

        public virtual void Clear()
        {
            this.root = null;
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex");
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("The number of elements in the AVLTree is greater than the available space from arrayIndex to the end of the destination array.");

            foreach (T t in this)
                array[arrayIndex++] = t;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() { return new BSTEnumerator(this); }

        IEnumerator IEnumerable.GetEnumerator() { return new BSTEnumerator(this); }

        public virtual int Count { get; protected set; }

        public virtual bool IsReadOnly { get { return false; } }

        public virtual bool Successor(T item, out T result)
        {
            result = default(T);
            TreeNode successor = this.Successor(this.Find(item));
            if (successor == null)
                return false;
            result = successor.Value;
            return true;
        }

        public virtual bool Predecessor(T item, out T result)
        {
            result = default(T);
            TreeNode predecessor = this.Predecessor(this.Find(item));
            if (predecessor == null)
                return false;
            result = predecessor.Value;
            return true;
        }

        protected virtual TreeNode Add(TreeNode node)
        {
            this.Count++;
            if (this.root == null)
            {
                this.root = node;
                return node;
            }
            TreeNode current = this.root;
            while (current != null)
            {
                if (node.Value.CompareTo(current.Value) < 0)
                {
                    if (current.Left != null)
                        current = current.Left;
                    else
                    {
                        current.Left = node;
                        break;
                    }
                }
                else if (node.Value.CompareTo(current.Value) > 0)
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
                {
                    current.Count++;
                    return current;
                }
            }
            node.Parent = current;
            return node;
        }

        protected virtual TreeNode Find(T item)
        {
            TreeNode current = this.root;
            while (current != null)
            {
                int c = item.CompareTo(current.Value);
                if (c < 0)
                    current = current.Left;
                else if (c > 0)
                    current = current.Right;
                else
                    break;
            }

            return current;
        }

        protected virtual void Delete(TreeNode current)
        {
            TreeNode move = current.Right;
            if (move == null)
                move = current.Left;

            if (current.Parent == null)
                root = move;
            else if (current.Parent.Left == current)
                current.Parent.Left = move;
            else
                current.Parent.Right = move;
            if (move != null)
                move.Parent = current.Parent;
        }

        protected virtual void RotateLeft(TreeNode start)
        {
            TreeNode leftC = start.Right.Left;
            if (start.Parent == null)
                root = start.Right;
            else if (start == start.Parent.Left)
                start.Parent.Left = start.Right;
            else
                start.Parent.Right = start.Right;
            start.Right.Parent = start.Parent;
            start.Parent = start.Right;
            start.Right.Left = start;
            start.Right = leftC;
            if (leftC != null)
                leftC.Parent = start;
        }

        protected virtual void RotateRight(TreeNode start)
        {
            TreeNode rightC = start.Left.Right;
            if (start.Parent == null)
                root = start.Left;
            else if (start == start.Parent.Left)
                start.Parent.Left = start.Left;
            else
                start.Parent.Right = start.Left;
            start.Left.Parent = start.Parent;
            start.Parent = start.Left;
            start.Left.Right = start;
            start.Left = rightC;
            if (rightC != null)
                rightC.Parent = start;
        }

        protected virtual TreeNode Successor(TreeNode item)
        {
            if (item.Right == null)
            {
                TreeNode previous;
                while (item.Parent != null)
                {
                    previous = item;
                    item = item.Parent;
                    if (item.Left == previous)
                        return item;
                }
                return null;
            }

            item = item.Right;
            while (item.Left != null)
                item = item.Left;

            return item;
        }

        protected virtual TreeNode Predecessor(TreeNode item)
        {
            if (item.Left == null)
            {
                TreeNode previous;
                while (item.Parent != null)
                {
                    previous = item;
                    item = item.Parent;
                    if (item.Right == previous)
                        return item;
                }
                return null;
            }

            item = item.Left;
            while (item.Right != null)
                item = item.Right;

            return item;
        }

        protected class TreeNode
        {
            public TreeNode(T value)
            {
                this.Value = value;
                this.Count = 1;
            }

            public int Count { get; set; }
            public T Value { get; set; }
            public TreeNode Parent { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
        }

        private class BSTEnumerator : IEnumerator<T>
        {
            private BinarySearchTree<T> tree;
            private TreeNode currentNode;
            private int currentCount;

            public BSTEnumerator(BinarySearchTree<T> tree)
            {
                this.tree = tree;
                this.currentNode = null;
                this.currentCount = 1;
            }

            public bool MoveNext()
            {
                TreeNode successor;
                if (tree.root == null)
                    return false;
                if (currentNode == null)
                {
                    currentNode = tree.root;
                    while (currentNode.Left != null)
                        currentNode = currentNode.Left;
                    return true;
                }

                if (currentNode.Count > currentCount)
                {
                    currentCount++;
                    return true;
                }

                if ((successor = tree.Successor(currentNode)) != null)
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