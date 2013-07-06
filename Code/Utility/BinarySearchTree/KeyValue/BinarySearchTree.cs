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
            /// Represents a dictionary implemented by an unbalanced binary search tree.
            /// </summary>
            /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
            /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
            public class BinarySearchTree<TKey, TValue> : BinaryTree, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>
                where TKey : IComparable<TKey>
            {
                /// <summary>
                /// Initializes a new instance of the BinarySearchTree&lt;TKey, TValue> class that is empty.
                /// </summary>
                public BinarySearchTree() { }

                /// <summary>
                /// Initializes a new instance of the BinarySearchTree&lt;TKey, TValue> class that contains elements copied from the specified IDictionary&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose elements are copied to the new BinarySearchTree&lt;TKey, TValue>.</param>
                public BinarySearchTree(IDictionary<TKey, TValue> collection)
                {
                    foreach (KeyValuePair<TKey, TValue> t in collection)
                        this.Add(t.Key, t.Value);
                }

                /// <summary>
                /// Adds the specified key and value to the RedBlackTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to add.</param>
                /// <param name="value">The value of the element to add.</param>
                public virtual void Add(TKey key, TValue value)
                {
                    if (key == null)
                        throw new ArgumentNullException("key");
                    KeyValueTreeNode node = this.Add(new KeyValueTreeNode(key, value));
                    if (node == null)
                        throw new ArgumentException("An item with the same key already exists in the collection.");
                }

                /// <summary>
                /// Adds the elements of the specified IDictionary&lt;TKey, TValue> to the BinarySearchTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="collection">The IDictionary&lt;TKey, TValue> whose keys and values should be added to the BinarySearchTree&lt;TKey, TValue>.</param>
                public virtual void AddRange(IDictionary<TKey, TValue> collection)
                {
                    if (collection == null)
                        throw new ArgumentNullException("collection");

                    foreach (KeyValuePair<TKey, TValue> kvp in collection)
                        this.Add(kvp.Key, kvp.Value);
                }

                /// <summary>
                /// Removes the value with the specified key from the RedBlackTree&lt;TKey, TValue>.
                /// </summary>
                /// <param name="key">The key of the element to remove.</param>
                /// <returns>true if the element is successfully found and removed; otherwise, false.</returns>
                public virtual bool Remove(TKey key)
                {
                    KeyValueTreeNode current = this.Find(key);

                    if (current == null)
                        return false;

                    if (current.Left == null || current.Right == null)
                        this.Delete(current);
                    else
                    {
                        KeyValueTreeNode replace = (KeyValueTreeNode)this.Predecessor(current);
                        if (replace == null)
                            replace = (KeyValueTreeNode)this.Successor(current);

                        current.KeyValuePair = replace.KeyValuePair;
                        this.Delete(replace);
                    }

                    this.Count--;
                    return true;
                }

                /// <summary>
                /// Determines whether the BinarySearchTree&lt;TKey, TValue> contains a specific key.
                /// </summary>
                public virtual bool ContainsKey(TKey key) { return this.Find(key) != null; }

                /// <summary>
                /// Determines whether the BinarySearchTree&lt;TKey, TValue> contains a specific value.
                /// </summary>
                public virtual bool ContainsValue(TValue value)
                {
                    foreach (KeyValuePair<TKey, TValue> kvp in this)
                        if (kvp.Value.Equals(value))
                            return true;
                    return false;
                }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator&lt;T> that can be used to iterate through the collection.</returns>
                IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() { return new BSTEnumerator(this); }

                /// <summary>
                /// Returns an enumerator that iterates through the collection.
                /// </summary>
                /// <returns>An IEnumerator that can be used to iterate through the collection.</returns>
                IEnumerator IEnumerable.GetEnumerator() { return new BSTEnumerator(this); }

                protected virtual KeyValueTreeNode Add(KeyValueTreeNode node)
                {
                    this.Count++;
                    if (this.root == null)
                    {
                        this.root = node;
                        return node;
                    }
                    KeyValueTreeNode current = (KeyValueTreeNode)this.root;
                    while (current != null)
                    {
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
                    node.Parent = current;
                    return node;
                }

                protected virtual KeyValueTreeNode Find(TKey key)
                {
                    KeyValueTreeNode current = (KeyValueTreeNode)this.root;
                    while (current != null)
                    {
                        int c = key.CompareTo(current.KeyValuePair.Key);
                        if (c < 0)
                            current = current.Left;
                        else if (c > 0)
                            current = current.Right;
                        else
                            break;
                    }

                    return current;
                }

                /// <summary>
                /// Gets a collection containing the keys in the BinarySearchTree&lt;TKey, TValue>.
                /// </summary>
                public ICollection<TKey> Keys
                {
                    get
                    {
                        List<TKey> keys = new List<TKey>(this.Count);
                        foreach (KeyValuePair<TKey, TValue> kvp in this)
                            keys.Add(kvp.Key);
                        return keys;
                    }
                }

                /// <summary>
                /// Gets a collection containing the values in the BinarySearchTree&lt;TKey, TValue>.
                /// </summary>
                public ICollection<TValue> Values
                {
                    get
                    {
                        List<TValue> values = new List<TValue>(this.Count);
                        foreach (KeyValuePair<TKey, TValue> kvp in this)
                            values.Add(kvp.Value);
                        return values;
                    }
                }

                /// <summary>
                /// Gets or sets the value associated with the specified key.
                /// </summary>
                /// <param name="key">The key of the value to get or set.</param>
                /// <value>The value associated with the specified key. If the specified key is not found, a get operation throws a KeyNotFoundException, and a set operation creates a new element with the specified key.</value>
                public TValue this[TKey key]
                {
                    get
                    {
                        if (key == null)
                            throw new ArgumentNullException("key");
                        KeyValueTreeNode node = this.Find(key);
                        if (node == null)
                            throw new KeyNotFoundException("The given key was not present in the collection.");
                        return node.Value;
                    }
                    set
                    {
                        if (key == null)
                            throw new ArgumentNullException("key");
                        KeyValueTreeNode node = this.Find(key);
                        if (node == null)
                            node = this.Add(new KeyValueTreeNode(key, value));
                        node.Value = value;
                    }
                }

                /// <summary>
                /// Gets the value associated with the specified key.
                /// </summary>
                /// <param name="key">The key of the value to get.</param>
                /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
                /// <returns>true if the BinarySearchTree&lt;TKey, TValue> contains an element with the specified key; otherwise, false.</returns>
                public bool TryGetValue(TKey key, out TValue value)
                {
                    if (key == null)
                        throw new ArgumentNullException("key");
                    KeyValueTreeNode node = this.Find(key);
                    if (node == null)
                    {
                        value = default(TValue);
                        return false;
                    }
                    value = node.Value;
                    return true;
                }

                protected class KeyValueTreeNode : TreeNode
                {
                    public KeyValueTreeNode(TKey key, TValue value) { this.KeyValuePair = new KeyValuePair<TKey, TValue>(key, value); }

                    public KeyValuePair<TKey, TValue> KeyValuePair { get; set; }
                    public TKey Key { get { return this.KeyValuePair.Key; } }
                    public TValue Value
                    {
                        get { return this.KeyValuePair.Value; }
                        set { this.KeyValuePair = new KeyValuePair<TKey, TValue>(this.Key, value); }
                    }
                    public new KeyValueTreeNode Parent { get { return (KeyValueTreeNode)base.Parent; } set { base.Parent = value; } }
                    public new KeyValueTreeNode Left { get { return (KeyValueTreeNode)base.Left; } set { base.Left = value; } }
                    public new KeyValueTreeNode Right { get { return (KeyValueTreeNode)base.Right; } set { base.Right = value; } }
                }

                private class BSTEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
                {
                    private BinarySearchTree<TKey, TValue> tree;
                    private KeyValueTreeNode currentNode;

                    public BSTEnumerator(BinarySearchTree<TKey, TValue> tree)
                    {
                        this.tree = tree;
                        this.currentNode = null;
                    }

                    public bool MoveNext()
                    {
                        KeyValueTreeNode successor;
                        if (tree.root == null)
                            return false;
                        if (currentNode == null)
                        {
                            currentNode = (KeyValueTreeNode)tree.root;
                            while (currentNode.Left != null)
                                currentNode = currentNode.Left;
                            return true;
                        }

                        if ((successor = (KeyValueTreeNode)tree.Successor(currentNode)) != null)
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

                bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }

                void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] item, int i) { throw new NotSupportedException(); }

                bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }

                void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) { throw new NotSupportedException(); }
            }
        }
    }
}