using System;
using System.Collections;
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
            public class RedBlackTree<TKey, TValue> : BinarySearchTree<TKey, TValue>, IEnumerable<KeyValuePair<TKey, TValue>>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initializes a new instance of the RedBlackTree&lt;TKey, TValue> class that is empty.
                /// </summary>
                public RedBlackTree() { }

                /// <summary>
                /// Initializes a new instance of the RedBlackTree&lt;TKey, TValue> class that contains elements copied from the specified IDictionary&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose elements are copied to the new RedBlackTree&lt;TKey, TValue>.</param>
                public RedBlackTree(IDictionary<TKey, TValue> collection) : base(collection) { }

                /// <summary>
                /// Adds the specified key and value to the RedBlackTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to add.</param>
                /// <param name="value">The value of the element to add.</param>
                public override void Add(TKey key, TValue value)
                {
                    RBNode node = new RBNode(key, value);
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
                        int c = node.Key.CompareTo(current.Key);
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
                            throw new ArgumentException("An item with the same key already exists in the collection.");
                    }
                    node.Parent = current;
                    this.InsertRebalance(node);
                }

                /// <summary>
                /// Removes the value with the specified key from the RedBlackTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to remove.</param>
                /// <returns>true if the element is successfully found and removed; otherwise, false.</returns>
                public override bool Remove(TKey key)
                {
                    RBNode current = (RBNode)this.root;
                    while (!current.IsLeaf)
                    {
                        int c = key.CompareTo(current.Key);
                        if (c < 0)
                            current = current.Left;
                        else if (c > 0)
                            current = current.Right;
                        else
                            break;
                    }

                    if (current.IsLeaf)
                        return false;

                    if (current.Left.IsLeaf || current.Right.IsLeaf)
                        this.Delete(current);
                    else
                    {
                        RBNode replace = (RBNode)this.Predecessor(current);
                        if (replace == null)
                            replace = (RBNode)this.Successor(current);

                        current.Value = replace.Value;
                        this.Delete(replace);
                    }

                    this.Count--;
                    return true;
                }

                protected override KeyValueTreeNode Find(TKey key)
                {
                    RBNode node = (RBNode)base.Find(key);
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
                IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return new RBEnumerator(this); }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
                IEnumerator IEnumerable.GetEnumerator() { return new RBEnumerator(this); }

                protected class RBNode : KeyValueTreeNode
                {
                    public RBNode()
                        : base(default(TKey), default(TValue))
                    {
                        this.Black = true;
                    }

                    public RBNode(TKey key, TValue value)
                        : base(key, value)
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

                private class RBEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
                {
                    private RedBlackTree<TKey, TValue> tree;
                    private RBNode currentNode;

                    public RBEnumerator(RedBlackTree<TKey, TValue> tree)
                    {
                        this.tree = tree;
                        this.currentNode = null;
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

                        if ((successor = (RBNode)tree.Successor(currentNode)) != null)
                        {
                            currentNode = successor;
                            return true;
                        }

                        return false;
                    }

                    public void Reset() { this.currentNode = null; }

                    void IDisposable.Dispose() { }

                    public KeyValuePair<TKey, TValue> Current { get { return currentNode.KeyValuePair; } }

                    object IEnumerator.Current { get { return currentNode.KeyValuePair; } }
                }
            }
        }
    }
}