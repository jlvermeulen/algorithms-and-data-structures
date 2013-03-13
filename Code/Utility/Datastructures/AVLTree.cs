using System;
using System.Collections;
using System.Collections.Generic;

public class AVLTree<T> : ICollection<T>
    where T : IComparable<T>
{
    private AVLNode root;

    public AVLTree() { }

    public AVLTree(IEnumerable<T> data)
    {
        foreach (T t in data)
            this.Add(t);
    }

    public void Add(T element)
    {
        this.Count++;
        AVLNode node = new AVLNode(element);
        if (this.root == null)
        {
            this.root = node;
            return;
        }
        AVLNode current = this.root;
        while (current != null)
        {
            if (element.CompareTo(current.Value) < 0)
            {
                if (current.Left != null)
                    current = current.Left;
                else
                {
                    current.Left = node;
                    break;
                }
            }
            else if (element.CompareTo(current.Value) > 0)
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
                break;
            }
        }
        node.Parent = current;
        this.Rebalance(node.Parent);
    }

    public bool Remove(T element)
    {
        AVLNode current = this.root;
        while (current != null)
        {
            if (element.CompareTo(current.Value) < 0)
                current = current.Left;
            else if (element.CompareTo(current.Value) > 0)
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
            AVLNode replace = this.Predecessor(current);
            if (replace == null)
                replace = this.Successor(current);

            current.Value = replace.Value;
            current.Count = replace.Count;

            this.Delete(replace);
        }

        this.Count--;
        return true;
    }

    public bool Contains(T element)
    {
        return this.Find(element) != null;
    }

    public void Clear()
    {
        this.root = null;
    }

    public void CopyTo(T[] array, int arrayIndex)
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

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return new AVLEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new AVLEnumerator(this);
    }

    public int Count { get; private set; }

    public bool IsReadOnly { get { return false; } }

    public bool Successor(T element, out T result)
    {
        result = default(T);
        AVLNode successor = this.Successor(this.Find(element));
        if (successor == null)
            return false;
        result = successor.Value;
        return true;
    }

    public bool Predecessor(T element, out T result)
    {
        result = default(T);
        AVLNode predecessor = this.Predecessor(this.Find(element));
        if (predecessor == null)
            return false;
        result = predecessor.Value;
        return true;
    }

    private bool Find(T element, out T result)
    {
        result = default(T);
        AVLNode current = this.root;
        bool found = false;

        while (current != null)
        {
            int c = element.CompareTo(current.Value);
            if (c < 0)
                current = current.Left;
            else if (c > 0)
                current = current.Right;
            else
            {
                found = true;
                result = current.Value;
                break;
            }
        }

        return found;
    }

    private AVLNode Find(T element)
    {
        AVLNode current = this.root;
        while (current != null)
        {
            int c = element.CompareTo(current.Value);
            if (c < 0)
                current = current.Left;
            else if (c > 0)
                current = current.Right;
            else
                break;
        }

        return current;
    }

    private void Delete(AVLNode current)
    {
        AVLNode move = current.Right;
        if (move == null)
            move = current.Left;

        if (current.Parent.Left == current)
            current.Parent.Left = move;
        else
            current.Parent.Right = move;
        if (move != null)
            move.Parent = current.Parent;

        this.Rebalance(current.Parent);
    }

    private AVLNode Successor(AVLNode element)
    {
        if (element.Right == null)
        {
            AVLNode previous;
            while (element.Parent != null)
            {
                previous = element;
                element = element.Parent;
                if (element.Left == previous)
                    return element;
            }
            return null;
        }

        element = element.Right;
        while (element.Left != null)
            element = element.Left;

        return element;
    }

    private AVLNode Predecessor(AVLNode element)
    {
        if (element.Left == null)
        {
            AVLNode previous;
            while (element.Parent != null)
            {
                previous = element;
                element = element.Parent;
                if (element.Right == previous)
                    return element;
            }
            return null;
        }

        element = element.Left;
        while (element.Right != null)
            element = element.Right;

        return element;
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

                if (diffC == -1)
                    this.RotateLeft(start);
                else if (diffC == 1)
                {
                    this.RotateRight(start.Right);
                    this.RotateLeft(start);
                }

                start = start.Parent.Parent;
                continue;
            }
            else if (diff == 2)
            {
                leftC = start.Left.Left != null ? start.Left.Left.Height : -1;
                rightC = start.Left.Right != null ? start.Left.Right.Height : -1;
                diffC = leftC - rightC;

                if (diffC == 1)
                    this.RotateRight(start);
                else if (diffC == -1)
                {
                    this.RotateLeft(start.Left);
                    this.RotateRight(start);
                }

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

    private void RotateRight(AVLNode start)
    {
        AVLNode rightC = start.Left.Right;
        if (start.Parent == null)
            root = start.Left;
        else if (start.Value.CompareTo(start.Parent.Value) < 0)
            start.Parent.Left = start.Left;
        else
            start.Parent.Right = start.Left;
        start.Left.Parent = start.Parent;
        start.Parent = start.Left;
        start.Left.Right = start;
        start.Left = rightC;
        if (rightC != null)
            rightC.Parent = start;

        int left, right;
        left = start.Left != null ? start.Left.Height : -1;
        right = start.Right != null ? start.Right.Height : -1;
        start.Height = Math.Max(left, right) + 1;

        left = start.Parent.Left != null ? start.Parent.Left.Height : -1;
        right = start.Height;
        start.Parent.Height = Math.Max(left, right) + 1;
    }

    private void RotateLeft(AVLNode start)
    {
        AVLNode leftC = start.Right.Left;
        if (start.Parent == null)
            root = start.Right;
        else if (start.Value.CompareTo(start.Parent.Value) < 0)
            start.Parent.Left = start.Right;
        else
            start.Parent.Right = start.Right;
        start.Right.Parent = start.Parent;
        start.Parent = start.Right;
        start.Right.Left = start;
        start.Right = leftC;
        if (leftC != null)
            leftC.Parent = start;

        int left, right;
        left = start.Left != null ? start.Left.Height : -1;
        right = start.Right != null ? start.Right.Height : -1;
        start.Height = Math.Max(left, right) + 1;

        left = start.Height;
        right = start.Parent.Right != null ? start.Parent.Right.Height : -1;
        start.Parent.Height = Math.Max(left, right) + 1;
    }

    private class AVLNode
    {
        public AVLNode(T value)
        {
            this.Value = value;
            this.Height = 0;
            this.Count = 1;
            this.Parent = null;
            this.Left = null;
            this.Right = null;
        }

        public int Height { get; set; }
        public int Count { get; set; }
        public T Value { get; set; }
        public AVLNode Parent { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
    }

    private class AVLEnumerator : IEnumerator<T>
    {
        private AVLTree<T> tree;
        private AVLNode currentNode;
        private int currentCount;

        public AVLEnumerator(AVLTree<T> tree)
        {
            this.tree = tree;
            this.currentNode = null;
            this.currentCount = 1;
        }

        public bool MoveNext()
        {
            AVLNode successor;
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