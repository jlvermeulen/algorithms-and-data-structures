using System;
using System.Collections.Generic;
using System.Collections;

namespace Utility
{
    namespace DataStructures
    {
        namespace Trees
        {
            public class Trie<TIn, TKey> : ICollection<TIn>
            {
                public delegate TKey[] Split(TIn item);
                public delegate TIn Join(TKey[] keys);
                public delegate int Index(TKey key);

                private Split SplitFunction;
                private Join JoinFunction;
                private Index IndexFunction;
                private TKey[] keys;

                private TrieNode root;

                public Trie(TKey[] keys, Split split, Join join, Index index)
                {
                    this.root = new TrieNode(null, keys.Length);
                    this.keys = keys;
                    this.SplitFunction = split;
                    this.JoinFunction = join;
                    this.IndexFunction = index;
                }

                public void Add(TIn item)
                {
                    TKey[] keys = this.SplitFunction(item);

                    TrieNode current = this.root;
                    foreach (TKey key in keys)
                    {
                        int index = this.IndexFunction(key);
                        if (current[index] == null)
                            current[index] = new TrieNode(current, this.keys.Length);
                        current = current[index];
                    }
                    current.Count++;
                    this.Count++;
                }

                public void AddRange(IEnumerable<TIn> collection)
                {
                    if (collection == null)
                        throw new ArgumentNullException("collection");

                    foreach (TIn t in collection)
                        this.Add(t);
                }

                public bool Remove(TIn item)
                {
                    TKey[] keys = this.SplitFunction(item);
                    TrieNode node = this.Find(keys);
                    if (node == null)
                        return false;

                    if (node.Count > 1)
                        node.Count--;
                    else
                    {
                        if (node.NumberOfChildren > 0)
                            node.Count--;
                        else
                        {
                            int index = this.IndexFunction(keys[0]);
                            for (int i = keys.Length - 1; i >= 0; i--)
                            {
                                index = this.IndexFunction(keys[i]);
                                node = node.Parent;

                                if (node.Count > 0 || node.NumberOfChildren > 1)
                                    break;
                            }
                            node[index] = null;
                        }
                    }

                    this.Count--;
                    return true;
                }

                public bool Contains(TIn item) { return this.Find(this.SplitFunction(item)) != null; }

                public TIn LongestPrefixMatch(TIn item)
                {
                    TKey[] keys = this.SplitFunction(item);
                    TrieNode current = this.root;
                    List<TKey> currentValue = new List<TKey>();
                    TKey[] value = new TKey[0];
                    foreach (TKey key in keys)
                    {
                        int index = this.IndexFunction(key);
                        if (current[index] == null)
                            break;
                        current = current[index];
                        currentValue.Add(this.keys[index]);
                        if (current.Count > 0)
                            value = currentValue.ToArray();
                    }

                    return this.JoinFunction(value);
                }

                public void Clear() { this.root = new TrieNode(null, keys.Length); this.Count = 0; }

                public void CopyTo(TIn[] array, int arrayIndex)
                {
                    if (array == null)
                        throw new ArgumentNullException("array");
                    if (arrayIndex < 0)
                        throw new ArgumentOutOfRangeException("arrayIndex");
                    if (array.Length - arrayIndex < this.Count)
                        throw new ArgumentException("The number of elements in the Trie is greater than the available space from arrayIndex to the end of the destination array.");

                    foreach (TIn t in this)
                        array[arrayIndex++] = t;
                }

                public int Count { get; private set; }

                public bool IsReadOnly { get { return false; } }

                IEnumerator<TIn> IEnumerable<TIn>.GetEnumerator() { return new TrieEnumerator(this); }

                IEnumerator IEnumerable.GetEnumerator() { return new TrieEnumerator(this); }

                private TrieNode Find(TKey[] keys)
                {
                    TrieNode current = this.root;
                    foreach (TKey key in keys)
                    {
                        int index = this.IndexFunction(key);
                        if (current[index] == null)
                            return null;
                        current = current[index];
                    }

                    if (current.Count == 0)
                        return null;

                    return current;
                }

                private class TrieNode
                {
                    private TrieNode[] children;

                    public TrieNode(TrieNode parent, int keyCount)
                    {
                        this.children = new TrieNode[keyCount];
                        this.Parent = parent;
                        this.Count = 0;
                    }

                    public TrieNode this[int index]
                    {
                        get { return this.children[index]; }
                        set
                        {
                            if (this.children[index] == null)
                                this.NumberOfChildren++;
                            else if (value == null)
                                this.NumberOfChildren--;
                            this.children[index] = value;
                        }
                    }

                    public TrieNode Parent { get; set; }

                    public uint Count { get; set; }

                    public uint NumberOfChildren { get; set; }
                }

                private class TrieEnumerator : IEnumerator<TIn>
                {
                    private Trie<TIn, TKey> trie;
                    private TrieNode currentNode;
                    private int currentCount = 0, currentIndex = 0;
                    private List<TKey> currentValue = new List<TKey>();
                    private Stack<int> stack = new Stack<int>();

                    public TrieEnumerator(Trie<TIn, TKey> trie)
                    {
                        this.trie = trie;
                        this.currentNode = this.trie.root;
                        this.stack.Push(0);
                    }

                    public virtual bool MoveNext()
                    {
                        if (this.currentNode.Count > this.currentCount)
                        {
                            this.currentCount++;
                            return true;
                        }

                        this.currentCount = 0;
                        while (this.stack.Count > 0)
                        {
                            bool hasChild = false;
                            for (int i = this.currentIndex; i < this.trie.keys.Length; i++)
                                if (this.currentNode[i] != null)
                                {
                                    hasChild = true;
                                    this.currentNode = this.currentNode[i];
                                    this.stack.Push(i);
                                    this.currentIndex = 0;
                                    this.currentValue.Add(this.trie.keys[i]);

                                    if (this.currentNode.Count > 0)
                                    {
                                        this.currentCount = 1;
                                        return true;
                                    }
                                    else
                                        break;
                                }

                            if (!hasChild)
                            {
                                if (currentNode.Parent == null)
                                    return false;

                                this.currentNode = this.currentNode.Parent;
                                this.currentValue.RemoveAt(this.currentValue.Count - 1);
                                this.currentIndex = this.stack.Pop() + 1;
                            }
                        }

                        return false;
                    }

                    public void Reset()
                    {
                        this.stack.Clear();
                        this.stack.Push(0);
                        this.currentNode = this.trie.root;
                        this.currentValue.Clear();
                        this.currentIndex = 0;
                        this.currentCount = 0;
                    }

                    void IDisposable.Dispose() { }

                    public TIn Current { get { return this.trie.JoinFunction(currentValue.ToArray()); } }

                    object IEnumerator.Current { get { return this.trie.JoinFunction(currentValue.ToArray()); } }
                }
            }
        }
    }
}