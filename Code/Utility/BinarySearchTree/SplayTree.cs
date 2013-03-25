using System;
using System.Collections.Generic;
using Utility;

public class SplayTree<T> : BinarySearchTree<T>
    where T : IComparable<T>
{
    public SplayTree() { }

    public SplayTree(IEnumerable<T> collection) : base(collection) { }

    public override void Add(T item)
    {
        TreeNode node = new TreeNode(item);
        node = base.Add(node);
        this.Splay(node);
    }

    public override bool Remove(T item)
    {
        TreeNode current = this.root, previous = null;
        while (current != null)
        {
            previous = current;
            if (item.CompareTo(current.Value) < 0)
                current = current.Left;
            else if (item.CompareTo(current.Value) > 0)
                current = current.Right;
            else
                break;
        }

        if (current == null)
        {
            this.Splay(previous);
            return false;
        }

        if (current.Count > 1)
        {
            this.Splay(current);
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

    protected override void Delete(TreeNode current)
    {
        base.Delete(current);
        this.Splay(current.Parent);
    }

    protected override TreeNode Find(T item)
    {
        TreeNode current = this.root, previous = null;
        while (current != null)
        {
            int c = item.CompareTo(current.Value);
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

    private void Splay(TreeNode start)
    {
        bool leftC, leftPC;

        if (this.root == start)
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